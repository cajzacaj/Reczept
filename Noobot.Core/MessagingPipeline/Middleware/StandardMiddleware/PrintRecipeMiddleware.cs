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
                        new StartsWithHandle("ingredienser")
                    },
                    Description = "Skriver ut ingredienter samt fullt recept.",
                    EvaluatorFunc = PrintRecipeHandler
                }
            };
        }

        private IEnumerable<ResponseMessage> PrintRecipeHandler(IncomingMessage message, IValidHandle matchedHandle)
        {
            App app = new App();
            Recipe recipe = new Recipe();
            DataAccess dataAccess = new DataAccess("Server=(localdb)\\mssqllocaldb; Database=Reczept");
            User user = new User();
            user.MemberId = message.UserId;
            recipe = dataAccess.GetLastRecipe(user);
            app.PrintRecipeFull(recipe);
            yield return message.ReplyToChannel("Hoppas det smakar!");
            yield return message.ReplyToChannel("Skriv 'gillar' eller 'gillar inte' när du prövat receptet!");

        }
    }
}