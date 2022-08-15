using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramBotFramework.Ui._Common;

namespace TelegramBotFramework.Ui
{
    public record SendMessageUi : ITelegramUi<Message>
    {
        public Task<Message> Show(ITelegramBotClient client)
        {
            throw new NotImplementedException();
        }
    }
}
