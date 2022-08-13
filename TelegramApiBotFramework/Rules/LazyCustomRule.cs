using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBotFramework.Rules
{
    public abstract class LazyCustomRule : ICustomRule
    {
        private IComparable? _rule;

        protected LazyCustomRule(string name)
        {
            Name = name;
        }
        public string Name { get; }

        public IComparable Rule
        {
            get
            {
                if (_rule == null)
                    _rule = GetRule();
                return _rule;
            }
        }

        protected abstract IComparable GetRule();
    }
}
