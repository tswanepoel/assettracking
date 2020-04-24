using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Assets.Models;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Assets
{
    [Route("api/tenants/{tenant}/[controller]")]
    [Authorize]
    [ApiController]
    public class ComputersController : ControllerBase
    {
        private readonly Entities.AssetsDbContext _db;
        private readonly TypeAdapterConfig _adapterConfig;
        private readonly HrefHelper _href;

        public ComputersController(Entities.AssetsDbContext db, TypeAdapterConfig adapterConfig, HrefHelper href)
        {
            _db = db;
            _adapterConfig = adapterConfig;
            _href = href;
        }

        [HttpGet]
        public async Task<ActionResult<IList<Computer>>> SearchAsync(string tenant)
        {
            int? tenantId = await GetTenantIdAsync(tenant);

            if (tenantId == null)
            {
                return NotFound();
            }

            List<Computer> models;

            using (var scope = new MapContextScope())
            {
                scope.Context.Parameters["href"] = _href;
                models = await Query((int)tenantId).ToListAsync();
            }

            return Ok(models);
        }

        [HttpGet("guid")]
        public async Task<ActionResult<Computer>> GetAsync(string tenant, Guid guid)
        {
            int? tenantId = await GetTenantIdAsync(tenant);

            if (tenantId == null)
            {
                return NotFound();
            }
            
            Computer model;

            using (var scope = new MapContextScope())
            {
                scope.Context.Parameters["href"] = _href;
                model = await Query((int)tenantId).SingleOrDefaultAsync(x => x.Guid == guid);
            }

            if (model == null)
            {
                return NotFound();
            }

            return Ok(model);
        }

        [HttpPost]
        public async Task<ActionResult<Computer>> PostAsync(string tenant, Computer model)
        {
            throw new NotImplementedException();
        }

        [HttpPut("guid")]
        public async Task<ActionResult<Computer>> PutAsync(string tenant, Guid guid, Computer model)
        {
            throw new NotImplementedException();
        }

        private async Task<int?> GetTenantIdAsync(string tenant)
        {
            return
            (
                await
                (
                    from t in _db.Tenants
                    where t.DeletedDate == null
                        && t.UserRoles.Any(x => x.User.UserName == User.Identity.Name)
                        && t.Area == tenant
                    select new { t.Id }
                ).SingleOrDefaultAsync()
            )?.Id;
        }

        private IQueryable<Computer> Query(int tenantId)
        {
            return
            (
                from computer in _db.Computers
                where computer.Asset.TenantId == tenantId
                    && computer.Asset.DeletedDate == null
                select computer
            ).ProjectToType<Computer>(_adapterConfig);
        }
    }
}
