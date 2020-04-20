using System;
using System.Collections.Generic;

namespace Assets.Entities
{
    public class Tenant
    {
        public int Id { get; set; }
        public byte[] Version { get; set; }
        public string Area { get; set; }
        public string Name { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public string CreatedUser { get; set; }
        public DateTimeOffset ModifiedDate { get; set; }
        public string ModifiedUser { get; set; }
        public DateTimeOffset? DeletedDate { get; set; }
        public string DeletedUser { get; set; }
        public virtual ICollection<Asset> Assets { get; set; }
        public virtual ICollection<Contact> Contacts { get; set; }
        public virtual ICollection<TenantUserRole> UserRoles { get; set; }
    }
}
