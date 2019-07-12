using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Assets.Entities
{
    public class User
    {
        public int Id { get; set; }
        public byte[] Version { get; set; }
        public string UserName { get; set; }
        public byte[] PasswordSalt { get; set; }
        public byte[] PasswordHash { get; set; }
        public string FullName { get; set; }
        public string Initials { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public DateTimeOffset? LastAccessedDate { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public string CreatedUser { get; set; }
        public DateTimeOffset ModifiedDate { get; set; }
        public string ModifiedUser { get; set; }
        public DateTimeOffset? DeletedDate { get; set; }
        public string DeletedUser { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
        public virtual ICollection<UserTenantRole> TenantRoles { get; set; }
    }
}
