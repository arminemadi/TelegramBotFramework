using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramBotFramework.Handlers.Models;

namespace TelegramBotFramework.Handlers
{
    public abstract class BaseHandler<TContext,TExecute> where TContext : HandlerContext
    {
        protected TContext Context { get; }

        protected BaseHandler(TContext context)
        {
            Context = context;
        }
        public abstract Task<bool> Execute(TExecute executeModel);
    }
}
