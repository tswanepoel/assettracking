using System;
using System.Collections.Generic;

namespace Assets.Entities
{
    public class Contact
    {
        public int Id { get; set; }
        public int ContactTypeId { get; set; }
        public int TenantId { get; set; }
        public Guid Guid { get; set; }
        public byte[] Version { get; set; }
        public string Name { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public string CreatedUser { get; set; }
        public DateTimeOffset ModifiedDate { get; set; }
        public string ModifiedUser { get; set; }
        public DateTimeOffset? DeletedDate { get; set; }
        public string DeletedUser { get; set; }
        public virtual ContactType ContactType { get; set; }
        public virtual Tenant Tenant { get; set; }
        public virtual ICollection<ContactPicture> Pictures { get; set; }
        public virtual ICollection<ContactComment> Comments { get; set; }
        public virtual ICollection<Asset> Assets { get; set; }
    }
}
