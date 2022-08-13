using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBotFramework.Rules
{
    public abstract class LazyCustomRule : ICustomRule
    {
        private object? _rule;

        protected LazyCustomRule(string name)
        {
            Name = name;
        }
        public string Name { get; }
        public async Task<object> GetValue(long userId)
        {
            return _rule ??= await Initialize();
        }

        protected abstract Task<object> Initialize();
    }
}
