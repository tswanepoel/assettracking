using System;

namespace Assets.Entities
{
    public class ContactComment
    {
        public int Id { get; set; }
        public Guid Guid { get; set; }
        public int ContactId { get; set; }
        public string Text { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public string CreatedUser { get; set; }
        public DateTimeOffset ModifiedDate { get; set; }
        public string ModifiedUser { get; set; }
        public DateTimeOffset? DeletedDate { get; set; }
        public string DeletedUser { get; set; }
        public virtual Contact Contact { get; set; }
    }
}
