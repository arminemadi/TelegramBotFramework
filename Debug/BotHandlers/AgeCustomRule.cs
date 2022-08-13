using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramBotFramework.Rules;

namespace Debug.BotHandlers
{
    internal class AgeCustomRule : LazyCustomRule
    {
        public AgeCustomRule() : base("age")
        {
        }

        protected override Task<object> Initialize()
        {
            return Task.FromResult<object>(20);
        }
    }
}
