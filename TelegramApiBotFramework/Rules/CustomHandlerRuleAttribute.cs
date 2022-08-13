using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBotFramework.Rules
{
    public class CustomHandlerRuleAttribute : Attribute
    {
        public string Name { get; }
        public object Value { get; }

        public CustomHandlerRuleAttribute(string name , object value)
        {
            Name = name.ToLower();
            Value = value;
        }
    }
}
