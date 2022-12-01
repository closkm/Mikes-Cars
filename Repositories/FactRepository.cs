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
