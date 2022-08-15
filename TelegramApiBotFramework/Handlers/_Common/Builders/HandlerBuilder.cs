using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using TelegramBotFramework.Exceptions;
using TelegramBotFramework.Handlers._Common.Attributes;
using TelegramBotFramework.Rules;

namespace TelegramBotFramework.Handlers._Common.Builders
{
    public class HandlerBuilder
    {
        public void AddHandlers<TAttribute, THandler>(IServiceCollection serviceCollection)
            where TAttribute : HandlerAttribute
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            var result = new List<ReadyHandler<TAttribute>>();
            var attributeType = typeof(TAttribute);
            var handlerType = typeof(THandler);
            foreach (var assembly in assemblies)
            {
                var types = assembly.GetTypes();
                foreach (var type in types)
                {
                    if (type.IsDefined(attributeType, true) == false)
                        continue;
                    if (type.IsSubclassOf(handlerType) == false)
                        continue;
                    var attributes = type.GetCustomAttributes<TAttribute>().ToList();
                    if (attributes.Count == 0)
                        throw new TelegramBotFrameworkException(ExceptionsMessages.FailToCastAttribute);
                    serviceCollection.AddScoped(type);
                }
            }
        }

        public List<ReadyHandler<TAttribute>> GetRules<TAttribute, THandler>()
            where TAttribute : HandlerAttribute
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            var result = new List<ReadyHandler<TAttribute>>();
            var attributeType = typeof(TAttribute);
            var handlerType = typeof(THandler);
            foreach (var assembly in assemblies)
            {
                var types = assembly.GetTypes();
                foreach (var type in types)
                {
                    if (type.IsDefined(attributeType, true) == false)
                        continue;
                    if (type.IsSubclassOf(handlerType) == false)
                        continue;
                    var attributes = type.GetCustomAttributes<TAttribute>().ToList();
                    if (attributes.Count == 0)
                        throw new TelegramBotFrameworkException(ExceptionsMessages.FailToCastAttribute);
                    var rules = type.GetCustomAttributes<CustomHandlerRuleAttribute>().ToList();
                    foreach (var handlerAttribute in attributes)
                    {
                        result.Add(new ReadyHandler<TAttribute>(
                            type.Name, type, handlerAttribute, rules));
                    }
                }
            }

            return result.OrderByDescending(Q => Q.Attribute.Priority).ToList();
        }
    }
}