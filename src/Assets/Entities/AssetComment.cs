using System;

namespace Assets.Entities
{
    public class AssetComment
    {
        public int Id { get; set; }
        public Guid Guid { get; set; }
        public int AssetId { get; set; }
        public string Text { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public string CreatedUser { get; set; }
        public DateTimeOffset ModifiedDate { get; set; }
        public string ModifiedUser { get; set; }
        public DateTimeOffset? DeletedDate { get; set; }
        public string DeletedUser { get; set; }
        public virtual Asset Asset { get; set; }
    }
}
