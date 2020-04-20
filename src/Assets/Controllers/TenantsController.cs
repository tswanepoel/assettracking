using Assets.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assets.Controllers
{
    [Route("api/[controller]")]
    public class TenantsController : Controller
    {
        private readonly Entities.AssetsDbContext _db;

        public TenantsController(Entities.AssetsDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        [Authorize(Policy = "Authenticated")]
        public async Task<ActionResult<IList<Tenant>>> GetAsync()
        {
            return Ok(await Query().ToListAsync());
        }

        [HttpGet("{area}")]
        [Authorize(Policy = "Authenticated")]
        public async Task<ActionResult<Tenant>> GetAsync([FromRoute] string area)
        {
            Tenant model = await Query().SingleOrDefaultAsync(x => x.Area == area);

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
                select new Tenant
                {
                    Href = Url.Action("GetAsync", new { area = tenant.Area }),
                    Area = tenant.Area,
                    Version = tenant.Version,
                    Name = tenant.Name
                }
            );
        }
    }
}
