using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using TelegramBotFramework.Handlers.Models;

namespace Debug
{
    public class BotContext : HandlerContext
    {
        public BotContext(ITelegramBotClient client) : base(client)
        {
        }
    }
}
