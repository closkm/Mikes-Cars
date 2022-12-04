using MikesCars.Interfaces;
using MikesCars.Models;
using System.Data.SqlClient;

namespace MikesCars.Repositories
{
        public class FactRepository : IFactRepository
        {
            private readonly IConfiguration _config;
            public FactRepository(IConfiguration config)
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

        public void DeleteFact(int listingId)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                                DELETE FROM [fact]
                                WHERE listingId = @listingId
                            ";

                    cmd.Parameters.AddWithValue("@listingId", listingId);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public Fact GetFacts(int listingId)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                                SELECT *
                                FROM [fact]
                                WHERE listingId = @listingId
                            ";

                    cmd.Parameters.AddWithValue("@listingId", listingId);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        Fact fact = new Fact();
                        while (reader.Read())
                        {
                            fact.id = reader.GetInt32(reader.GetOrdinal("id"));
                            fact.electric = reader.GetBoolean(reader.GetOrdinal("electric"));
                            fact.listingId = 0;
                            fact.mpg = reader.GetDouble(reader.GetOrdinal("mpg"));
                            fact.crashes = reader.GetInt32(reader.GetOrdinal("crashes"));
                            fact.miles = reader.GetInt32(reader.GetOrdinal("miles"));
                            fact.warranty = reader.GetBoolean(reader.GetOrdinal("warranty"));
                        }
                        return fact;
                    }
                }
            }
        }

        public void PostFacts(Fact fact)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                                INSERT INTO [fact](electric, listingId, mpg, crashes, miles, warranty)
                                VALUES(@electric, @listingId, @mpg, @crashes, @miles, @warranty)
                            ";

                    cmd.Parameters.AddWithValue("@electric", fact.electric);
                    cmd.Parameters.AddWithValue("@listingId", fact.listingId);
                    cmd.Parameters.AddWithValue("@mpg", fact.mpg);
                    cmd.Parameters.AddWithValue("@crashes", fact.crashes);
                    cmd.Parameters.AddWithValue("@miles", fact.miles);
                    cmd.Parameters.AddWithValue("@warranty", fact.warranty);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
