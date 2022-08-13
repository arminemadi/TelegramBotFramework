using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types.Enums;
using TelegramBotFramework.Rules;

namespace TelegramBotFramework.Handlers
{
    public abstract class HandlerAttribute : Attribute
    {
        protected HandlerAttribute(UpdateType type)
        {
            Type = type;
        }

        public UpdateType Type { get; }
        public bool AlwaysRun { get; protected set; }
        public int Priority { get; set; }

    }
}