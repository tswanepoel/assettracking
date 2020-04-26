using System.Collections;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace Assets.Extensions
{
    public static class EntityFrameworkQueryableExtensions
    {
        public static Task<IList> ToListAsync(this IQueryable source, CancellationToken cancellationToken = default)
        {
            MethodInfo toListAsyncMethod = typeof(Microsoft.EntityFrameworkCore.EntityFrameworkQueryableExtensions)
                .GetMethod("ToListAsync", BindingFlags.Public | BindingFlags.Static)
                .MakeGenericMethod(source.ElementType);

            object task = toListAsyncMethod.Invoke(null, new object[] { source, null });

            MethodInfo getAwaiterMethod = toListAsyncMethod.ReturnType.GetMethod("GetAwaiter");
            object awaiter = getAwaiterMethod.Invoke(task, null);

            MethodInfo getResultMethod = getAwaiterMethod.ReturnType.GetMethod("GetResult");
            return Task.FromResult((IList)getResultMethod.Invoke(awaiter, null));
        }
    }
}
