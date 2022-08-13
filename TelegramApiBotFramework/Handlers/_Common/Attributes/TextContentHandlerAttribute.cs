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
        protected TextContentHandlerAttribute(string content , UpdateType updateType) : base(updateType)
        {
            Content = content;
            AlwaysRun = false;
        }
        protected TextContentHandlerAttribute(UpdateType updateType) : base(updateType)
        {
            AlwaysRun = true;
        }
        public string? Content { get; }
        public bool StartWith { get; set; } = false;
        public bool EndWith { get; set; } = false;
        public bool Equal { get; set; } = true;
        public bool Normalize { get; set; } = true;
        public bool Segmented { get; set; } = false;
        public int? SegmentsCount { get; set; }
        public string? SegmentSplit { get; set; }

        public bool MustExecute(in string? content)
        {
            if (AlwaysRun)
                return true;

            var normalizedContent = GetMessage(content);
            if (string.IsNullOrWhiteSpace(normalizedContent))
                return false;
            if (string.IsNullOrWhiteSpace(Content))
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
            if (Normalize)
                return content.ToLower().Trim();
            return content;
        }

        private bool CheckEqual(in string content)
        {
            if (Equal == false)
                return false;
            return content == Content;
        }
        private bool CheckStartWith(in string content)
        {
            if (StartWith == false)
                return false;
            return Content.StartsWith(content);
        }
        private bool CheckEndWith(in string content)
        {
            if (EndWith == false)
                return false;
            return Content.EndsWith(content);
        }

        private bool CheckSegments(in string content)
        {
            if (Segmented == false)
                return false;
            if (SegmentSplit == null)
                throw new TelegramBotFrameworkException("Split segment cannot be null.");
            var split = SegmentsCount.HasValue
                ? content.Split(SegmentSplit, SegmentsCount.Value)
                : content.Split(SegmentSplit);
            if (split.Length == 0)
                return false;
            if (SegmentsCount.HasValue && split.Length != SegmentsCount.Value)
                return false;
            if (Content != null && split[0] != Content)
                return false;
            return true;
        }
    }
}
