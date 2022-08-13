using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramBotFramework.Handlers.Models;
using TelegramBotFramework.Rules;

namespace TelegramBotFramework.Handlers
{
    public class CachedHandler<TAttribute, TContext, THandler> where TContext : HandlerContext where TAttribute : HandlerAttribute
    {
        public CachedHandler(TAttribute attribute, Func<TContext, THandler> constructorFunc , IReadOnlyList<CustomRuleAttribute> customRuleAttributes)
        {
            Attribute = attribute;
            ConstructorFunc = constructorFunc;
            Rules = customRuleAttributes;
        }

        public TAttribute Attribute { get; }
        public IReadOnlyList<CustomRuleAttribute> Rules { get; }
        public Func<TContext, THandler> ConstructorFunc { get; }
    }
}
