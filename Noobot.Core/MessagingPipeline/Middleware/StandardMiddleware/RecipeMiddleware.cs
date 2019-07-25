using Noobot.Core.MessagingPipeline.Middleware.ValidHandles;
using Noobot.Core.MessagingPipeline.Request;
using Noobot.Core.MessagingPipeline.Response;
using ReczeptBot;
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
                    Description = "Hämtar ett slumpat recept, eller ett slumpat recept med den tag du anger (recept <tag>).",
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
            User user = new User
            {
                MemberId = message.UserId
            };
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
                        yield return message.ReplyToChannel("Vill du laga receptet? Använd då kommandot 'ingredienser'");
                        yield return message.ReplyToChannel("Om inte, skriv 'recept' igen för att få ett nytt.");
                        success = true;
                        dataAccess.AddToHistory(user,recipe);
                        break;
                    }
                }
                if (!success)
                    yield return message.ReplyToChannel("Felaktig tag!");
            }
            else
            {
                recipe = app.GetRandomRecipe();
                yield return message.ReplyToChannel(recipe.Name);
                yield return message.ReplyToChannel("Vill du laga receptet? Använd då kommandot 'ingredienser'");
                yield return message.ReplyToChannel("Om inte, skriv 'recept' igen för att få ett nytt.");
            }
        }
    }
}