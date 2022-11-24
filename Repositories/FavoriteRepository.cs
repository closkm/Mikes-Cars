using MikesCars.Interfaces;
using System.Data.SqlClient;

namespace MikesCars.Repositories
{
    public class FavoriteRepository : IFavoriteRepository
    {
        private readonly IConfiguration _config;
        public FavoriteRepository(IConfiguration config)
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

        public void AddToFavTable(int userId, int listingId)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                                INSERT INTO [favorite](userId, listingId)
                                VALUES(@userId, @listingId)
                            ";

                    cmd.Parameters.AddWithValue("@userId", userId);
                    cmd.Parameters.AddWithValue("@listingId", listingId);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DeleteFromFavorite(int userId, int listingId)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                                DELETE FROM [favorite]
                                WHERE userId = @userId
                                AND listingId = @listingId
                            ";

                    cmd.Parameters.AddWithValue("@userId", userId);
                    cmd.Parameters.AddWithValue("@listingId", listingId);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
