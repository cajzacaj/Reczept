using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ReczeptBot
{
    public class DataAccess
    {
        private string conString;

        public DataAccess(string conString)
        {
            this.conString = conString;
        }

        public void Connect(string sql, Action<SqlCommand> action)
        {
            using (SqlConnection connection = new SqlConnection(conString))
            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                connection.Open();
                action(command);
            }
        }

        internal List<Recipe> GetAllRecipes()
        {
            var list = new List<Recipe>();

            Connect("SELECT [Id], [Name], [Description] FROM Recipe", (command) =>
            {
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var recipe = new Recipe
                    {
                        Id = reader.GetSqlInt32(0).Value,
                        Name = reader.GetSqlString(1).Value,
                        Description = reader.GetSqlString(2).Value
                    };
                    list.Add(recipe);
                }
            });
            return list;
        }

        public List<Recipe> GetAllRecipesWithTag(Tag tag)
        {
            var list = new List<Recipe>();

            Connect(@"SELECT Recipe.Id, Recipe.Name, Recipe.Description
                      FROM Recipe
                      JOIN TagsOnRecipe tor ON Recipe.Id = tor.RecipeId
                      JOIN Tag ON tor.TagId = Tag.Id
                      WHERE Tag.Id = @Id", (command) =>
            {
                command.Parameters.Add(new SqlParameter("Id", tag.Id));

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var recipe = new Recipe
                    {
                        Id = reader.GetSqlInt32(0).Value,
                        Name = reader.GetSqlString(1).Value,
                        Description = reader.GetSqlString(2).Value
                    };
                    list.Add(recipe);
                }
            });
            return list;
        }

        public void GetUserIdFromName(User user)
        {
            Connect(@"SELECT MemberId
                        FROM SlackUser
                        WHERE Name=@Name", (command) =>
            {
                command.Parameters.Add(new SqlParameter("Name", user.Name));
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    user.MemberId = reader.GetSqlString(0).Value;
                };
            });
        }

        public void GetTagId(Tag tag)
        {
            Connect(@"SELECT Id
                        FROM Tag
                        WHERE Name=@Name", (command) =>
            {
                command.Parameters.Add(new SqlParameter("Name", tag.Name));

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    tag.Id = reader.GetSqlInt32(0).Value;
                };
            });
        }

        public List<Tag> GetTagsForRecipe(Recipe recipe)
        {
            var list = new List<Tag>();

            Connect(@"SELECT t.id, t.Name FROM Tag t
                        JOIN TagsOnRecipe tor ON t.Id=tor.TagId
                        JOIN Recipe r ON tor.RecipeId = r.Id
                        WHERE r.Id=@Id", (command) =>
            {
                command.Parameters.Add(new SqlParameter("Id", recipe.Id));

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var tag = new Tag
                    {
                        Id = reader.GetSqlInt32(0).Value,
                        Name = reader.GetSqlString(1).Value,
                    };
                    list.Add(tag);
                }
            });
            return list;
        }

        public List<Tag> GetAllTags()
        {
            var list = new List<Tag>();

            Connect(@"SELECT t.id, t.Name FROM Tag t", (command) =>
            {
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var tag = new Tag
                    {
                        Id = reader.GetSqlInt32(0).Value,
                        Name = reader.GetSqlString(1).Value,
                    };
                    list.Add(tag);
                }
            });
            return list;
        }

        public List<Recipe> GetAllRecipesLikedByUser(User currentUser)
        {
            List<Recipe> list = new List<Recipe>();
            Connect(@"SELECT Recipe.Id, Recipe.Name, Recipe.Description FROM Recipe
                        JOIN UserHistory uh ON Recipe.Id=uh.RecipeId
                        JOIN SlackUser su ON uh.UserId=su.MemberId
                        WHERE su.MemberId=@Id And uh.UserLikesRecipe = 'true'", (command) =>
            {
                command.Parameters.Add(new SqlParameter("Id", currentUser.MemberId));
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var recipe = new Recipe
                    {
                        Id = reader.GetSqlInt32(0).Value,
                        Name = reader.GetSqlString(1).Value,
                        Description = reader.GetSqlString(2).Value
                    };
                    list.Add(recipe);
                }
            });
            return list;
        }

        internal List<Recipe> GetAllRecipesMainCourse()
        {
            var list = new List<Recipe>();

            Connect(@"SELECT Recipe.Id, Recipe.Name, Recipe.Description
                        FROM Recipe
                        JOIN TagsOnRecipe tor ON Recipe.Id = tor.RecipeId
                        JOIN Tag t ON tor.TagId = t.Id
                        WHERE t.Id = 11", (command) =>
            {
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var recipe = new Recipe
                    {
                        Id = reader.GetSqlInt32(0).Value,
                        Name = reader.GetSqlString(1).Value,
                        Description = reader.GetSqlString(2).Value
                    };
                    list.Add(recipe);
                }
            });
            return list;
        }

        internal List<Recipe> GetAllRecipesLikedByUserMainCourse(User currentUser)
        {
            List<Recipe> list = new List<Recipe>();
            Connect(@"SELECT Recipe.Id, Recipe.Name, Recipe.Description FROM Recipe
                        JOIN UserHistory uh ON Recipe.Id=uh.RecipeId
                        JOIN SlackUser su ON uh.UserId=su.MemberId
                        JOIN TagsOnRecipe tor ON Recipe.Id = tor.RecipeId
                        JOIN Tag t ON tor.TagId = t.Id
                        WHERE su.MemberId=@Id And uh.UserLikesRecipe = 'true' AND t.Id = 11", (command) =>
            {
                command.Parameters.Add(new SqlParameter("Id", currentUser.MemberId));
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var recipe = new Recipe
                    {
                        Id = reader.GetSqlInt32(0).Value,
                        Name = reader.GetSqlString(1).Value,
                        Description = reader.GetSqlString(2).Value
                    };
                    list.Add(recipe);
                }
            });
            return list;
        }

        public void AddToHistory(User currentUser, Recipe recipe)
        {
            Connect(@"INSERT INTO UserHistory(UserId, RecipeId, DateCooked) VALUES(@UserId, @RecipeId, @DateTime)", (command) =>
            {
                command.Parameters.Add(new SqlParameter("UserId", currentUser.MemberId));
                command.Parameters.Add(new SqlParameter("RecipeId", recipe.Id));
                command.Parameters.Add(new SqlParameter("DateTime", DateTime.Now));
                command.ExecuteNonQuery();
            });
        }

        public void AddIfLikedOrNot(User currentUser, bool likedRecipe)
        {
            Connect(@"UPDATE UserHistory set UserLikesRecipe = @Liked WHERE DateCooked = (select max(DateCooked) FROM UserHistory where UserId = @UserId)", (command) =>
            {
                if (likedRecipe)
                    command.Parameters.Add(new SqlParameter("Liked", true));
                else if (!likedRecipe)
                    command.Parameters.Add(new SqlParameter("Liked", false));
                command.Parameters.Add(new SqlParameter("UserId", currentUser.MemberId));
                command.ExecuteNonQuery();
            });
        }

        internal List<Ingredient> GetIngredientsInRecipe(Recipe recipe)
        {
            var list = new List<Ingredient>();

            Connect(@"SELECT i.id, i.Namn, rci.Quantity, u.Unit FROM Ingredient i
                        JOIN RecipeContainsIngredient rci ON i.id=rci.IngredientId
                        JOIN Recipe r ON rci.RecipeId = r.Id
                        JOIN Unit u ON rci.MeasurementUnit=u.Id
                        WHERE r.Id=@Id", (command) =>
            {
                command.Parameters.Add(new SqlParameter("Id", recipe.Id));

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var ingredient = new Ingredient
                    {
                        Id = reader.GetSqlInt32(0).Value,
                        Name = reader.GetSqlString(1).Value,
                        Quantity = reader.GetSqlDouble(2).Value,
                        Unit = reader.GetSqlString(3).Value,
                    };
                    list.Add(ingredient);
                }
            });
            return list;
        }

        public Recipe GetLastRecipe(User currentUser)
        {
            var recipe = new Recipe();
            var ingredient = new Ingredient();

            Connect(@"Select RecipeId, Recipe.Name, Recipe.Description from UserHistory
                    JOIN Recipe on Recipe.Id = RecipeId
                    WHERE DateCooked = (select max(DateCooked) FROM UserHistory where UserId = 'UJYGD2D1B')", (command) =>
            {
                command.Parameters.Add(new SqlParameter("UserId", currentUser.MemberId));
                command.ExecuteNonQuery();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    recipe.Id = reader.GetSqlInt32(0).Value;
                    recipe.Name = reader.GetSqlString(1).Value;
                    recipe.Description = reader.GetSqlString(2).Value;
                }
            });
            recipe.Ingredients = GetIngredientsInRecipe(recipe);
            return recipe;
        }

        public void CheckIfExistingUser(User user)
        {
            bool success = true;
            Connect(@"Select MemberId from SlackUser WHERE MemberId = @MemberID", (command) =>
            {
                command.Parameters.Add(new SqlParameter("MemberId", user.MemberId));
                command.ExecuteNonQuery();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    success = false;
                }
            });
            if (success == true)
            {
                Connect(@"Insert into SlackUser(MemberId, Name) values (@MemberId, @Name)", (command) =>
                {
                    command.Parameters.Add(new SqlParameter("MemberId", user.MemberId));
                    command.Parameters.Add(new SqlParameter("Name", user.Name));
                    command.ExecuteNonQuery();
                });
            }
        }

        internal void GetIngredientId(Ingredient ingredient)
        {
            Connect(@"SELECT Id
                        FROM Ingredient
                        WHERE Namn=@Name", (command) =>
            {
                command.Parameters.Add(new SqlParameter("Name", ingredient.Name));

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    ingredient.Id = reader.GetSqlInt32(0).Value;
                }
            });
        }

        internal List<Recipe> GetAllRecipesWithIngredient(Ingredient ingredient)
        {
            var list = new List<Recipe>();

            Connect(@"SELECT r.Id, r.Name, r.Description
                      FROM Recipe r
                      JOIN RecipeContainsIngredient rci ON r.Id = rci.RecipeId
                      JOIN Ingredient i ON rci.IngredientId = i.Id
                      WHERE i.Id = @Id", (command) =>
            {
                command.Parameters.Add(new SqlParameter("Id", ingredient.Id));

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var recipe = new Recipe
                    {
                        Id = reader.GetSqlInt32(0).Value,
                        Name = reader.GetSqlString(1).Value,
                        Description = reader.GetSqlString(2).Value,
                    };
                    list.Add(recipe);
                }
            });
            return list;
        }
    }
}