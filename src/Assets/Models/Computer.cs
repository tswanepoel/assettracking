using System;

namespace Assets.Models
{
    public class Computer
    {
        public string Href { get; set; }
        public Guid Guid { get; set; }
        public byte[] Version { get; set; }
        public string Description { get; set; }
        public string SerialNumber { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string Tag { get; set; }
        public string Processor { get; set; }
        public long? Memory { get; set; }
        public ContactRef AllocatedContact { get; set; }
    }
}
