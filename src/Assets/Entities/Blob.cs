using System;
using System.Collections.Generic;

namespace Assets.Entities
{
    public class Blob
    {
        public int Id { get; set; }
        public Guid Guid { get; set; }
        public string ContentType { get; set; }
        public byte[] Content { get; set; }
    }
}
