using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using TelegramBotFramework.Handlers._Common.Attributes;
using TelegramBotFramework.Rules;

namespace TelegramBotFramework.Handlers.Messages;

public class MessageHandlerAttribute : TextContentHandlerAttribute
{
    public MessageHandlerAttribute(string content) : base(content, UpdateType.Message)
    {
    }

    public MessageHandlerAttribute() : base(UpdateType.Message)
    {
    }

    public bool MustExecute(Message message)
    {
        return MustExecute(message.Text);
    }



}