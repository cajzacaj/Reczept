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
            var sql = @"SELECT [Id] [Name]
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
            var sql = @"SELECT [Id] [Name]
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
        internal void GetTagId(Tag tag)
        {
            throw new NotImplementedException();
        }
    }
}
