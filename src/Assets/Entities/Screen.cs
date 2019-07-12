namespace Assets.Entities
{
    public class Screen
    {
        public int ScreenId { get; set; }
        public decimal? SizeInches { get; set; }
        public virtual Asset Asset { get; set; }
    }
}
