using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramBotFramework._Common;

namespace TelegramBotFramework.Handlers._Common
{
    public abstract class BaseHandler<TExecute>
    {
        public abstract Task<ChainCommand> Execute(TExecute model);
    }
}
