using System;
using System.Collections.Generic;
using Noobot.Core.MessagingPipeline.Middleware.ValidHandles;
using Noobot.Core.MessagingPipeline.Request;
using Noobot.Core.MessagingPipeline.Response;
using ReczeptBot;

namespace Noobot.Core.MessagingPipeline.Middleware.StandardMiddleware
{
    internal class RecipeMiddleware : MiddlewareBase
    {
        public RecipeMiddleware(IMiddleware next) : base(next)
        {
            HandlerMappings = new[]
            {
                new HandlerMapping
                {
                    ValidHandles = ExactMatchHandle.For("recipe"),
                    Description = "Will eventually fetch a recipe!",
                    EvaluatorFunc = RecipeHandler
                    
                }
            };
        }

        private IEnumerable<ResponseMessage> RecipeHandler(IncomingMessage message, IValidHandle matchedHandle)
        {
            App app = new App();
            Recipe recipe = new Recipe();
            recipe = app.GetRandomRecipe();
            User user = new User();
            user.MemberId = message.UserId;
            user.Name = message.Username;
            DataAccess dataAccess = new DataAccess();
            yield return message.ReplyToChannel(recipe.Name);
            yield return message.ReplyToChannel("Vill du laga receptet? Använd då kommandot 'ingredients'");
            yield return message.ReplyToChannel("Om inte, skriv 'recipe' igen för att få ett nytt.");
            dataAccess.AddUserLikesRecipe(recipe,user);
        }
    }
}
