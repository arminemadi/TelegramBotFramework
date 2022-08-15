using Telegram.Bot.Types;
using TelegramBotFramework.Handlers._Common;

namespace TelegramBotFramework
{
    public interface ITelegramBotFrameworkService
    {
        Task Handle(Update update);
    }
}