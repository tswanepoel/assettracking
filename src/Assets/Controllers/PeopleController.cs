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
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Assets.Controllers
{
    [Route("api/tenants/{tenant}/[controller]")]
    [Authorize]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        private readonly Entities.AssetsDbContext _db;
        private readonly IMapper _mapper;
        private readonly HrefBuilder _href;

        public PeopleController(Entities.AssetsDbContext db, IMapper mapper, HrefBuilder href)
        {
            _db = db;
            _mapper = mapper;
            _href = href;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IList<Person>), 200)]
        public async Task<ActionResult> SearchAsync(string tenant, ODataQueryOptions<Person> options)
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

                IQueryable<Person> query =
                (
                    from contact in _db.Contacts
                    where contact.ContactTypeId == (int)Entities.ContactTypeId.Person
                        && contact.TenantId == tenantId
                        && contact.DeletedDate == null
                    select contact
                ).ProjectToType<Person>(_mapper.Config);

                models = await options.ApplyTo(query).ToListAsync();
            }

            return Ok(models);
        }

        [HttpGet("guid")]
        public async Task<ActionResult<Person>> GetAsync(string tenant, Guid guid)
        {
            int? tenantId = await GetTenantIdAsync(tenant);

            if (tenantId == null)
            {
                return NotFound();
            }
            
            Person model;

            using (var scope = new MapContextScope())
            {
                scope.Context.Parameters["href"] = _href;

                IQueryable<Person> query =
                (
                    from contact in _db.Contacts
                    where contact.ContactTypeId == (int)Entities.ContactTypeId.Person
                        && contact.TenantId == tenantId
                        && contact.DeletedDate == null
                    select contact
                ).ProjectToType<Person>(_mapper.Config);

                model = await query.SingleOrDefaultAsync(x => x.Guid == guid);
            }

            if (model == null)
            {
                return NotFound();
            }

            return Ok(model);
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