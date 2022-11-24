using MikesCars.Interfaces;
using MikesCars.Models;
using System.Data.SqlClient;

namespace MikesCars.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IConfiguration _config;

        // The constructor accepts an IConfiguration object as a parameter. This class comes from the ASP.NET framework and is useful for retrieving things out of the appsettings.json file like connection strings.
        public UserRepository(IConfiguration config)
        {
            _config = config;
        }
        public SqlConnection Connection
        {
            get
            {
                return new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            }
        }

        public void EditLoggedInUser(User user)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                                UPDATE [user]
                                SET firstName = @firstName,
                                lastName = @lastName
                                WHERE id = @id
                            ";
                    cmd.Parameters.AddWithValue("@firstName", user.firstName);
                    cmd.Parameters.AddWithValue("@lastName", user.lastName);
                    cmd.Parameters.AddWithValue("@id", user.id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<User> GetAllUsers()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                                SELECT *
                                FROM [user]
                            ";
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        List<User> users = new List<User>();
                        while (reader.Read())
                        {
                            User user = new User()
                            {
                                id = reader.GetInt32(reader.GetOrdinal("id")),
                                firstName = reader.GetString(reader.GetOrdinal("firstName")),
                                lastName = reader.GetString(reader.GetOrdinal("lastName")),
                                firebaseId = reader.GetString(reader.GetOrdinal("firebaseId"))
                            };
                            users.Add(user);
                        }
                        return users;
                    }
                }
            }
        }

        public User GetLoggedInUser(string firebaseId)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                                SELECT *
                                FROM [user]
                                WHERE firebaseId = @firebaseId
                            ";
                    cmd.Parameters.AddWithValue("@firebaseId", firebaseId);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        User user = new User();
                        while (reader.Read())
                        {
                            user.id = reader.GetInt32(reader.GetOrdinal("id"));
                            user.firstName = reader.GetString(reader.GetOrdinal("firstName"));
                            user.lastName = reader.GetString(reader.GetOrdinal("lastName"));
                            user.firebaseId = reader.GetString(reader.GetOrdinal("firebaseId"));
                        }
                        return user;
                    }
                }
            }
        }
    }
}
