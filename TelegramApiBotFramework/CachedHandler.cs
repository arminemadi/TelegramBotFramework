using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramBotFramework.Handlers;
using TelegramBotFramework.Handlers.Models;

namespace TelegramBotFramework
{
    public class CachedHandler<TAttribute ,TContext , THandler> where TContext : HandlerContext where  TAttribute : HandlerAttribute
    {
        public CachedHandler(TAttribute attribute, Func<TContext, THandler> constructorFunc)
        {
            Attribute = attribute;
            ConstructorFunc = constructorFunc;
        }

        public TAttribute Attribute { get; }
        public Func<TContext , THandler> ConstructorFunc { get; }
    }
}
