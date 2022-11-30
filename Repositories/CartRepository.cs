using MikesCars.Interfaces;
using MikesCars.Models;
using System.Data.SqlClient;

namespace MikesCars.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly IConfiguration _config;
        public CartRepository(IConfiguration config)
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

        public void AddToCart(int listingId, int userId)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                                INSERT INTO [cart](userId, listingId, purchased)
                                VALUES(@userId, @listingId, 0)
                            ";

                    cmd.Parameters.AddWithValue("@userId", userId);
                    cmd.Parameters.AddWithValue("@listingId", listingId);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DeleteFromCart(int listingId, int userId)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                                DELETE FROM [cart]
                                WHERE userId = @userId
                                AND listingId = @listingId
                            ";

                    cmd.Parameters.AddWithValue("@userId", userId);
                    cmd.Parameters.AddWithValue("@listingId", listingId);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<Listing> GetAllItemsInCart(int listingId)
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

        public List<int> GetIdsInCart(int userId)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                                SELECT *
                                FROM [cart]
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
