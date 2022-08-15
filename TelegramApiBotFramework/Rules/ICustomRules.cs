namespace TelegramBotFramework.Rules;

public interface ICustomRules
{
    IReadOnlyDictionary<string, ICustomRule> Rules { get; }

}