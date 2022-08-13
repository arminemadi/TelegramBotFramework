using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using TelegramBotFramework.Handlers.Models;

namespace TelegramBotFramework.Handlers.Messages
{
    public abstract class MessageHandler<TContext> : BaseHandler<TContext, Message> where TContext : HandlerContext
    {
        protected MessageHandler(TContext context) : base(context)
        {

        }
    }
}