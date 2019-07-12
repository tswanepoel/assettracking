using System;
using System.Collections.Generic;

namespace Assets.Entities
{
    public class Asset
    {
        public int Id { get; set; }
        public int TenantId { get; set; }
        public int AssetTypeId { get; set; }
        public Guid Guid { get; set; }
        public byte[] Version { get; set; }
        public string Description { get; set; }
        public string SerialNumber { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string Tag { get; set; }
        public int? AllocatedContactId { get; set; }
        public DateTimeOffset? AllocatedDate { get; set; }
        public string AllocatedUser { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public string CreatedUser { get; set; }
        public DateTimeOffset ModifiedDate { get; set; }
        public string ModifiedUser { get; set; }
        public DateTimeOffset? DeletedDate { get; set; }
        public string DeletedUser { get; set; }
        public virtual AssetType AssetType { get; set; }
        public virtual Computer Computer { get; set; }
        public virtual Screen Screen { get; set; }
        public virtual Phone Phone { get; set; }
        public virtual Tenant Tenant { get; set; }
        public virtual ICollection<AssetPicture> Pictures { get; set; }
        public virtual ICollection<AssetComment> Comments { get; set; }
        public virtual Contact AllocatedContact { get; set; }
        public virtual ICollection<AssetAllocationChange> AllocationChanges { get; set; }
    }
}
