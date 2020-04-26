using System.Linq;
using Assets.Entities;
using Mapster;

namespace Assets.Models.Mapping
{
    public class TenantRegister : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.ForType<Entities.Tenant, Tenant>()
                .Map(x => x.Href, x => ((HrefBuilder)MapContext.Current.Parameters["href"]).Tenants(x.Area))
                .Map(x => x.Computers.Href, x => ((HrefBuilder)MapContext.Current.Parameters["href"]).Computers(x.Area))
                .Map(x => x.Computers.Count, x => x.Assets.Count(x => x.AssetTypeId == (int)AssetTypeId.Computer))
                .Map(x => x.Monitors.Href, x => ((HrefBuilder)MapContext.Current.Parameters["href"]).Monitors(x.Area))
                .Map(x => x.Monitors.Count, x => x.Assets.Count(x => x.AssetTypeId == (int)AssetTypeId.Monitor))
                .Map(x => x.Phones.Href, x => ((HrefBuilder)MapContext.Current.Parameters["href"]).Phones(x.Area))
                .Map(x => x.Phones.Count, x => x.Assets.Count(x => x.AssetTypeId == (int)AssetTypeId.Phone));
        }
    }
}
