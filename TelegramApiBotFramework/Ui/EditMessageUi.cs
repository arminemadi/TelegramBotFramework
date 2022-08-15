using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramBotFramework.Ui._Common;

namespace TelegramBotFramework.Ui;

public record EditMessageUi : ITelegramUi<Message>
{
    public Task<Message> Show(ITelegramBotClient client)
    {
        throw new NotImplementedException();
    }
}