namespace Assets.Entities
{
    public class UserTenantRole
    {
        public int UserId { get; set; }
        public int TenantId { get; set; }
        public int RoleId { get; set; }
        public virtual User User { get; set; }
        public virtual Tenant Tenant { get; set; }
        public virtual Role Role { get; set; }
    }
}
