using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Medallion;

namespace ReczeptBot
{
    public class App
    {
        DataAccess _dataAccess = new DataAccess("Server=(localdb)\\mssqllocaldb; Database=Reczept");
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
                    case Page.GetWeekMenu:
                        PageGetWeekMenu();
                        break;
                    case Page.EndProgram:
                        PageEndProgram();
                        return;
                }
            }
        }

        private void PageGetWeekMenu()
        {
            Header("Skapa en veckomeny");

            Console.WriteLine("V�lj ett alternativ");
            Console.WriteLine("a) Veckomeny baserad p� slumpade recept");
            Console.WriteLine("b) Veckomeny baserad p� en tag");
            Console.WriteLine("c) Veckomeny baserad p� recept som du gillar");
            Console.WriteLine("d) G� till huvudmenyn");
            Console.WriteLine();

            ConsoleKey input = Console.ReadKey(true).Key;

            List<Recipe> recipes;

            switch (input)
            {
                case ConsoleKey.A:
                    recipes = GetAllRecipesMainCourse();
                    PrintWeekMenu(recipes);
                    return;
                case ConsoleKey.B:
                    recipes = GetAllRecipesWithTag();
                    PrintWeekMenu(recipes);
                    return;
                case ConsoleKey.C:
                    recipes = GetAllRecipesLikedByUserMainCourse();
                    PrintWeekMenu(recipes);
                    return;
                case ConsoleKey.D:
                    _currentPage = Page.MainMenu;
                    return;

            }
        }

        private void PrintWeekMenu(List<Recipe> recipes)
        {
            Header("Veckomeny");

            Console.ForegroundColor = ConsoleColor.DarkGreen;

            if (recipes.Count < 7)
            {
                var takeOut = new Recipe
                {
                    Id = 0,
                    Name = "H�mtmat",
                    Description = "K�p mat p� din favorit-resturang. Eller bara den som �r n�rmast."
                };

                for (int i = recipes.Count; i < 7; i++)
                {
                    recipes.Add(takeOut);
                }
            }

            recipes.Shuffle();

            List<Recipe> WeekMenu = recipes.Take(7).ToList();

            Console.WriteLine($"  M�ndag: {WeekMenu[0].Name}");
            Console.WriteLine($"  Tisdag: {WeekMenu[1].Name}");
            Console.WriteLine($"  Onsdag: {WeekMenu[2].Name}");
            Console.WriteLine($" Torsdag: {WeekMenu[3].Name}");
            Console.WriteLine($"  Fredag: {WeekMenu[4].Name}");
            Console.WriteLine($"  L�rdag: {WeekMenu[5].Name}");
            Console.WriteLine($"  S�ndag: {WeekMenu[6].Name}");

            Console.ResetColor();

            Console.ReadKey();
        }

        public void PageGetRecipeList()
        {
            Header("H�mta en lista med recept");

            Console.WriteLine("V�lj ett alternativ");
            Console.WriteLine("a) H�mta alla recept");
            Console.WriteLine("b) H�mta alla recept med en angiven tag");
            Console.WriteLine("c) H�mta alla recept som du gillar");
            Console.WriteLine("d) H�mta alla recept som inneh�ller en s�rskild ingrediens");
            Console.WriteLine("e) G� till huvudmenyn");
            Console.WriteLine();

            ConsoleKey input = Console.ReadKey(true).Key;

            List<Recipe> recipes;

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
                    recipes = GetAllRecipesLikedByUser();
                    PrintListOfRecipes(recipes);
                    return;
                case ConsoleKey.D:
                    recipes = GetAllRecipesWithIngredient();
                    PrintListOfRecipes(recipes);
                    return;
                case ConsoleKey.E:
                    _currentPage = Page.MainMenu;
                    return;

            }
        }

        private List<Recipe> GetAllRecipesLikedByUser()
        {
            return _dataAccess.GetAllRecipesLikedByUser(_currentUser);
        }
        private List<Recipe> GetAllRecipesLikedByUserMainCourse()
        {
            return _dataAccess.GetAllRecipesLikedByUserMainCourse(_currentUser);
        }

       public List<Recipe> GetAllRecipesWithTag()
        {
            Console.Write("Vilken tag vill du anv�nda? ");

            Tag tag = new Tag
            {
                Name = Console.ReadLine()
            };

            _dataAccess.GetTagId(tag);

            return _dataAccess.GetAllRecipesWithTag(tag);
        }

        public void PrintListOfRecipes(List<Recipe> recipes)
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

        public List<Recipe> GetAllRecipes()
        {
            return _dataAccess.GetAllRecipes();
        }
        private List<Recipe> GetAllRecipesMainCourse()
        {
            return _dataAccess.GetAllRecipesMainCourse();
        }

        public void PageLoginScreen()
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

        public void PageEndProgram()
        {
            Header("Avslutar");
            Console.WriteLine("V�lkommen �ter");
            Console.ReadKey(true);
        }

        public void PageGetRecipe()
        {
            Header("H�mta ett recept");

            Console.WriteLine("V�lj ett alternativ");
            Console.WriteLine("a) H�mta ett slumpat recept");
            Console.WriteLine("b) H�mta ett recept med en tag");
            Console.WriteLine("c) H�mta ett recept som du gillar");
            Console.WriteLine("d) H�mta ett recept som inneh�ller en s�rskild ingrediens");
            Console.WriteLine("e) G� till huvudmenyn");
            Console.WriteLine();

            ConsoleKey input = Console.ReadKey(true).Key;

            Recipe recipe;

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
                    recipe = GetRandomRecipeFromLikedRecipes();
                    PrintRecipe(recipe);
                    return;
                case ConsoleKey.D:
                    recipe = GetRecipeContainingIngredient();
                    PrintRecipe(recipe);
                    return;
                case ConsoleKey.E:
                    _currentPage = Page.MainMenu;
                    return;


            }
        }
        private Recipe GetRandomRecipeFromLikedRecipes()
        {
            List<Recipe> recipes = _dataAccess.GetAllRecipesLikedByUser(_currentUser);

            return GetRandomRecipeFromList(recipes);
        }

        public Recipe GetRandomRecipeWithTag()
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

        public Recipe GetRandomRecipeFromList(List<Recipe> list)
        {
            int randomIndex = Rand.Next(0, list.Count - 1);

            return list[randomIndex];
        }

        internal void PageMainMenu()
        {
            Header("V�lkommen till Reczipe!");

            Console.WriteLine("V�lj ett alternativ");
            Console.WriteLine("a) H�mta ett recept");
            Console.WriteLine("b) H�mta en lista med recept");
            Console.WriteLine("c) Skapa en veckomeny");
            Console.WriteLine("d) Avsluta programmet");
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
                    _currentPage = Page.GetWeekMenu;
                    break;
                case ConsoleKey.D:
                    _currentPage = Page.EndProgram;
                    break;
            }
        }

        public Recipe GetRandomRecipe()
        {
            List<Recipe> recipes = _dataAccess.GetAllRecipes();

            return GetRandomRecipeFromList(recipes);
        }

        public Recipe GetRandomRecipe2()
        {
            List<Recipe> recipes = _dataAccess.GetAllRecipes();

            return GetRandomRecipeFromList(recipes);
        }
        public void PrintRecipe(Recipe recipe)
        {
            Header(recipe.Name);

            PrintRecipeTags(recipe);

            _dataAccess.AddToHistory(_currentUser, recipe);

            Console.Write("\nVill du laga det h�r receptet? (j/n): ");

            while(true)
            {
                ConsoleKey input = Console.ReadKey().Key;

                switch(input)
                {
                    case ConsoleKey.J:
                        PrintRecipeFull(recipe);
                        return;
                    case ConsoleKey.N:
                        _currentPage = Page.GetRecipe;
                        return;
                }

            }
        }

        public void PrintRecipeTags(Recipe recipe)
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
        public void PrintRecipeFull(Recipe recipe)
        {
            Header(recipe.Name);

            recipe.Ingredients = _dataAccess.GetIngredientsInRecipe(recipe);
            //Tempor�rl�sning f�r att kunna prova lite bara
            //recipe.Description = "H�r ska det st� hur du ska g�ra";
            //List<Ingredient> ingredients = new List<Ingredient>();

            //var ingredient1 = new Ingredient
            //{
            //    Id = 1,
            //    Name = "Salt",
            //    Quantity = 1,
            //    Unit = "tsk"
            //};
            //ingredients.Add(ingredient1);
            //var ingredient2 = new Ingredient
            //{
            //    Id = 2,
            //    Name = "K�rlek",
            //    Quantity = 100,
            //    Unit = "st"
            //};
            //ingredients.Add(ingredient2);

            Console.WriteLine("Ingredienser:");
            foreach (Ingredient ingredient in recipe.Ingredients)
            {
                Console.WriteLine($"{ingredient.Quantity, -4} {ingredient.Unit, -7} {ingredient.Name}");
            }
            Console.WriteLine("\nG�r s� h�r:");
            Console.WriteLine(recipe.Description);
            Console.WriteLine();
            PrintRecipeTags(recipe);


            Console.Write("\nGillade du detta recept? (j/n): ");

            while (true)
            {
                ConsoleKey input = Console.ReadKey().Key;

                switch (input)
                {
                    case ConsoleKey.J:
                        _dataAccess.AddIfLikedOrNot(_currentUser, true);
                        return;
                    case ConsoleKey.N:
                        _dataAccess.AddIfLikedOrNot(_currentUser, false);
                        return;
                }

            }
        }

        public Recipe GetRecipeContainingIngredient()
        {
            Console.Write("Vilken ingrediens vill du anv�nda? ");
            Console.ForegroundColor = ConsoleColor.DarkMagenta;

            Ingredient ingredient = new Ingredient
            {
                Name = Console.ReadLine()
            };

            Console.ResetColor();

            _dataAccess.GetIngredientId(ingredient);

            List<Recipe> recipes = _dataAccess.GetAllRecipesWithIngredient(ingredient);

            return GetRandomRecipeFromList(recipes);
        }
        public List<Recipe> GetAllRecipesWithIngredient()
        {
            Console.Write("Vilken ingrediens vill du anv�nda? ");
            Console.ForegroundColor = ConsoleColor.DarkMagenta;

            Ingredient ingredient = new Ingredient
            {
                Name = Console.ReadLine()
            };

            Console.ResetColor();

            _dataAccess.GetIngredientId(ingredient);

            return _dataAccess.GetAllRecipesWithIngredient(ingredient);
        }
    }
}
