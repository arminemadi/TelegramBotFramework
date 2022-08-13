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
        AlwaysRun = true;
    }
    public string? Text { get; }
    public bool StartWith { get; set; } = false;
    public bool EndWith { get; set; } = false;
    public bool Equal { get; set; } = true;
    public bool Normalize { get; set; } = true;
    public bool Segmented { get; set; } = false;
    public int? SegmentsCount { get; set; }
    public string? SegmentSplit { get; set; }

    public bool MustExecute(Message message)
    {
        if(AlwaysRun)
            return true;

        var messageText = GetMessage(message);
        if (messageText == null)
            return false;
        if (CheckEqual(messageText))
            return true;
        if (CheckStartWith(messageText))
            return true;
        if (CheckEndWith(messageText))
            return true;
        if (CheckSegments(messageText))
            return true;
        return false;
    }

    private string? GetMessage(Message message)
    {
        if (message.Text == null)
            return null;
        if (Normalize)
            return message.Text.ToLower().Trim();
        return message.Text;
    }

    private bool CheckEqual(in string message)
    {
        if (Equal == false)
            return false;
        return message == Text;
    }
    private bool CheckStartWith(in string message)
    {
        if (StartWith == false)
            return false;
        return Text.StartsWith(message);
    }
    private bool CheckEndWith(in string message)
    {
        if (EndWith == false)
            return false;
        return Text.EndsWith(message);
    }

    private bool CheckSegments(in string message)
    {
        if (Segmented == false)
            return false;
        if (SegmentSplit == null)
            throw new Exception("Split segment cannot be null.");
        var split = SegmentsCount.HasValue
            ? message.Split(SegmentSplit, SegmentsCount.Value)
            : message.Split(SegmentSplit);
        if (split.Length == 0)
            return false;
        if (SegmentsCount.HasValue && split.Length != SegmentsCount.Value)
            return false;
        if (Text != null && split[0] != Text)
            return false;
        return true;
    }
}