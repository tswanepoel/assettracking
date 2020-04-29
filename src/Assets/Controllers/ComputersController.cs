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
        [ProducesResponseType(typeof(IList<Computer>), 200)]
        public async Task<ActionResult<IList>> SearchAsync(string tenant, ODataQueryOptions<Computer> options)
        { 
            options.Validate(new ODataValidationSettings());
            int? tenantId = await GetTenantIdAsync(tenant);

            if (tenantId == null)
            {
                return NotFound();
            }

            IList models;

            using (var scope = new MapContextScope())
            {
                scope.Context.Parameters["href"] = _href;

                IQueryable<Computer> query = DbQuery(tenantId.Value).ProjectToType<Computer>(_mapper.Config);
                models = await options.ApplyTo(query, new ODataQuerySettings { EnsureStableOrdering = false }).ToListAsync();
            }

            return Ok(models);
        }

        [HttpGet("{guid}")]
        [ProducesResponseType(typeof(Computer), 200)]
        public async Task<ActionResult<object>> GetAsync(string tenant, Guid guid, ODataQueryOptions<Computer> options)
        {
            options.Validate(new ODataValidationSettings { AllowedQueryOptions = AllowedQueryOptions.Select | AllowedQueryOptions.Expand });
            int? tenantId = await GetTenantIdAsync(tenant);

            if (tenantId == null)
            {
                return NotFound();
            }
            
            object model;

            using (var scope = new MapContextScope())
            {
                scope.Context.Parameters["href"] = _href;

                IQueryable<Computer> query = DbQuery(tenantId.Value, guid).ProjectToType<Computer>(_mapper.Config);
                model = await options.ApplyTo(query).SingleOrDefaultAsync();
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

            return await PostCoreAsync(tenantId.Value, model);
        }

        [HttpPatch("guid")]
        public async Task<ActionResult<Computer>> PatchAsync(string tenant, Guid guid, JsonPatchDocument<Computer> patch)
        {
            int? tenantId = await GetTenantIdAsync(tenant);

            if (tenantId == null)
            {
                return NotFound();
            }

            return await PatchCoreAsync(tenantId.Value, guid, patch);
        }

        [HttpPut("guid")]
        public async Task<ActionResult<Computer>> PutAsync(string tenant, Guid guid, Computer model)
        {
            if (model.Guid != default && guid != model.Guid)
            {
                return BadRequest();
            }

            int? tenantId = await GetTenantIdAsync(tenant);

            if (tenantId == null)
            {
                return NotFound();
            }

            return await PutCoreAsync(tenantId.Value, guid, model);
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

        private async Task<ActionResult<Computer>> PostCoreAsync(int tenantId, Computer model)
        {
            Guid guid = await CreateCoreAsync(tenantId, model);
            model = await RetrieveCoreAsync(tenantId, guid);

            return Ok(model);
        }

        private async Task<ActionResult<Computer>> PatchCoreAsync(int tenantId, Guid guid, JsonPatchDocument<Computer> patch)
        {
            Computer model = await RetrieveCoreAsync(tenantId, guid);
            patch.ApplyTo(model);

            return await PutCoreAsync(tenantId, guid, model);
        }

        private async Task<ActionResult<Computer>> PutCoreAsync(int tenantId, Guid guid, Computer model)
        {
            bool created = await CreateOrUpdateCoreAsync(tenantId, guid, model);
            model = await RetrieveCoreAsync(tenantId, guid);

            if (created)
            {
                return Created(model.Href, model);
            }

            return Ok(model);
        }

        private async Task<Computer> RetrieveCoreAsync(int tenantId, Guid guid)
        {
            using var scope = new MapContextScope();
            scope.Context.Parameters["href"] = _href;

            IQueryable<Computer> query = DbQuery(tenantId, guid).ProjectToType<Computer>(_mapper.Config);
            return await query.SingleOrDefaultAsync();
        }

        private async Task<bool> CreateOrUpdateCoreAsync(int tenantId, Guid guid, Computer model)
        {
            Entities.Computer entity = await DbQuery(tenantId, guid).SingleOrDefaultAsync();
            
            if (entity == null)
            {
                model.Guid = guid;
                
                await CreateCoreAsync(tenantId, model);
                return true;
            }

            _mapper.Map(model, entity);
            Touch(entity, DateTimeOffset.Now, User.Identity.Name);
            
            await _db.SaveChangesAsync();
            return false;
        }

        private async Task<Guid> CreateCoreAsync(int tenantId, Computer model)
        {
            var entity = _mapper.Map<Entities.Computer>(model);
            _db.Computers.Add(entity);

            Guid guid = model.Guid != default ? model.Guid : Guid.NewGuid();
            Init(entity, tenantId, guid, DateTimeOffset.Now, User.Identity.Name);
            
            await _db.SaveChangesAsync();
            return guid;
        }

        private static void Init(Entities.Computer entity, int tenantId, Guid guid, DateTimeOffset now, string user)
        {
            entity.Asset ??= new Entities.Asset();
            entity.Asset.TenantId = tenantId;
            entity.Asset.Guid = guid;
            entity.Asset.AssetTypeId = (int)Entities.AssetTypeId.Computer;
            entity.Asset.CreatedDate = now;
            entity.Asset.CreatedUser = user;
            Touch(entity, now, user);
        }

        private static void Touch(Entities.Computer entity, DateTimeOffset now, string user)
        {
            entity.Asset.ModifiedDate = now;
            entity.Asset.ModifiedUser = user;
        }

        private IQueryable<Entities.Computer> DbQuery(int tenantId, Guid guid)
        {
            return DbQuery(tenantId).Where(x => x.Asset.Guid == guid);
        }

        private IQueryable<Entities.Computer> DbQuery(int tenantId)
        {
            return
                from computer in _db.Computers.Include(x => x.Asset)
                where computer.Asset.TenantId == tenantId
                    && computer.Asset.DeletedDate == null
                select computer;
        }
    }
}
