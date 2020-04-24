using Mapster;

namespace Assets.Models.Mapping
{
    public class ContactRefRegister : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.ForType<Entities.Contact, ContactRef>()
                .Map(x => x.Href, x => ((HrefHelper)MapContext.Current.Parameters["href"]).Contacts(x.Tenant.Area, x.ContactTypeId, x.Guid));
        }
    }
}
