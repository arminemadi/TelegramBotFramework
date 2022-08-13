using Debug;
using Debug.BotHandlers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Telegram.Bot;
using TelegramBotFramework;
using TelegramBotFramework.DependencyInjection;

var collection = new ServiceCollection();
collection.AddTelegramBotFramework<BotContext, MyBotRules>("5493976201:AAFH1ztC4b6jZdhNLzO5M3NnJSd7hXpEaa8");
collection.AddLogging(opt =>
{
    opt.SetMinimumLevel(LogLevel.Trace);
    opt.AddConsole();
});
var provider = collection.BuildServiceProvider(true);

var client = provider.GetRequiredService<ITelegramBotClient>();
var offset = 0;
while (true)
{
    var updates = await client.GetUpdatesAsync(offset);
    foreach (var update in updates)
    {
        using var scope = provider.CreateScope();
        var framework = scope.ServiceProvider.GetService<TelegramBotFrameworkService<BotContext>>();
        await framework.Handle(update);
        offset = update.Id + 1;
    }
}