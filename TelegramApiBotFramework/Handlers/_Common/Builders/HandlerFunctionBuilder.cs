﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TelegramBotFramework.Exceptions;
using TelegramBotFramework.Handlers._Common.Attributes;
using TelegramBotFramework.Rules;

namespace TelegramBotFramework.Handlers._Common.Builders
{
    public class HandlerFunctionBuilder<TContext> where TContext : HandlerContext
    {
        public List<ReadyHandler<TAttribute, TContext, THandler>> BuildConstructorFunctions<TAttribute, THandler>()
            where TAttribute : HandlerAttribute
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            var result = new List<ReadyHandler<TAttribute, TContext, THandler>>();
            var attributeType = typeof(TAttribute);
            var handlerType = typeof(THandler);
            var contextType = typeof(TContext);
            foreach (var assembly in assemblies)
            {
                var types = assembly.GetTypes();
                foreach (var type in types)
                {
                    if (type.IsDefined(attributeType, true) == false)
                        continue;
                    if (type.IsSubclassOf(handlerType) == false)
                        continue;
                    var contextPram = Expression.Parameter(contextType);
                    var ctor = type.GetConstructor(new[] { contextType });
                    if (ctor == null)
                        throw new TelegramBotFrameworkException(ExceptionsMessages.HandlerInvalidConstructor,
                            type.Name);
                    var ctorExpression = Expression.New(ctor, contextPram);
                    var func = Expression.Lambda<Func<TContext, THandler>>(ctorExpression, contextPram).Compile();
                    var attributes = type.GetCustomAttributes<TAttribute>().ToList();
                    if (attributes.Count == 0)
                        throw new TelegramBotFrameworkException(ExceptionsMessages.FailToCastAttribute);
                    var rules = type.GetCustomAttributes<CustomHandlerRuleAttribute>().ToList();
                    foreach (var handlerAttribute in attributes)
                    {
                        result.Add(new ReadyHandler<TAttribute, TContext, THandler>(
                            type.Name, handlerAttribute, func, rules));
                    }
                }
            }

            return result.OrderByDescending(Q => Q.Attribute.Priority).ToList();
        }
    }
}