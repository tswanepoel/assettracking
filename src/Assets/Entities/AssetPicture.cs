using System;

namespace Assets.Entities
{
    public class AssetPicture
    {
        public int AssetId { get; set; }
        public int PictureId { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public string CreatedUser { get; set; }
        public DateTimeOffset ModifiedDate { get; set; }
        public string ModifiedUser { get; set; }
        public DateTimeOffset? DeletedDate { get; set; }
        public string DeletedUser { get; set; }
        public virtual Asset Asset { get; set; }
        public virtual Blob Picture { get; set; }
    }
}
