using Telegram.Bot.Types;
using TelegramBotFramework.Handlers._Common;

namespace TelegramBotFramework
{
    public interface ITelegramBotFramework
    {
        Task Handle(Update update);
    }
}