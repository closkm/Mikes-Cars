using MikesCars.Interfaces;
using MikesCars.Models;
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

        public List<Listing> GetAllItemsInFavorite(int listingId)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                                SELECT *
                                FROM [listing]
                                WHERE id IN (@listingIds)
                                AND purchased = 0
                            ";

                    cmd.Parameters.AddWithValue("@listingIds", listingId);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        List<Listing> listings = new List<Listing>();
                        while (reader.Read())
                        {
                            Listing listing = new Listing()
                            {
                                id = reader.GetInt32(reader.GetOrdinal("id")),
                                userId = reader.GetInt32(reader.GetOrdinal("userId")),
                                type = reader.GetString(reader.GetOrdinal("type")),
                                maker = reader.GetString(reader.GetOrdinal("maker")),
                                address = reader.GetString(reader.GetOrdinal("address")),
                                price = reader.GetDouble(reader.GetOrdinal("price")),
                                dateOfListing = reader.GetDateTime(reader.GetOrdinal("dateOfListing")),
                                favorites = reader.GetInt32(reader.GetOrdinal("favorites")),
                                purchased = reader.GetBoolean(reader.GetOrdinal("purchased")),
                                inCart = reader.GetBoolean(reader.GetOrdinal("inCart"))
                            };
                            listings.Add(listing);
                        }
                        return listings;
                    }
                }
            }
        }

        public List<int> GetIdsInFav(int userId)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                                SELECT *
                                FROM [favorite]
                                WHERE userId = @userId
                            ";

                    cmd.Parameters.AddWithValue("@userId", userId);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        List<int> listings = new List<int>();
                        while (reader.Read())
                        {
                            int listingId = reader.GetInt32(reader.GetOrdinal("listingId"));
                            listings.Add(listingId);
                        }
                        return listings;
                    }
                }
            }
        }
    }
}
