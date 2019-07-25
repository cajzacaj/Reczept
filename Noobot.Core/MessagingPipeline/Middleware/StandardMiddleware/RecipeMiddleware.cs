using Noobot.Core.MessagingPipeline.Middleware.ValidHandles;
using Noobot.Core.MessagingPipeline.Request;
using Noobot.Core.MessagingPipeline.Response;
using ReczeptBot;
using System;
using System.Collections.Generic;

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
                    ValidHandles = new IValidHandle[]
                    {
                        new StartsWithHandle("recept")
                    },
                    Description = "Hämtar ett slumpat recept, eller ett recept med den tag du anger (recept <tag>).",
                    EvaluatorFunc = RecipeHandler
                }
            };
        }

        private IEnumerable<ResponseMessage> RecipeHandler(IncomingMessage message, IValidHandle matchedHandle)
        {
            App app = new App();
            Recipe recipe = new Recipe();
            DataAccess dataAccess = new DataAccess("Server=(localdb)\\mssqllocaldb; Database=Reczept");
            List<Tag> tagList = dataAccess.GetAllTags();
            var tempArray = message.TargetedText.Split(' ');
            bool success = false;
            if (tempArray.Length > 1)
            {
                foreach (Tag tag in tagList)
                {
                    if (tag.Name.ToLower() == tempArray[1])
                    {
                        var recipes = dataAccess.GetAllRecipesWithTag(tag);
                        recipe = app.GetRandomRecipeFromList(recipes);
                        yield return message.ReplyToChannel(recipe.Name);
                        yield return message.ReplyToChannel("Vill du laga receptet? Använd då kommandot 'ingredients'");
                        yield return message.ReplyToChannel("Om inte, skriv 'recipe' igen för att få ett nytt.");
                        success = true;
                        break;
                    }
                }
                if (!success)
                    yield return message.ReplyToChannel("Felaktig tag!");
            }
            else
            {
                recipe = app.GetRandomRecipe();
                User user = new User();
                user.MemberId = message.UserId;
                user.Name = message.Username;
                yield return message.ReplyToChannel(recipe.Name);
                yield return message.ReplyToChannel("Vill du laga receptet? Använd då kommandot 'ingredients'");
                yield return message.ReplyToChannel("Om inte, skriv 'recipe' igen för att få ett nytt.");
            }
        }
    }
}