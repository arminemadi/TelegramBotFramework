using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBotFramework.Rules
{
    public abstract class UserCustomRules
    {
        public abstract IReadOnlyDictionary<string,ICustomRule> Rules { get; }


    }
}
