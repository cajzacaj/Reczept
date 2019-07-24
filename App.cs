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
        Page _currentPage = Page.LoginScreen;
        User _currentUser = new User();

        internal void Run()
        {
            while (true)
            {
                switch (_currentPage)
                {
                    case Page.LoginScreen:
                        PageLoginScreen();
                        break;
                    case Page.MainMenu:
                        PageMainMenu();
                        break;
                    case Page.GetRecipe:
                        PageGetRecipe();
                        break;
                    case Page.GetRecipeList:
                        PageGetRecipeList();
                        break;
                    case Page.EndProgram:
                        PageEndProgram();
                        return;
                }
            }

        }

        private void PageGetRecipeList()
        {
            Header("H�mta en lista med recept");

            Console.WriteLine("V�lj ett alternativ");
            Console.WriteLine("a) H�mta alla recept");
            Console.WriteLine("b) H�mta alla recept med en angiven tag");
            Console.WriteLine("c) G� till huvudmenyn");
            Console.WriteLine();

            ConsoleKey input = Console.ReadKey(true).Key;

            List<Recipe> recipes = new List<Recipe>();

            switch (input)
            {
                case ConsoleKey.A:
                    recipes = GetAllRecipes();
                    PrintListOfRecipes(recipes);
                    return;
                case ConsoleKey.B:
                    recipes = GetAllRecipesWithTag();
                    PrintListOfRecipes(recipes);
                    return;
                case ConsoleKey.C:
                    _currentPage = Page.MainMenu;
                    return;

            }
        }

        private List<Recipe> GetAllRecipesWithTag()
        {
            Console.Write("Vilken tag vill du anv�nda? ");

            Tag tag = new Tag
            {
                Name = Console.ReadLine()
            };

            _dataAccess.GetTagId(tag);

            return _dataAccess.GetAllRecipesWithTag(tag);
        }

        private void PrintListOfRecipes(List<Recipe> recipes)
        {
            Header("Receptlista");

            Console.ForegroundColor = ConsoleColor.DarkGreen;

            foreach (Recipe recipe in recipes)
            {
                Console.WriteLine($"* {recipe.Name}");
            }

            Console.ResetColor();

            Console.ReadKey();
        }

        private List<Recipe> GetAllRecipes()
        {
            return _dataAccess.GetAllRecipes();
        }

        private void PageLoginScreen()
        {
            Header("V�lkommen till Reczept!");

            Console.Write("Vad heter du? ");

            User user = new User
            {
                Name = Console.ReadLine()
            };

            _dataAccess.GetUserIdFromName(user);

            _currentUser = user;

            _currentPage = Page.MainMenu;
        }

        private void PageEndProgram()
        {
            Header("Avslutar");
            Console.WriteLine("V�lkommen �ter");
            Console.ReadKey();
        }

        private void PageGetRecipe()
        {
            Header("H�mta ett recept");

            Console.WriteLine("V�lj ett alternativ");
            Console.WriteLine("a) H�mta ett slumpat recept");
            Console.WriteLine("b) H�mta ett recept med en tag");
            Console.WriteLine("c) G� till huvudmenyn");
            Console.WriteLine();

            ConsoleKey input = Console.ReadKey(true).Key;

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
            Console.Write("Vilken tag vill du anv�nda? ");
            Console.ForegroundColor = ConsoleColor.DarkMagenta;

            Tag tag = new Tag
            {
                Name = Console.ReadLine()
            };

            Console.ResetColor();

            _dataAccess.GetTagId(tag);

            List<Recipe> recipes = _dataAccess.GetAllRecipesWithTag(tag);

            return GetRandomRecipeFromList(recipes);
        }

        private Recipe GetRandomRecipeFromList(List<Recipe> list)
        {
            int randomIndex = r.Next(list.Count - 1);

            return list[randomIndex];
        }

        internal void PageMainMenu()
        {
            Header("V�lkommen till Reczipe!");

            Console.WriteLine("V�lj ett alternativ");
            Console.WriteLine("a) H�mta ett recept");
            Console.WriteLine("b) H�mta en lista med recept");
            Console.WriteLine("c) Avsluta programmet");
            Console.WriteLine();

            ConsoleKey choice = Console.ReadKey().Key;

            switch(choice)
            {
                case ConsoleKey.A:
                    _currentPage = Page.GetRecipe;
                    break;
                case ConsoleKey.B:
                    _currentPage = Page.GetRecipeList;
                    break;
                case ConsoleKey.C:
                    _currentPage = Page.EndProgram;
                    break;
            }
        }

        private Recipe GetRandomRecipe()
        {
            List<Recipe> recipes = _dataAccess.GetAllRecipes();

            return GetRandomRecipeFromList(recipes);
        }

        private void PrintRecipe(Recipe recipe)
        {
            Header(recipe.Name);

            PrintRecipeTags(recipe);

            Console.Write("Gillade du detta recept? (j/n): ");

            while(true)
            {
                ConsoleKey input = Console.ReadKey().Key;

                switch(input)
                {
                    case ConsoleKey.J:
                        _dataAccess.AddUserLikesRecipe(recipe, _currentUser);
                        return;
                    case ConsoleKey.N:
                        return;
                }

            }
        }

        private void PrintRecipeTags(Recipe recipe)
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            
            List<Tag> tags = _dataAccess.GetTagsForRecipe(recipe);

            foreach (Tag tag in tags)
            {
                Console.Write($"#{tag.Name}   ");
            }

            Console.WriteLine();

            Console.ResetColor();
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
