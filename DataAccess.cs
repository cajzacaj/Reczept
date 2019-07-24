using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ReczeptBot
{
    internal class DataAccess
    {
        private const string conString = "Server=(localdb)\\mssqllocaldb; Database=Reczept";

        internal List<Recipe> GetAllRecipes()
        {
            var sql = @"SELECT [Id], [Name]
                        FROM Recipe";

            using (SqlConnection connection = new SqlConnection(conString))
            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                connection.Open();

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
	

        internal List<Recipe> GetAllRecipesWithTag(Tag tag)
        {
            var sql = @"SELECT [Id], [Name]
                        FROM Recipe
                        JOIN TagsOnRecipe tor ON Recipe.Id=tor.RecipeId
                        JOIN Tag ON tor.TagId=Tag.Id
                        WHERE Tag.Id=@Id";

            using (SqlConnection connection = new SqlConnection(conString))
            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                connection.Open();
                command.Parameters.Add(new SqlParameter("Id", tag.Id));

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

        internal void GetUserIdFromName(User user)
        {
            var sql = @"SELECT MemberId
                        FROM SlackUser 
                        WHERE Name=@Name";

            using (SqlConnection connection = new SqlConnection(conString))
            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                connection.Open();
                command.Parameters.Add(new SqlParameter("Name", user.Name));

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    user.MemberId = reader.GetSqlString(0).Value;
                }
            }
        }

        internal void GetTagId(Tag tag)
        {
            var sql = @"SELECT Id
                        FROM Tag 
                        WHERE Name=@Name";

            using (SqlConnection connection = new SqlConnection(conString))
            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                connection.Open();
                command.Parameters.Add(new SqlParameter("Name", tag.Name));

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    tag.Id = reader.GetSqlInt32(0).Value;
                }
            }
        }

        internal List<Tag> GetTagsForRecipe(Recipe recipe)
        {
            var sql = @"SELECT t.id, t.Name FROM Tag t
                        JOIN TagsOnRecipe tor ON t.Id=tor.TagId
                        JOIN Recipe r ON tor.RecipeId = r.Id
                        WHERE r.Id=@Id";

            using (SqlConnection connection = new SqlConnection(conString))
            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                connection.Open();
                command.Parameters.Add(new SqlParameter("Id", recipe.Id));

                SqlDataReader reader = command.ExecuteReader();

                var list = new List<Tag>();

                while (reader.Read())
                {
                    var tag = new Tag
                    {
                        Id = reader.GetSqlInt32(0).Value,
                        Name = reader.GetSqlString(1).Value,
                    };
                    list.Add(tag);
                }

                return list;
            }
        }

        internal void AddUserLikesRecipe(Recipe recipe, User currentUser)
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
}
