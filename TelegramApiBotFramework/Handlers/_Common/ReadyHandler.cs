using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramBotFramework.Handlers._Common.Attributes;
using TelegramBotFramework.Rules;

namespace TelegramBotFramework.Handlers._Common
{
    public class ReadyHandler<TAttribute, TContext, THandler> where TContext : HandlerContext where TAttribute : HandlerAttribute
    {
        public ReadyHandler(string name , IReadOnlyList<TAttribute> attributes, Func<TContext, THandler> constructorFunc, IReadOnlyList<CustomHandlerRuleAttribute> customRuleAttributes)
        {
            Name = name;
            Attributes = attributes;
            ConstructorFunc = constructorFunc;
            Rules = customRuleAttributes;
        }

        public string Name { get; }
        public IReadOnlyList<TAttribute> Attributes { get; }
        public IReadOnlyList<CustomHandlerRuleAttribute> Rules { get; }
        public Func<TContext, THandler> ConstructorFunc { get; }
    }
}
