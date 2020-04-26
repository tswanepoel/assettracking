using Microsoft.AspNet.OData.Query;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Linq;
using System.Collections.Generic;

namespace Assets
{
    public class ODataQueryOptionsFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            List<string> exclude = context.ApiDescription.ParameterDescriptions
                .Where(x => x.Type.IsSubclassOf(typeof(ODataQueryOptions)))
                .Select(x => x.Name)
                .ToList();
            
            if (exclude.Count == 0)
            {
                return;
            }

            for (var i = 0; i < operation.Parameters.Count; i++)
            {
                if (exclude.Contains(operation.Parameters[i].Name))
                {
                    operation.Parameters.RemoveAt(i);
                    i--;
                }
            }
        }
    }
}
