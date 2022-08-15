using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramBotFramework.Handlers._Common.Attributes;
using TelegramBotFramework.Rules;

namespace TelegramBotFramework.Handlers._Common
{
    public class ReadyHandler<TAttribute>
    {
        public ReadyHandler(string name , Type type , TAttribute attribute , IReadOnlyList<CustomHandlerRuleAttribute> customRuleAttributes)
        {
            Name = name;
            Attribute = attribute;
            Type = type;
            Rules = customRuleAttributes;
        }

        public string Name { get; }
        public TAttribute Attribute { get; }
        public IReadOnlyList<CustomHandlerRuleAttribute> Rules { get; }
        public Type Type { get; }
    }
}
