using Assets.Models;
using Mapster;
using MapsterMapper;
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
        private readonly IMapper _mapper;
        private readonly HrefHelper _href;

        public TenantsController(Entities.AssetsDbContext db, IMapper mapper, HrefHelper href)
        {
            _db = db;
            _mapper = mapper;
            _href = href;
        }

        [HttpGet]
        public async Task<ActionResult<IList<Tenant>>> SearchAsync()
        {
            List<Tenant> models;

            using (var scope = new MapContextScope())
            {
                scope.Context.Parameters["href"] = _href;

                IQueryable<Tenant> query =
                (
                    from tenant in _db.Tenants
                    where tenant.DeletedDate == null
                        && tenant.UserRoles.Any(x => x.User.UserName == User.Identity.Name)
                    select tenant
                ).ProjectToType<Tenant>(_mapper.Config);

                models = await query.ToListAsync();
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

                IQueryable<Tenant> query =
                (
                    from tenant in _db.Tenants
                    where tenant.DeletedDate == null
                        && tenant.UserRoles.Any(x => x.User.UserName == User.Identity.Name)
                    select tenant
                ).ProjectToType<Tenant>(_mapper.Config);

                model = await query.SingleOrDefaultAsync(x => x.Area == area);
            }

            if (model == null)
            {
                return NotFound();
            }

            return Ok(model);
        }
    }
}
