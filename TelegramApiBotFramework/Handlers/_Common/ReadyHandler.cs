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
        public ReadyHandler(string name ,TAttribute attribute, Func<TContext, THandler> constructorFunc, IReadOnlyList<CustomRuleAttribute> customRuleAttributes)
        {
            Name = name;
            Attribute = attribute;
            ConstructorFunc = constructorFunc;
            Rules = customRuleAttributes;
        }

        public string Name { get; }
        public TAttribute Attribute { get; }
        public IReadOnlyList<CustomRuleAttribute> Rules { get; }
        public Func<TContext, THandler> ConstructorFunc { get; }
    }
}
