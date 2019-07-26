using Newtonsoft.Json;
using Noobot.Core.MessagingPipeline.Middleware.ValidHandles;
using Noobot.Core.MessagingPipeline.Request;
using Noobot.Core.MessagingPipeline.Response;
using ReczeptBot;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

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
                    Description = "Hämtar ett slumpat recept, en slumpad veckomeny (recept veckomeny) eller ett slumpat recept med den tag du anger (recept <tag>).",
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
            Ingredient ingredient = new Ingredient();
            List<Recipe> recipes = new List<Recipe>();
            Dictionary<string, Dictionary<DayOfWeek, Recipe>> weeklyMenu = new Dictionary<string, Dictionary<DayOfWeek, Recipe>>();
            User user = new User
            {
                MemberId = message.UserId
            };
            var tempArray = message.TargetedText.Split(' ');
            bool success = false;
            bool weekmenu = false;
            if (tempArray.Length > 1)
            {
                if (tempArray[1] == "gillar")
                {
                    recipes = dataAccess.GetAllRecipesLikedByUser(user);
                    recipe = app.GetRandomRecipeFromList(recipes);
                    success = true;
                }
                else if (tempArray[1] == "veckomeny")
                {
                    recipes = dataAccess.GetAllRecipes();
                    recipes = app.PrintWeekMenuSlack(recipes);
                    int i = 0;
                    var tempDict = new Dictionary<DayOfWeek, Recipe>();
                    foreach (Recipe r in recipes)
                    {
                        tempDict.Add((DayOfWeek)i, r);
                        i++;
                    }
                    weeklyMenu.Add(user.MemberId, tempDict);
                    string json = JsonConvert.SerializeObject(weeklyMenu);
                    File.WriteAllText(@"Weeklymenu.json", json);
                    success = true;
                    weekmenu = true;
                }
                else if (tempArray[1] == "dagens")
                {
                    string json = File.ReadAllText(@"Weeklymenu.json");
                    var tempDict = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<DayOfWeek, Recipe>>>(json);
                    var currentDay = DateTime.Today.DayOfWeek;
                    if (tempDict.ContainsKey(user.MemberId))
                    {
                        recipe = tempDict[user.MemberId][currentDay];
                        success = true;
                    }
                }
                else if (tempArray[1] == "ingrediens" && tempArray.Length > 2)
                {
                    ingredient.Name = tempArray[2];
                    dataAccess.GetIngredientId(ingredient);
                    if (ingredient.Id != 0)
                    {
                        recipes = dataAccess.GetAllRecipesWithIngredient(ingredient);
                        recipe = app.GetRandomRecipeFromList(recipes);

                        success = true;
                    }
                }
                else
                {
                    foreach (Tag tag in tagList)
                    {
                        if (tag.Name.ToLower() == tempArray[1])
                        {
                            recipes = dataAccess.GetAllRecipesWithTag(tag);
                            recipe = app.GetRandomRecipeFromList(recipes);
                            success = true;
                            dataAccess.AddToHistory(user, recipe);
                            break;
                        }
                    }
                }
            }
            else
            {
                recipe = app.GetRandomRecipe();
                dataAccess.AddToHistory(user, recipe);
                success = true;
            }
            if (!success)
                yield return message.ReplyToChannel($"<@{message.UserId}>Felaktig tag eller ingrediens!");
            else if (weekmenu)
            {
                var culture = new System.Globalization.CultureInfo("sv-SE");
                StringBuilder builder = new StringBuilder();
                builder.Append($"Veckomeny för <@{message.UserId}>:\n```");
                int j = 1;
                for (int i = 0; i < recipes.Count; i++)
                {
                    builder.Append($"{(culture.DateTimeFormat.GetDayName((DayOfWeek)j)).ToString().PadRight(7)}: {recipes[j].Name}\n");
                    j++;
                    if (j == 7)
                        j = 0;
                }
                {
                }
                builder.Append("```");
                yield return message.ReplyToChannel(builder.ToString());
            }
            else
            {
                yield return message.ReplyToChannel($"<@{message.UserId}> du fick {recipe.Name}, vill du laga receptet? \nAnvänd då kommandot 'ingredienser'\nOm inte, skriv 'recept' igen för att få ett nytt.");
            }
        }
    }
}