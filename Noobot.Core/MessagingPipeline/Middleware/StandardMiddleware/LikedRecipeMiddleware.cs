using Noobot.Core.MessagingPipeline.Middleware.ValidHandles;
using Noobot.Core.MessagingPipeline.Request;
using Noobot.Core.MessagingPipeline.Response;
using ReczeptBot;
using System.Collections.Generic;

namespace Noobot.Core.MessagingPipeline.Middleware.StandardMiddleware
{
    internal class PrintRecipeMiddleware : MiddlewareBase
    {
        public PrintRecipeMiddleware(IMiddleware next) : base(next)
        {
            HandlerMappings = new[]
            {
                new HandlerMapping
                {
                    ValidHandles = new IValidHandle[]
                    {
                        new StartsWithHandle("gillar")
                    },
                    Description = "Anger att du gillar eller ogillar det förra slumpade receptet ('gillar' eller 'gillar inte') .",
                    EvaluatorFunc = LikedRecipeHandler
                }
            };
        }

        private IEnumerable<ResponseMessage> LikedRecipeHandler(IncomingMessage message, IValidHandle matchedHandle)
        {
            App app = new App();
            Recipe recipe = new Recipe();
            DataAccess dataAccess = new DataAccess("Server=(localdb)\\mssqllocaldb; Database=Reczept");
            User user = new User
            {
                MemberId = message.UserId
            };
            var tempArray = message.TargetedText.Split(' ');
            if (tempArray.Length > 1)
            {
                if (tempArray[1] == "inte")
                {
                    dataAccess.AddIfLikedOrNot(user,false);
                    yield return message.ReplyToChannel("Tack! Ditt val är sparat!");
                }
                else
                {
                    yield return message.ReplyToChannel("Jag förstod inte ditt svar. Skriv 'gillar' eller 'gillar inte' för att registrera din preferens!");
                }
            }
            else
            {
                dataAccess.AddIfLikedOrNot(user,true);
                yield return message.ReplyToChannel("Tack! Ditt val är sparat!");
            }
            
        }
    }
}