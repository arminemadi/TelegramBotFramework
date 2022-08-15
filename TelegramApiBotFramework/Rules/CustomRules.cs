using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBotFramework.Rules
{
    public class CustomRules : ICustomRules
    {
        public CustomRules(params ICustomRule[] rules)
        {
            Rules = rules.ToDictionary(Q => Q.Name);
        }
        public CustomRules(List<ICustomRule> rules)
        {
            Rules = rules.ToDictionary(Q => Q.Name);
        }
        public IReadOnlyDictionary<string, ICustomRule> Rules { get; }
    }

    public interface ICustomRules
    {
        IReadOnlyDictionary<string, ICustomRule> Rules { get; }

    }
}
