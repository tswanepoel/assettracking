using Mapster;

namespace Assets.Models.Mapping
{
    public class ComputerRegister : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.ForType<Entities.Computer, Computer>()
                .Map(x => x.Href, x => ((HrefBuilder)MapContext.Current.Parameters["href"]).Computers(x.Asset.Tenant.Area, x.Asset.Guid))
                .Map(x => x.AllocatedDate, x => x.Asset.AllocatedDate)
                .Map(x => x.AllocatedUser, x => x.Asset.AllocatedUser)
                .Map(x => x.CreatedDate, x => x.Asset.CreatedDate)
                .Map(x => x.CreatedUser, x => x.Asset.CreatedUser)
                .Map(x => x.ModifiedDate, x => x.Asset.ModifiedDate)
                .Map(x => x.ModifiedUser, x => x.Asset.ModifiedUser)
                .Map(x => x.AllocatedContact, x => x.Asset.AllocatedContact)
                .TwoWays()
                .Map(x => x.Guid, x => x.Asset.Guid)
                .Map(x => x.Version, x => x.Asset.Version)
                .Map(x => x.Description, x => x.Asset.Description)
                .Map(x => x.SerialNumber, x => x.Asset.SerialNumber)
                .Map(x => x.Make, x => x.Asset.Make)
                .Map(x => x.Model, x => x.Asset.Model)
                .Map(x => x.Tag, x => x.Asset.Tag);                
        }
    }
}
