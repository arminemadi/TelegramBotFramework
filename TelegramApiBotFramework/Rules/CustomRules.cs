using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBotFramework.Rules
{
    public class CustomRules
    {
        public List<ICustomRule> Rules { get; }

        public CustomRules(List<ICustomRule> rules)
        {
            Rules = rules;
        }
    }
}
