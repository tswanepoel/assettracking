using Mapster;

namespace Assets.Models.Mapping
{
    public class PersonRegister : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.ForType<Entities.Contact, Person>()
                .Map(x => x.Href, x => ((HrefBuilder)MapContext.Current.Parameters["href"]).People(x.Tenant.Area, x.Guid))
                .Map(x => x.CreatedDate, x => x.CreatedDate)
                .Map(x => x.CreatedUser, x => x.CreatedUser)
                .Map(x => x.ModifiedDate, x => x.ModifiedDate)
                .Map(x => x.ModifiedUser, x => x.ModifiedUser)
                .TwoWays()
                .Map(x => x.Guid, x => x.Guid)
                .Map(x => x.Version, x => x.Version)
                .Map(x => x.Name, x => x.Name);                
        }
    }
}
