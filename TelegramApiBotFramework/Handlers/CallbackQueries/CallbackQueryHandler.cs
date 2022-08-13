using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using TelegramBotFramework.Handlers._Common;

namespace TelegramBotFramework.Handlers.CallbackQueries
{
    public abstract class CallbackQueryHandler<TContext> : BaseHandler<TContext,CallbackQuery> where TContext : HandlerContext
    {
        protected CallbackQueryHandler(TContext context) : base(context)
        {
        }
    }
}
