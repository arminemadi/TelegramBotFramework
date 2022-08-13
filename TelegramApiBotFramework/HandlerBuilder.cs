using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TelegramBotFramework.Handlers;
using TelegramBotFramework.Handlers.Models;

namespace TelegramBotFramework
{
    public class HandlerBuilder<TContext , THandler> where TContext : HandlerContext
    {
        public List<CachedHandler<TAttribute , TContext , THandler>> BuildConstructors<TAttribute>() where TAttribute : HandlerAttribute
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            var result = new List<CachedHandler<TAttribute, TContext, THandler>>();
            var attributeType = typeof(TAttribute);
            var handlerType = typeof(THandler);
            var contextType = typeof(TContext);
            foreach (var assembly in assemblies)
            {
                var types = assembly.GetTypes();
                foreach (var type in types)
                {
                    if(type.IsDefined(attributeType,true) == false)
                        continue;
                    if(type.IsSubclassOf(handlerType) == false)
                        continue;
                    var contextPram = Expression.Parameter(type);
                    var ctor = type.GetConstructor(new []{ contextType });
                    if (ctor == null)
                        throw new Exception($"{type.FullName} does not have suitable constructor.");
                    var ctorExpression = Expression.New(ctor, contextPram);
                    var func = Expression.Lambda<Func<TContext , THandler>>(ctorExpression).Compile();
                    if(type.GetCustomAttribute(attributeType) is not TAttribute attribute)
                        throw new Exception($"Unable to cast into handle attribute.");
                    result.Add(new CachedHandler<TAttribute, TContext, THandler>(
                        attribute , func));
                }

            }
            return result.
                OrderBy(Q => Q.Attribute.AlwaysRun)
                .ThenByDescending(Q => Q.Attribute.Priority).ToList()
        }
    }
}
