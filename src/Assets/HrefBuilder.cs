using System;
using Assets.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;

namespace Assets
{
    public class HrefBuilder
    {
        private readonly IUrlHelper _url;

        public HrefBuilder(IUrlHelperFactory factory, IActionContextAccessor context)
        {
            _url = factory.GetUrlHelper(context.ActionContext);
        }

        public string Tenants(string area)
        {
            return _url.Action("Get", "Tenants", new { area });
        }

        public string Contacts(string tenant, int contactTypeId, Guid guid)
        {
            if (contactTypeId == (int)ContactTypeId.Person)
            {
                return People(tenant, guid);
            }

            if (contactTypeId == (int)ContactTypeId.Organisation)
            {
                return Organisations(tenant, guid);
            }

            throw new NotSupportedException();
        }

        public string People(string tenant, Guid guid)
        {
            return _url.Action("Get", "People", new { tenant, guid });
        }

        public string Organisations(string tenant, Guid guid)
        {
            return _url.Action("Get", "Organisations", new { tenant, guid });
        }

        public string Computers(string tenant)
        {
            return _url.Action("Search", "Computers", new { tenant });
        }

        public string Computers(string tenant, Guid guid)
        {
            return _url.Action("Get", "Computers", new { tenant, guid });
        }

        public string Monitors(string tenant)
        {
            return _url.Action("Search", "Monitors", new { tenant });
        }

        public string Monitors(string tenant, Guid guid)
        {
            return _url.Action("Get", "Monitors", new { tenant, guid });
        }

        public string Phones(string tenant)
        {
            return _url.Action("Search", "Phones", new { tenant });
        }

        public string Phones(string tenant, Guid guid)
        {
            return _url.Action("Get", "Phones", new { tenant, guid });
        }
    }
}
