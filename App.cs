using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReczeptBot
{
    public class App
    {
        DataAccess _dataAccess = new DataAccess();
        Random r = new Random();
        Page _currentPage = Page.MainMenu;

        internal void Run()
        {
            while (true)
            {
                switch (_currentPage)
                {
                    case Page.MainMenu:
                        PageMainMenu();
                        break;
                    case Page.GetRecipe:
                        PageGetRecipe();
                        break;
                    case Page.EndProgram:
                        PageEndProgram();
                        return;
                }
            }

        }

        private void PageEndProgram()
        {
            Header("Avslutar");
            Console.WriteLine("Välkommen åter");
            Console.ReadKey();
        }

        private void PageGetRecipe()
        {
            Header("Hämta ett recept");

            Console.WriteLine("Välj ett alternativ");
            Console.WriteLine("a) Hämta ett slumpat recept");
            Console.WriteLine("b) Hämta ett recept med en tag");
            Console.WriteLine();

            ConsoleKey input = Console.ReadKey().Key;

            Recipe recipe = new Recipe();

            switch (input)
            {
                case ConsoleKey.A:
                    recipe = GetRandomRecipe();
                    PrintRecipe(recipe);
                    return;
                case ConsoleKey.B:
                    recipe = GetRandomRecipeWithTag();
                    PrintRecipe(recipe);
                    return;
                case ConsoleKey.C:
                    _currentPage = Page.MainMenu;
                    return;

            }
        }

        private Recipe GetRandomRecipeWithTag()
        {
            Console.Write("Vilken tag vill du använda? ");
            Console.ForegroundColor = ConsoleColor.DarkMagenta;

            Tag tag = new Tag
            {
                Name = Console.ReadLine()
            };

            Console.ResetColor();

            _dataAccess.GetTagId(tag);

            List<Recipe> recipes = _dataAccess.GetAllRecipesWithTag(tag);

            int randomIndex = r.Next(recipes.Count);

            return recipes[randomIndex];
        }

        internal void PageMainMenu()
        {
            Header("Välkommen till Reczipe!");

            Console.WriteLine("Välj ett alternativ");
            Console.WriteLine("a) Hämta ett recept");
            Console.WriteLine("c) Avsluta programmet");
            Console.WriteLine();

            ConsoleKey choice = Console.ReadKey().Key;

            switch(choice)
            {
                case ConsoleKey.A:
                    _currentPage = Page.GetRecipe;
                    break;
                case ConsoleKey.B:
                    _currentPage = Page.EndProgram;
                    break;
            }
        }

        private Recipe GetRandomRecipe()
        {
            List<Recipe> recipes = _dataAccess.GetAllRecipes();

            int randomIndex = r.Next(recipes.Count);

            return recipes[randomIndex];
        }
        private void PrintRecipe(Recipe recipe)
        {
            Header(recipe.Name);
        }

        void Header(string h = "")
        {
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.Green;

            Console.WriteLine();
            Console.WriteLine(h);
            Console.WriteLine();

            Console.ResetColor();
        }
    }
}
