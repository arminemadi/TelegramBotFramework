using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using TelegramBotFramework.Rules;

namespace TelegramBotFramework.Handlers.Messages;

public class MessageHandlerAttribute : HandlerAttribute
{
    public MessageHandlerAttribute(string text) : base(UpdateType.Message)
    {
        Text = text;
        AlwaysRun = false;
    }
    public MessageHandlerAttribute() : base(UpdateType.Message)
    {
        AlwaysRun = false;
    }
    public string? Text { get; }
    public bool StartWith { get; set; } = false;
    public bool EndWith { get; set; } = false;
    public bool Equal { get; set; } = true;
    public bool Normalize { get; set; } = true;
    public bool Segmented { get; set; } = false;
    public int? SegmentsCount { get; set; }
    public string? SegmentSplit { get; set; }

    public bool MustExecute(Message message , List<ICustomRule>? rules)
    {
        var customRulesResult = CheckCustomRules(rules);
        if (customRulesResult == false)
            return false;
        return true;
    }
}