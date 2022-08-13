using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using TelegramBotFramework.Handlers._Common.Attributes;

namespace TelegramBotFramework.Handlers.CallbackQueries
{
    public class CallbackQueryHandlerAttribute : TextContentHandlerAttribute
    {
        public CallbackQueryHandlerAttribute(string attributeContent) : base(attributeContent, UpdateType.CallbackQuery)
        {
        }

        public CallbackQueryHandlerAttribute() : base(UpdateType.CallbackQuery)
        {
        }

        public bool MustExecute(CallbackQuery query)
        {
            return MustExecute(query.Data);
        }
    }
}
