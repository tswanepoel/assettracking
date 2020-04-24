using Mapster;

namespace Assets.Models.Mapping
{
    public class ComputerRegister : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.ForType<Entities.Computer, Computer>()
                .Map(x => x.Href, x => ((HrefHelper)MapContext.Current.Parameters["href"]).Computers(x.Asset.Tenant.Area, x.Asset.Guid))
                .Map(x => x.Guid, x => x.Asset.Guid)
                .Map(x => x.Version, x => x.Asset.Version)
                .Map(x => x.Description, x => x.Asset.Description)
                .Map(x => x.SerialNumber, x => x.Asset.SerialNumber)
                .Map(x => x.Make, x => x.Asset.Make)
                .Map(x => x.Model, x => x.Asset.Model)
                .Map(x => x.Tag, x => x.Asset.Tag)
                .Map(x => x.AllocatedContact, x => x.Asset.AllocatedContact);
        }
    }
}
