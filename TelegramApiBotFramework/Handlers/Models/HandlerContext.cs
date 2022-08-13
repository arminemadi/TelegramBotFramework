using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;

namespace TelegramBotFramework.Handlers.Models
{
    public class HandlerContext
    {
        public HandlerContext(ITelegramBotClient client)
        {
            Client = client;
        }
        public ITelegramBotClient Client { get; }
    }
}
