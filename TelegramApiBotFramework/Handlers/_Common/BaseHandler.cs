using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBotFramework.Handlers._Common
{
    public abstract class BaseHandler<TExecute>
    {
        public abstract Task<bool> Execute(TExecute model);
    }
}
