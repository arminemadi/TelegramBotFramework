using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Telegram.Bot;
using TelegramBotFramework.Handlers._Common;
using TelegramBotFramework.Rules;

namespace TelegramBotFramework.DependencyInjection
{
    public static class TelegramBotFrameworkDependency
    {
        public static void AddTelegramBotFramework<TContext>(this IServiceCollection collection)
            where TContext : HandlerContext
        {
            collection.AddScoped<ITelegramBotFrameworkService, TelegramBotFrameworkService<TContext>>();
            collection.AddScoped<TContext, TContext>();
        }

        public static void AddTelegramBotFramework<TContext, TRules>(this IServiceCollection collection)
            where TContext : HandlerContext where TRules : UserCustomRules
        {
            collection.AddTelegramBotFramework<TContext>();
            collection.AddScoped<UserCustomRules, TRules>();
        }

        public static void AddTelegramBotFramework<TContext>(this IServiceCollection collection, string token)
            where TContext : HandlerContext
        {
            collection.AddTelegramBotFramework<TContext>();
            collection.AddSingleton<ITelegramBotClient, TelegramBotClient>(provider => new TelegramBotClient(token));
        }

        public static void AddTelegramBotFramework<TContext, TRules>(this IServiceCollection collection, string token)
            where TContext : HandlerContext where TRules : UserCustomRules
        {
            collection.AddTelegramBotFramework<TContext, TRules>();
            collection.AddSingleton<ITelegramBotClient, TelegramBotClient>(provider => new TelegramBotClient(token));
        }
    }
}