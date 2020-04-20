namespace Assets.Entities
{
    public class Monitor
    {
        public int MonitorId { get; set; }
        public decimal? SizeInches { get; set; }
        public virtual Asset Asset { get; set; }
    }
}
