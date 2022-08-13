using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Telegram.Bot.Types;
using TelegramBotFramework.Handlers.Models;

namespace TelegramBotFramework
{
    public class TelegramBotFrameworkService<TContext> where TContext : HandlerContext
    {
        private readonly IServiceProvider _serviceProvider;

        public TelegramBotFrameworkService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task Parse(Update update)
        {
            await using var scope = _serviceProvider.CreateAsyncScope();
            var context = scope.ServiceProvider.GetService<TContext>();
        }
    }
}
