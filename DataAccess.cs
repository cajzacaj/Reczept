using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ReczeptBot
{
    internal class DataAccess
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

            Connect("SELECT [Id], [Name] FROM Recipe", (command) =>
            {
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var recipe = new Recipe
                    {
                        Id = reader.GetSqlInt32(0).Value,
                        Name = reader.GetSqlString(1).Value,
                    };
                    list.Add(recipe);
                }
            });
            return list;
        }

        internal List<Recipe> GetAllRecipesWithTag(Tag tag)
        {
            var list = new List<Recipe>();

            Connect(@"SELECT Recipe.Id, Recipe.Name
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
                    };
                    list.Add(recipe);
                }
            });
            return list;
        }

        internal void GetUserIdFromName(User user)
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

        internal void GetTagId(Tag tag)
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

        internal List<Tag> GetTagsForRecipe(Recipe recipe)
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

        internal void AddUserLikesRecipe(Recipe recipe, User currentUser) //Behöver göras om helt framöver
        {
            if (!UserLikesRecipe(recipe, currentUser))
            {
                var sql = "INSERT INTO UserLikesRecipe(UserId, RecipeId) VALUES(@UserId, @RecipeId)";

                using (SqlConnection connection = new SqlConnection(conString))
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    connection.Open();
                    command.Parameters.Add(new SqlParameter("UserId", currentUser.MemberId));
                    command.Parameters.Add(new SqlParameter("RecipeId", recipe.Id));

                    command.ExecuteNonQuery();
                }
            }
        }

        internal List<Recipe> GetAllRecipesLikedByUser(User currentUser) //Behöver göras om helt framöver
        {
            var sql = @"SELECT Recipe.Id, Recipe.Name
                        FROM Recipe
                        JOIN UserLikesRecipe ulr ON Recipe.Id=ulr.RecipeId
                        JOIN SlackUser su ON ulr.UserId=su.MemberId
                        WHERE su.MemberId=@Id";

            using (SqlConnection connection = new SqlConnection(conString))
            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                connection.Open();
                command.Parameters.Add(new SqlParameter("Id", currentUser.MemberId));

                SqlDataReader reader = command.ExecuteReader();

                var list = new List<Recipe>();

                while (reader.Read())
                {
                    var recipe = new Recipe
                    {
                        Id = reader.GetSqlInt32(0).Value,
                        Name = reader.GetSqlString(1).Value,
                    };
                    list.Add(recipe);
                }

                return list;
            }
        }

        internal List<Recipe> GetAllRecipesMainCourse()
        {
            var list = new List<Recipe>();

            Connect(@"SELECT Recipe.Id, Recipe.Name
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
                    };
                    list.Add(recipe);
                }
            });
            return list;
        }

        internal bool UserLikesRecipe(Recipe recipe, User currentUser) //Behöver göras om helt framöver
        {
            var sql = @"SELECT RecipeId
                        FROM UserLikesRecipe
                        WHERE RecipeId=@RecipeId AND UserId=@UserId";

            using (SqlConnection connection = new SqlConnection(conString))
            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                connection.Open();
                command.Parameters.Add(new SqlParameter("RecipeId", recipe.Id));
                command.Parameters.Add(new SqlParameter("UserId", currentUser.MemberId));

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                    return true;
                else
                    return false;
            }
        }

        internal List<Recipe> GetAllRecipesLikedByUserMainCourse(User currentUser) //Behöver göras om helt framöver
        {
            var sql = @"SELECT Recipe.Id, Recipe.Name
                            FROM Recipe
                            JOIN UserLikesRecipe ulr ON Recipe.Id=ulr.RecipeId
                            JOIN SlackUser su ON ulr.UserId=su.MemberId
                            JOIN TagsOnRecipe tor ON Recipe.Id = tor.RecipeId
                            JOIN Tag t ON tor.TagId = t.Id
                            WHERE su.MemberId =@Id AND t.Id = 11";

            using (SqlConnection connection = new SqlConnection(conString))
            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                connection.Open();
                command.Parameters.Add(new SqlParameter("Id", currentUser.MemberId));

                SqlDataReader reader = command.ExecuteReader();

                var list = new List<Recipe>();

                while (reader.Read())
                {
                    var recipe = new Recipe
                    {
                        Id = reader.GetSqlInt32(0).Value,
                        Name = reader.GetSqlString(1).Value,
                    };
                    list.Add(recipe);
                }

                return list;
            }
        }
    }
}