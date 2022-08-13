using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBotFramework.Rules
{
    public class CustomRuleAttribute : Attribute
    {
        public string Name { get; }
        public object Value { get; }

        public CustomRuleAttribute(string name , object value)
        {
            Name = name.ToLower();
            Value = value;
        }
    }
}
