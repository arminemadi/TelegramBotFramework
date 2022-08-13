using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types.Enums;

namespace TelegramBotFramework.Handlers
{
    public abstract class HandlerAttribute
    {
        protected HandlerAttribute(UpdateType type)
        {
            Type = type;
        }

        public UpdateType Type { get; }
        public bool AlwaysRun { get; protected set; }
        public IComparable[]? CustomRules { get; set; }
    }
}
