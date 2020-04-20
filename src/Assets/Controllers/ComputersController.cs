using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Assets.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Assets
{
    [Route("api/{tenant}/[controller]")]
    [ApiController]
    public class ComputersController : ControllerBase
    {
        private readonly Entities.AssetsDbContext _db;

        public ComputersController(Entities.AssetsDbContext db)
        {
            _db = db;
        }

        [HttpGet("guid")]
        [Authorize(Policy = "Authenticated")]
        public async Task<ActionResult<Computer>> GetAsync([FromRoute] string tenant, [FromRoute] Guid guid)
        {
            int? tenantId = await GetTenantIdAsync(tenant);

            if (tenantId == null)
            {
                return NotFound();
            }
            
            Computer model = await Query((int)tenantId).SingleOrDefaultAsync(x => x.Guid == guid);

            if (model == null)
            {
                return NotFound();
            }

            return Ok(model);
        }

        [HttpGet]
        [Authorize(Policy = "Authenticated")]
        public async Task<ActionResult<IList<Computer>>> GetAsync([FromRoute] string tenant)
        {
            int? tenantId = await GetTenantIdAsync(tenant);

            if (tenantId == null)
            {
                return NotFound();
            }

            return Ok(await Query((int)tenantId).ToListAsync());
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
                let allocatedContactHref = computer.Asset.AllocatedContact.ContactType.Id == (int)Entities.ContactTypeId.Person
                    ? Url.Action("GetAsync", "People", new { tenant = computer.Asset.AllocatedContact.Tenant.Area, guid = computer.Asset.AllocatedContact.Guid })
                    : (computer.Asset.AllocatedContact.ContactType.Id == (int)Entities.ContactTypeId.Organisation
                        ? Url.Action("GetAsync", "Organisations", new { tenant = computer.Asset.AllocatedContact.Tenant.Area, guid = computer.Asset.AllocatedContact.Guid })
                        : null)
                select new Computer
                {
                    Href = Url.Action("GetAsync", new { tenant = computer.Asset.Tenant.Area, guid = computer.Asset.Guid }),
                    Guid = computer.Asset.Guid,
                    Version = computer.Asset.Version,
                    Description = computer.Asset.Description,
                    SerialNumber = computer.Asset.SerialNumber,
                    Make = computer.Asset.Make,
                    Model = computer.Asset.Model,
                    Tag = computer.Asset.Tag,
                    Processor = computer.Processor,
                    Memory = computer.Memory,
                    AllocatedContact = computer.Asset.AllocatedContact != null
                        ? new ContactRef
                        {
                            Href = allocatedContactHref,
                            Name = computer.Asset.AllocatedContact.Name
                        }
                        : null
                }
            );
        }
    }
}
