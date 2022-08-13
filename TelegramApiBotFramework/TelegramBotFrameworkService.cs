using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using TelegramBotFramework.Handlers.Messages;
using TelegramBotFramework.Handlers.Models;
using TelegramBotFramework.Rules;

namespace TelegramBotFramework
{
    public class TelegramBotFrameworkService<TContext> where TContext : HandlerContext
    {
        private readonly TContext _context;
        private readonly List<ICustomRule>? _rules;

        private static readonly List<CachedHandler<MessageHandlerAttribute, TContext, MessageHandler<TContext>>>
            MessageHandlers;

        static TelegramBotFrameworkService()
        {
            var builder = new HandlerBuilder<TContext>();
            MessageHandlers = builder.BuildConstructors<MessageHandlerAttribute, MessageHandler<TContext>>();
        }

        public TelegramBotFrameworkService(TContext context, CustomRules? rules = null)
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
            foreach (var messageHandler in MessageHandlers)
            {
                if(messageHandler.Attribute.MustExecute(message , _rules) == false)
                    continue;
                var handler = messageHandler.ConstructorFunc(_context);
                var handlerResult = await handler.Execute(message);
                if(handlerResult)
                    return;
            }
        }
    }
}