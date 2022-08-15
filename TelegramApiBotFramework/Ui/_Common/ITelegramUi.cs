using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;

namespace TelegramBotFramework.Ui._Common
{
    public interface ITelegramUi<TResult>
    {
        Task<TResult> Show(ITelegramBotClient client);
    }
    public interface ITelegramUi
    {
        Task Show(ITelegramBotClient client);
    }
}
