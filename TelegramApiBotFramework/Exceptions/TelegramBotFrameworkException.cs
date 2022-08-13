using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBotFramework.Exceptions
{
    public class TelegramBotFrameworkException : Exception
    {
        public TelegramBotFrameworkException(string message) : base(message)
        {
        }

        public TelegramBotFrameworkException(string message, params object[] parameters) : base(string.Format(message,
            parameters))
        {
        }
    }
}