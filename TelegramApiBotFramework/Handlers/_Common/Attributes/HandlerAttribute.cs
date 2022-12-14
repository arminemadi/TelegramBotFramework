using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types.Enums;
using TelegramBotFramework.Rules;

namespace TelegramBotFramework.Handlers._Common.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public abstract class HandlerAttribute : Attribute
    {
        protected HandlerAttribute(UpdateType type)
        {
            Type = type;
        }

        public UpdateType Type { get; }
        public bool AlwaysRun { get; protected set; }
        public int Priority { get; set; }
        public ChatType[]? ChatTypes { get; set; }

        protected bool HasValidChatType(ChatType? type)
        {
            if (ChatTypes == null)
                return true;
            if (type == null)
                return false;
            return ChatTypes.Any(Q => Q == type);
        }
    }
}