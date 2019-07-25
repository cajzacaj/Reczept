using ReczeptBot;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Bloggy.RepoAlternative
{
    public class TagRepositoryAlternative
    {
        string conString;

        public TagRepositoryAlternative(string conString)
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

        public void Add(Tag tag)
        {
            Connect("INSERT INTO [dbo].[Tag] ([Id], [Name]) VALUES (@Id, @Name)", (x) => {

                x.Parameters.Add(new SqlParameter("Id", tag.Id));
                x.Parameters.Add(new SqlParameter("Name", tag.Name));

                x.ExecuteScalar();
            });
        }

        public void Remove(Guid id)
        {
            Connect("DELETE FROM BlogPostsTags WHERE TagId=@Id", (command) => {

                command.Parameters.Add(new SqlParameter("Id", id));
                command.ExecuteNonQuery();
            });

            Connect("DELETE FROM Tag WHERE Id=@Id", (command) => {

                command.Parameters.Add(new SqlParameter("Id", id));
                command.ExecuteNonQuery();
            });
        }

        public IEnumerable<Tag> GetAll()
        {

            var list = new List<Tag>();
            Connect("SELECT[Id], [Name] FROM Tag", (command) => {

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    //var t = new Tag(reader.GetGuid(0), reader.GetString(1));
                    //list.Add(t);
                }

            });
            return list;
        }

        public void Update(Guid id, Tag tag)
        {
            Connect("UPDATE Tag SET Name=@Name WHERE Id=@Id", (command) => {

                command.Parameters.Add(new SqlParameter("Id", tag.Id));
                command.Parameters.Add(new SqlParameter("Name", tag.Name));
                command.ExecuteNonQuery();
            });
        }

    }
}
