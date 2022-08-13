using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramBotFramework.Rules;

namespace Debug.BotHandlers
{
    public class MyBotRules : UserCustomRules
    {
        public MyBotRules()
        {
            Rules = new Dictionary<string, ICustomRule>()
            {
                { "age", new AgeCustomRule() }
            };
        }

        public override IReadOnlyDictionary<string, ICustomRule> Rules { get; }
    }
}
