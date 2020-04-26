using Assets.Extensions;
using Assets.Models;
using Mapster;
using MapsterMapper;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assets.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class TenantsController : ControllerBase
    {
        private readonly Entities.AssetsDbContext _db;
        private readonly IMapper _mapper;
        private readonly HrefBuilder _href;

        public TenantsController(Entities.AssetsDbContext db, IMapper mapper, HrefBuilder href)
        {
            _db = db;
            _mapper = mapper;
            _href = href;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IList<Tenant>), 200)]
        public async Task<ActionResult> SearchAsync(ODataQueryOptions<Tenant> options)
        {
            IList models;

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

                models = await options.ApplyTo(query).ToListAsync();
            }

            return Ok(models);
        }

        [HttpGet("{area}")]
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
