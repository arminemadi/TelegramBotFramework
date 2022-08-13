using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramBotFramework.Handlers._Common;
using TelegramBotFramework.Handlers.CallbackQueries;

namespace Debug.BotHandlers
{
    [CallbackQueryHandler("armin" , Equal = false , Segmented = true , SegmentSplit = " ")]
    public class ArminCallbackHandler : CallbackQueryHandler<HandlerContext>
    {
        public ArminCallbackHandler(HandlerContext context) : base(context)
        {
        }

        public override async Task<bool> Execute(CallbackQuery model)
        {
            try
            {
                await Context.Client.AnswerCallbackQueryAsync(model.Id, "Hiiiii");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return false;
        }
    }
}
