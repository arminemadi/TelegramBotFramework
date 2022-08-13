using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using TelegramBotFramework.Handlers.Models;
using TelegramBotFramework.Rules;

namespace TelegramBotFramework
{
    public static class TelegramBotFrameworkDependency
    {
        public static void AddTelegramBotFramework<TContext>(this IServiceCollection collection) where TContext : HandlerContext
        {
            collection.AddScoped<TelegramBotFrameworkService<TContext>, TelegramBotFrameworkService<TContext>>();
            collection.AddScoped<TContext, TContext>();
        }
        public static void AddTelegramBotFramework<TContext , TRules>(this IServiceCollection collection) where TContext : HandlerContext where TRules : UserCustomRules
        {
            AddTelegramBotFramework<TContext>(collection);
            collection.AddScoped<UserCustomRules, TRules>();
        }
    }
}
