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

        internal List<Recipe> GetAllRecipesWithTag(string tag)
        {
            throw new NotImplementedException();
        }
    }
}