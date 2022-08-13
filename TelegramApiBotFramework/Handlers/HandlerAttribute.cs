using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types.Enums;
using TelegramBotFramework.Rules;

namespace TelegramBotFramework.Handlers
{
    public abstract class HandlerAttribute : Attribute
    {
        protected HandlerAttribute(UpdateType type)
        {
            Type = type;
        }

        public UpdateType Type { get; }
        public bool AlwaysRun { get; protected set; }
        public Dictionary<string, IComparable> CustomRules { get; set; } = new();
        public int Priority { get; set; }

        protected bool CheckCustomRules(List<ICustomRule>? rules)
        {
            if (CustomRules.Count == 0)
               return true;
            if (rules == null || CustomRules.Count > rules.Count)
                throw new Exception("Invalid rules configuration.");
            foreach (var rule in rules)
            {
                if (CustomRules.TryGetValue(rule.Name, out var handlerRule))
                {
                    if (handlerRule.CompareTo(rule) != 0)
                        return false;
                }
            }
            return true;
        }
    }
}