using System;
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
            MethodInfo method = typeof(Microsoft.EntityFrameworkCore.EntityFrameworkQueryableExtensions)
                .GetMethod("ToListAsync", BindingFlags.Public | BindingFlags.Static)
                .MakeGenericMethod(source.ElementType);

            object task = method.Invoke(null, new object[] { source, cancellationToken });

            MethodInfo getAwaiterMethod = method.ReturnType.GetMethod("GetAwaiter");
            object awaiter = getAwaiterMethod.Invoke(task, null);

            MethodInfo getResultMethod = getAwaiterMethod.ReturnType.GetMethod("GetResult");
            return Task.FromResult((IList)getResultMethod.Invoke(awaiter, null));
        }

        public static Task<object> SingleOrDefaultAsync(this IQueryable source, CancellationToken cancellationToken = default)
        {
            MethodInfo method = typeof(Microsoft.EntityFrameworkCore.EntityFrameworkQueryableExtensions)
                .GetMethod("SingleOrDefaultAsync", 1,
                    new[] 
                    { 
                        typeof(IQueryable<>).MakeGenericType(Type.MakeGenericMethodParameter(0)), 
                        typeof(CancellationToken)
                    })
                .MakeGenericMethod(source.ElementType);

            object task = method.Invoke(null, new object[] { source, cancellationToken });

            MethodInfo getAwaiterMethod = method.ReturnType.GetMethod("GetAwaiter");
            object awaiter = getAwaiterMethod.Invoke(task, null);

            MethodInfo getResultMethod = getAwaiterMethod.ReturnType.GetMethod("GetResult");
            return Task.FromResult(getResultMethod.Invoke(awaiter, null));
        }
    }
}
