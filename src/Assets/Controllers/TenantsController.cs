using Assets.Extensions;
using Assets.Models;
using Mapster;
using MapsterMapper;
using Microsoft.AspNet.OData;
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
            options.Validate(new ODataValidationSettings());
            IList models;

            using (var scope = new MapContextScope())
            {
                scope.Context.Parameters["href"] = _href;

                IQueryable query =
                (
                    from tenant in _db.Tenants
                    where tenant.DeletedDate == null
                        && tenant.UserRoles.Any(x => x.User.UserName == User.Identity.Name)
                    select tenant
                ).ProjectToType<Tenant>(_mapper.Config);

                models = await options.ApplyTo(query,new ODataQuerySettings { EnsureStableOrdering = false }).ToListAsync();
            }

            return Ok(models);
        }

        [HttpGet("{area}")]
        [ProducesResponseType(typeof(Tenant), 200)]
        public async Task<ActionResult<Tenant>> GetAsync(string area, ODataQueryOptions<Tenant> options)
        {
            options.Validate(new ODataValidationSettings { AllowedQueryOptions = AllowedQueryOptions.Select | AllowedQueryOptions.Expand });
            object model;

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

                model = await options.ApplyTo(query.Where(x => x.Area == area)).SingleOrDefaultAsync();
            }

            if (model == null)
            {
                return NotFound();
            }

            return Ok(model);
        }
    }
}
