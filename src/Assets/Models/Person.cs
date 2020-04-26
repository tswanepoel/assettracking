using System;

namespace Assets.Models
{
    public class Person
    {
        public string Href { get; set; }
        public Guid Guid { get; set; }
        public byte[] Version { get; set; }
        public string Name { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public string CreatedUser { get; set; }
        public DateTimeOffset ModifiedDate { get; set; }
        public string ModifiedUser { get; set; }
    }
}