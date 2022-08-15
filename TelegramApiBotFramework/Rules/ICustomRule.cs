using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBotFramework.Rules
{
    public interface ICustomRule
    {
        string Name { get; }
        Task<object> GetValue(long userId , long? chatId);
    }
}
