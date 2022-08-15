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
    public class TelegramBotFrameworkService : ITelegramBotFrameworkService
    {
        private readonly IServiceProvider _provider;
        private readonly ILogger<TelegramBotFrameworkService> _logger;
        private IReadOnlyDictionary<string, ICustomRule>? _rules;

        private static readonly List<ReadyHandler<MessageHandlerAttribute>>
            MessageHandlers;

        private static readonly
            List<ReadyHandler<CallbackQueryHandlerAttribute>>
            CallbackQueriesHandlers;

        static TelegramBotFrameworkService()
        {
            var builder = new HandlerBuilder();
            MessageHandlers =
                builder.GetRules<MessageHandlerAttribute, MessageHandler>();
            CallbackQueriesHandlers =
                builder.GetRules<CallbackQueryHandlerAttribute, CallbackQueryHandler>();
        }

        public TelegramBotFrameworkService(IServiceProvider provider,
            ILogger<TelegramBotFrameworkService> logger)
        {
            _provider = provider;
            _logger = logger;

        }

        public async Task Handle(Update update)
        {
            await using var scope = _provider.CreateAsyncScope();
            var customRules = scope.ServiceProvider.GetService<CustomRules>();
            _rules = customRules?.Rules;


            _logger.LogDebug("Handling update with type {type}", update.Type);
            if (update.Type == UpdateType.Message)
                await HandleMessage(update.Message ??
                                    throw new TelegramBotFrameworkException(ExceptionsMessages.NullUpdate,
                                        update.Type),scope.ServiceProvider);
            else if (update.Type == UpdateType.CallbackQuery)
                await HandleCallbackQuery(update.CallbackQuery ??
                                          throw new TelegramBotFrameworkException(ExceptionsMessages.NullUpdate,
                                              update.Type) , scope.ServiceProvider);
            _logger.LogDebug("Handling update with type {type} finished.", update.Type);
        }

        private async Task HandleMessage(Message message , IServiceProvider scopeProvider)
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
                var handler = (MessageHandler)scopeProvider.GetRequiredService(messageHandler.Type);
                var handlerResult = await handler.Execute(message);
                if (handlerResult)
                    return;
            }

            _logger.LogWarning(
                "Handlers never broke chain this could cause issues by adding new handlers. when handler is done with request must return true to finish chain.");
        }

        private async Task HandleCallbackQuery(CallbackQuery query , IServiceProvider scopeProvider)
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
                var handler = (CallbackQueryHandler)scopeProvider.GetRequiredService(queryHandler.Type);
                var handlerResult = await handler.Execute(query);
                if (handlerResult)
                    return;
            }

            _logger.LogWarning(
                "Handlers never broke chain this could cause issues by adding new handlers. when handler is done with request must return true to finish chain.");
        }

        private async Task<bool> CheckCustomRules<TAttribute>(
            ReadyHandler<TAttribute> handler, long userId, long? chatId)
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