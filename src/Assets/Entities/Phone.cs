namespace Assets.Entities
{
    public class Phone
    {
        public int PhoneId { get; set; }
        public string Imei { get; set; }
        public string Processor { get; set; }
        public long? Memory { get; set; }
        public virtual Asset Asset { get; set; }
    }
}
