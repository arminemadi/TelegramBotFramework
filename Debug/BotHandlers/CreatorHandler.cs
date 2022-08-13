using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramBotFramework.Handlers._Common;
using TelegramBotFramework.Handlers.Messages;

namespace Debug.BotHandlers
{
    [MessageHandler("/creator")]
    [MessageHandler("/about")]
    public class CreatorHandler : MessageHandler<HandlerContext>
    {
        public CreatorHandler(HandlerContext context) : base(context)
        {
        }

        public override async Task<bool> Execute(Message model)
        {
            await Context.Client.SendTextMessageAsync(model.From.Id, "@Armin");
            return true;
        }
    }
}
