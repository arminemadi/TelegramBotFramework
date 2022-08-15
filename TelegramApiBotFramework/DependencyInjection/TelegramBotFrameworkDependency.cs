using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Telegram.Bot;
using TelegramBotFramework.Handlers._Common;
using TelegramBotFramework.Handlers._Common.Builders;
using TelegramBotFramework.Handlers.CallbackQueries;
using TelegramBotFramework.Handlers.Messages;
using TelegramBotFramework.Rules;

namespace TelegramBotFramework.DependencyInjection
{
    public static class TelegramBotFrameworkDependency
    {
        public static void AddTelegramBotFramework(this IServiceCollection collection)
        {
            var builder = new HandlerBuilder();
            collection.AddScoped<ITelegramBotFramework, TelegramBotFramework>();
            builder.AddHandlers<MessageHandlerAttribute, MessageHandler>(collection);
            builder.AddHandlers<CallbackQueryHandlerAttribute, CallbackQueryHandler>(collection);
        }

        public static void AddTelegramBotFramework<TRules>(this IServiceCollection collection)
            where TRules : class , ICustomRules 
        {
            collection.AddScoped<ICustomRules, TRules>();
            AddTelegramBotFramework(collection);
        }

        public static void AddTelegramBotFramework(this IServiceCollection collection, string token)
        {
            AddTelegramBotFramework(collection);
            collection.AddSingleton<ITelegramBotClient, TelegramBotClient>(provider => new TelegramBotClient(token));
        }

        public static void AddTelegramBotFramework<TRules>(this IServiceCollection collection, string token) where TRules :class , ICustomRules
        {
            AddTelegramBotFramework<TRules>(collection);
            collection.AddSingleton<ITelegramBotClient, TelegramBotClient>(provider => new TelegramBotClient(token));
        }
    }
}