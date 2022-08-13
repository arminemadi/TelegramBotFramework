using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using TelegramBotFramework.Exceptions;

namespace TelegramBotFramework.Handlers._Common.Attributes
{
    public abstract class TextContentHandlerAttribute : HandlerAttribute
    {
        protected TextContentHandlerAttribute(string attributeContent , UpdateType updateType) : base(updateType)
        {
            AttributeContent = attributeContent;
            AlwaysRun = false;
        }
        protected TextContentHandlerAttribute(UpdateType updateType) : base(updateType)
        {
            AlwaysRun = true;
        }
        public string? AttributeContent { get; }
        public bool StartWith { get; set; } = false;
        public bool EndWith { get; set; } = false;
        public bool Equal { get; set; } = true;
        public bool NormalizeRequest { get; set; } = true;
        public bool Segmented { get; set; } = false;
        public int SegmentsCount { get; set; } = -1;
        public string? SegmentSplit { get; set; }

        public bool MustExecute(in string? content)
        {
            if (AlwaysRun)
                return true;

            var normalizedContent = GetMessage(content);
            if (string.IsNullOrWhiteSpace(normalizedContent))
                return false;
            if (string.IsNullOrWhiteSpace(AttributeContent))
                return false;
            if (CheckEqual(normalizedContent))
                return true;
            if (CheckStartWith(normalizedContent))
                return true;
            if (CheckEndWith(normalizedContent))
                return true;
            if (CheckSegments(normalizedContent))
                return true;
            return false;
        }

        private string? GetMessage(in string? content)
        {
            if (content == null)
                return null;
            if (NormalizeRequest)
                return content.ToLower().Trim();
            return content;
        }

        private bool CheckEqual(in string content)
        {
            if (Equal == false)
                return false;
            return content == AttributeContent;
        }
        private bool CheckStartWith(in string content)
        {
            if (StartWith == false)
                return false;
            return content.StartsWith(AttributeContent);
        }
        private bool CheckEndWith(in string content)
        {
            if (EndWith == false)
                return false;
            return content.EndsWith(AttributeContent);
        }

        private bool CheckSegments(in string content)
        {
            if (Segmented == false)
                return false;
            if (SegmentSplit == null)
                throw new TelegramBotFrameworkException(ExceptionsMessages.SplitSegmentNull);
            var split = SegmentsCount != -1
                ? content.Split(SegmentSplit, SegmentsCount)
                : content.Split(SegmentSplit);
            if (split.Length == 0)
                return false;
            if (SegmentsCount != -1 && split.Length != SegmentsCount)
                return false;
            if (AttributeContent != null && split[0] != AttributeContent)
                return false;
            return true;
        }
    }
}
