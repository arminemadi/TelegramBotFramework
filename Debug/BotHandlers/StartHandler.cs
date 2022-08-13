using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramBotFramework.Handlers._Common;
using TelegramBotFramework.Handlers.Messages;
using TelegramBotFramework.Rules;

namespace Debug.BotHandlers
{
    [MessageHandler("/start", Equal = true)]
    //[CustomHandlerRule("age", 20)]
    public class StartHandler : MessageHandler<HandlerContext>
    {
        public StartHandler(HandlerContext context) : base(context)
        {
        }

        public override async Task<bool> Execute(Message model)
        {
            await Context.Client.SendTextMessageAsync(model.From.Id, "Salam",
                replyMarkup: new InlineKeyboardMarkup(InlineKeyboardButton.WithCallbackData("Armin 1379 123123123")));
            return false;
        }
    }
}