namespace Assets.Models
{
    public class Tenant
    {
        public string Href { get; set; }
        public string Area { get; set; }
        public byte[] Version { get; set; }
        public string Name { get; set; }
        public CollectionRef Computers { get; set; }
        public CollectionRef Monitors { get; set; }
        public CollectionRef Phones { get; set; }
    }
}
