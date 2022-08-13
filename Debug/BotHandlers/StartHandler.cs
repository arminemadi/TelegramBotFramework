using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramBotFramework.Handlers.Messages;
using TelegramBotFramework.Rules;

namespace Debug.BotHandlers
{
    [MessageHandler("/start" , Equal = true , Normalize = true)]
    [CustomRule("age" , 20)]
    public class StartHandler : MessageHandler<BotContext>
    {
        public StartHandler(BotContext context) : base(context)
        {
        }

        public override async Task<bool> Execute(Message executeModel)
        {
            await Context.Client.SendTextMessageAsync(executeModel.From.Id, "Salam");
            return true;
        }
    }
}
