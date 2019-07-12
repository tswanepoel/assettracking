using System.Collections.Generic;

namespace Assets.Entities
{
    public class AssetType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Asset> Assets { get; set; }
    }
}
