using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBotFramework.Rules
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class CustomHandlerRuleAttribute : Attribute
    {
        public string Name { get; }
        public object[] Values { get; }

        public CustomHandlerRuleAttribute(string name ,object[] values)
        {
            Name = name.ToLower();
            Values = values;
        }
        public CustomHandlerRuleAttribute(string name, object value)
        {
            Name = name.ToLower();
            Values = new []{value};
        }
    }
}
