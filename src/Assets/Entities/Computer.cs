namespace Assets.Entities
{
    public class Computer
    {
        public int ComputerId { get; set; }
        public string Processor { get; set; }
        public long? Memory { get; set; }
        public virtual Asset Asset { get; set; }
    }
}
