using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBotFramework.Exceptions
{
    public static class ExceptionsMessages
    {
        public const string NullUserIdInMessage = "Unable to extract user id from message.";
        public const string InvalidCustomRulesConfiguration = "Invalid rules configuration in {0} handler.";
        public const string SplitSegmentNull = "Split segment cannot be null.";
        public const string HandlerInvalidConstructor = "{0} does not have suitable constructor.";
        public const string FailToCastAttribute = "Unable to cast into handle attribute.";
    }
}
