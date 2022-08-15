using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using TelegramBotFramework.Exceptions;
using TelegramBotFramework.Handlers;
using TelegramBotFramework.Handlers._Common;
using TelegramBotFramework.Handlers._Common.Attributes;
using TelegramBotFramework.Handlers._Common.Builders;
using TelegramBotFramework.Handlers.CallbackQueries;
using TelegramBotFramework.Handlers.Messages;
using TelegramBotFramework.Rules;

namespace TelegramBotFramework
{
    public class TelegramBotFrameworkService<TContext> : ITelegramBotFrameworkService where TContext : HandlerContext
    {
        private readonly TContext _context;
        private readonly ILogger<TelegramBotFrameworkService<TContext>> _logger;
        private readonly IReadOnlyDictionary<string, ICustomRule>? _rules;

        private static readonly List<ReadyHandler<MessageHandlerAttribute, TContext, MessageHandler<TContext>>>
            MessageHandlers;

        private static readonly
            List<ReadyHandler<CallbackQueryHandlerAttribute, TContext, CallbackQueryHandler<TContext>>>
            CallbackQueriesHandlers;

        static TelegramBotFrameworkService()
        {
            var builder = new HandlerFunctionBuilder<TContext>();
            MessageHandlers =
                builder.BuildConstructorFunctions<MessageHandlerAttribute, MessageHandler<TContext>>();
            CallbackQueriesHandlers =
                builder.BuildConstructorFunctions<CallbackQueryHandlerAttribute, CallbackQueryHandler<TContext>>();
        }

        public TelegramBotFrameworkService(TContext context,
            ILogger<TelegramBotFrameworkService<TContext>> logger,
            CustomRules? rules = null)
        {
            _context = context;
            _logger = logger;
            if (rules == null)
            {
                _rules = null;
                return;
            }

            _rules = rules.Rules;
        }

        public async Task Handle(Update update)
        {
            _logger.LogDebug("Handling update with type {type}", update.Type);
            if (update.Type == UpdateType.Message)
                await HandleMessage(update.Message ??
                                    throw new TelegramBotFrameworkException(ExceptionsMessages.NullUpdate,
                                        update.Type));
            else if (update.Type == UpdateType.CallbackQuery)
                await HandleCallbackQuery(update.CallbackQuery ??
                                          throw new TelegramBotFrameworkException(ExceptionsMessages.NullUpdate,
                                              update.Type));
            _logger.LogDebug("Handling update with type {type} finished.", update.Type);
        }

        private async Task HandleMessage(Message message)
        {
            var userId = message.From?.Id;
            if (userId == null)
            {
                _logger.LogCritical(ExceptionsMessages.NullUserIdInMessage);
                throw new TelegramBotFrameworkException(ExceptionsMessages.NullUserIdInMessage);
            }

            foreach (var messageHandler in MessageHandlers)
            {
                if (messageHandler.Attribute.MustExecute(message) == false)
                    continue;
                if (await CheckCustomRules(
                        messageHandler,
                        userId.Value,
                        message.Chat.Id) == false)
                    continue;
                var handler = messageHandler.ConstructorFunc(_context);
                var handlerResult = await handler.Execute(message);
                if (handlerResult)
                    return;
            }

            _logger.LogWarning(
                "Handlers never broke chain this could cause issues by adding new handlers. when handler is done with request must return true to finish chain.");
        }

        private async Task HandleCallbackQuery(CallbackQuery query)
        {
            var userId = query.From.Id;
            foreach (var queryHandler in CallbackQueriesHandlers)
            {
                if (queryHandler.Attribute.MustExecute(query) == false)
                    continue;
                if (await CheckCustomRules(
                        queryHandler,
                        userId,
                        query.Message?.Chat.Id) == false)
                    continue;
                var handler = queryHandler.ConstructorFunc(_context);
                var handlerResult = await handler.Execute(query);
                if (handlerResult)
                    return;
            }

            _logger.LogWarning(
                "Handlers never broke chain this could cause issues by adding new handlers. when handler is done with request must return true to finish chain.");
        }

        private async Task<bool> CheckCustomRules<THandler, TAttribute>(
            ReadyHandler<TAttribute, TContext, THandler> handler, long userId, long? chatId)
            where TAttribute : HandlerAttribute
        {
            if (handler.Rules.Count == 0)
                return true;
            if (_rules == null || handler.Rules.Count > _rules.Count)
            {
                _logger.LogCritical(ExceptionsMessages.InvalidCustomRulesConfiguration, handler.Name);
                throw new TelegramBotFrameworkException(ExceptionsMessages.InvalidCustomRulesConfiguration,
                    handler.Name);
            }

            foreach (var rule in handler.Rules)
            {
                if (_rules.TryGetValue(rule.Name, out var userRule))
                {
                    var value = await userRule.GetValue(userId, chatId);
                    if (value.Equals(rule.Value) == false)
                        return false;
                }
            }

            return true;
        }
    }
}