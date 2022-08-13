using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using TelegramBotFramework.Handlers;
using TelegramBotFramework.Handlers.Messages;
using TelegramBotFramework.Handlers.Models;
using TelegramBotFramework.Rules;

namespace TelegramBotFramework
{
    public class TelegramBotFrameworkService<TContext> where TContext : HandlerContext
    {
        private readonly TContext _context;
        private readonly IReadOnlyDictionary<string,ICustomRule>? _rules;

        private static readonly List<CachedHandler<MessageHandlerAttribute, TContext, MessageHandler<TContext>>>
            MessageHandlers;

        static TelegramBotFrameworkService()
        {
            var builder = new HandlerBuilder<TContext>();
            MessageHandlers = builder.BuildConstructors<MessageHandlerAttribute, MessageHandler<TContext>>();
        }

        public TelegramBotFrameworkService(TContext context, UserCustomRules? rules = null)
        {
            _context = context;
            if (rules == null)
            {
                _rules = null;
                return;
            }

            _rules = rules.Rules;
        }

        public async Task Parse(Update update)
        {
            if (update.Type == UpdateType.Message)
                await HandleMessage(update.Message ?? throw new NullReferenceException());
        }

        private async Task HandleMessage(Message message)
        {
            var userId = message.From?.Id;
            if (userId == null)
                throw new Exception("User id is null.");
            foreach (var messageHandler in MessageHandlers)
            {
                if(messageHandler.Attribute.MustExecute(message) == false)
                    continue;
                if(await CheckCustomRules(messageHandler.Rules , userId.Value) == false)
                    continue;
                var handler = messageHandler.ConstructorFunc(_context);
                var handlerResult = await handler.Execute(message);
                if(handlerResult)
                    return;
            }
        }

        private async Task<bool> CheckCustomRules(IReadOnlyList<CustomRuleAttribute> handlerRules ,long userId)
        {
            if (handlerRules.Count == 0)
                return true;
            if (_rules == null || handlerRules.Count > _rules.Count)
                throw new Exception("Invalid rules configuration.");
            foreach (var rule in handlerRules)
            {
                if (_rules.TryGetValue(rule.Name, out var userRule))
                {
                    var value = await userRule.GetValue(userId);
                    if (value.Equals(rule.Value) == false)
                        return false;
                }
            }
            return true;
        }
    }
}