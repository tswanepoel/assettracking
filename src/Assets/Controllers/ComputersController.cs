using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Assets.Extensions;
using Assets.Models;
using Mapster;
using MapsterMapper;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
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
        private readonly IMapper _mapper;
        private readonly HrefBuilder _href;

        public ComputersController(Entities.AssetsDbContext db, IMapper mapper, HrefBuilder href)
        {
            _db = db;
            _mapper = mapper;
            _href = href;
        }

        [HttpGet]
        public async Task<ActionResult<IList<Computer>>> SearchAsync(string tenant, ODataQueryOptions<Computer> options)
        {
            int? tenantId = await GetTenantIdAsync(tenant);

            if (tenantId == null)
            {
                return NotFound();
            }

            IList models;

            using (var scope = new MapContextScope())
            {
                scope.Context.Parameters["href"] = _href;

                IQueryable<Computer> query =
                (
                    from computer in _db.Computers
                    where computer.Asset.TenantId == tenantId
                        && computer.Asset.DeletedDate == null
                    select computer
                ).ProjectToType<Computer>(_mapper.Config);

                models = await options.ApplyTo(query).ToListAsync();
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

                IQueryable<Computer> query =
                (
                    from computer in _db.Computers
                    where computer.Asset.TenantId == tenantId
                        && computer.Asset.DeletedDate == null
                    select computer
                ).ProjectToType<Computer>(_mapper.Config);

                model = await query.SingleOrDefaultAsync(x => x.Guid == guid);
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
            int? tenantId = await GetTenantIdAsync(tenant);

            if (tenantId == null)
            {
                return NotFound();
            }

            var entity = new Entities.Computer 
            {
                Asset = new Entities.Asset
                {
                    TenantId = tenantId.Value,
                    AssetTypeId = (int)Entities.AssetTypeId.Computer
                }
            };

            _mapper.Map(model, entity);

            if (entity.Asset.Guid == default)
            {
                entity.Asset.Guid = Guid.NewGuid();
            }

            entity.Asset.ModifiedDate =
                entity.Asset.CreatedDate = DateTimeOffset.Now;

            entity.Asset.ModifiedUser = 
                entity.Asset.CreatedUser = User.Identity.Name;

            _db.Computers.Add(entity);
            await _db.SaveChangesAsync();

            using (var scope = new MapContextScope())
            {
                scope.Context.Parameters["href"] = _href;

                IQueryable<Computer> query =
                (
                    from computer in _db.Computers
                    where computer.Asset.TenantId == tenantId
                        && computer.Asset.DeletedDate == null
                    select computer
                ).ProjectToType<Computer>(_mapper.Config);

                model = await query.SingleOrDefaultAsync(x => x.Guid == entity.Asset.Guid);
            }

            return Created(model.Href, model);
        }

        [HttpPut("guid")]
        public async Task<ActionResult<Computer>> PutAsync(string tenant, Guid guid, Computer model)
        {
            int? tenantId = await GetTenantIdAsync(tenant);

            if (tenantId == null)
            {
                return NotFound();
            }

            throw new NotImplementedException();
        }

        [HttpPatch("guid")]
        public async Task<ActionResult<Computer>> PatchAsync(string tenant, Guid guid, JsonPatchDocument<Computer> patch)
        {
            int? tenantId = await GetTenantIdAsync(tenant);

            if (tenantId == null)
            {
                return NotFound();
            }

            throw new NotImplementedException();
        }

        private async Task<int?> GetTenantIdAsync(string tenant)
        {
            var query =
                from t in _db.Tenants
                where t.DeletedDate == null
                    && t.UserRoles.Any(x => x.User.UserName == User.Identity.Name)
                    && t.Area == tenant
                select new { t.Id };

            return (await query.SingleOrDefaultAsync())?.Id;
        }
    }
}
