using Assets.Models;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assets.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TenantsController : ControllerBase
    {
        private readonly Entities.AssetsDbContext _db;
        private readonly TypeAdapterConfig _adapterConfig;
        private readonly HrefHelper _href;

        public TenantsController(Entities.AssetsDbContext db, TypeAdapterConfig adapterConfig, HrefHelper href)
        {
            _db = db;
            _adapterConfig = adapterConfig;
            _href = href;
        }

        [HttpGet]
        public async Task<ActionResult<IList<Tenant>>> SearchAsync()
        {
            List<Tenant> models;

            using (var scope = new MapContextScope())
            {
                scope.Context.Parameters["href"] = _href;
                models = await Query().ToListAsync();
            }

            return Ok(models);
        }

        [HttpGet("{area}")]
        [Authorize]
        public async Task<ActionResult<Tenant>> GetAsync(string area)
        {
            Tenant model;

            using (var scope = new MapContextScope())
            {
                scope.Context.Parameters["href"] = _href;
                model = await Query().SingleOrDefaultAsync(x => x.Area == area);
            }

            if (model == null)
            {
                return NotFound();
            }

            return Ok(model);
        }

        private IQueryable<Tenant> Query()
        {
            return
            (
                from tenant in _db.Tenants
                where tenant.DeletedDate == null
                    && tenant.UserRoles.Any(x => x.User.UserName == User.Identity.Name)
                select tenant
            ).ProjectToType<Tenant>(_adapterConfig);
        }
    }
}
