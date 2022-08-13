using Debug;
using Debug.BotHandlers;
using Microsoft.Extensions.DependencyInjection;
using Telegram.Bot;
using TelegramBotFramework;



var collection = new ServiceCollection();
collection.AddTelegramBotFramework<BotContext,MyBotRules>();
collection.AddSingleton<ITelegramBotClient, TelegramBotClient>(Q => 
    new TelegramBotClient("5493976201:AAFH1ztC4b6jZdhNLzO5M3NnJSd7hXpEaa8"));
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
        await framework.Parse(update);
        offset = update.Id + 1;
    }
}
