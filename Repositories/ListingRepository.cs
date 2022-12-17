using MikesCars.Interfaces;
using MikesCars.Models;
using System.Data.SqlClient;

namespace MikesCars.Repositories
{
    public class ListingRepository : IListingRepository
    {
        private readonly IConfiguration _config;

        public ListingRepository(IConfiguration config)
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

        public void AddToCart(int listingId)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                                UPDATE [listing]
                                SET inCart = 1
                                WHERE id = @listingId
                            ";

                    cmd.Parameters.AddWithValue("@listingId", listingId);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public bool CheckIfUsersCar(int userId, int listingId)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                                SELECT *
                                FROM [listing]
                                WHERE userId = @userId
                                AND id = @listingId
                            ";
                    cmd.Parameters.AddWithValue("@userId", userId);
                    cmd.Parameters.AddWithValue("@listingId", listingId);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            return true;
                        }
                        return false;
                    }
                }
            }
        }

        public void DeleteFromCart(int listingId)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                                UPDATE [listing]
                                SET inCart = 0
                                WHERE id = @listingId
                            ";

                    cmd.Parameters.AddWithValue("@listingId", listingId);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DeleteFromFavorite(int listingId)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                                UPDATE [listing]
                                SET favorites = favorites - 1
                                WHERE id = @listingId
                            ";

                    cmd.Parameters.AddWithValue("@listingId", listingId);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DeleteListing(int listingId)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                                DELETE FROM [listing]
                                WHERE id = @listingId
                            ";

                    cmd.Parameters.AddWithValue("@listingId", listingId);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void EditListing(Listing listing)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                                UPDATE [listing]
                                SET type = @type,
                                maker = @maker,
                                address = @address,
                                price = @price
                                WHERE id = @id
                            ";

                    cmd.Parameters.AddWithValue("@type", listing.type);
                    cmd.Parameters.AddWithValue("@maker", listing.maker);
                    cmd.Parameters.AddWithValue("@address", listing.address);
                    cmd.Parameters.AddWithValue("@price", listing.price);
                    cmd.Parameters.AddWithValue("@id", listing.id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<Listing> GetAllAvailableListings(int userId)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                                SELECT *
                                FROM [listing]
                                WHERE purchased = 0
                                AND id not in ( select listingId from
                                [cart] where userId = @userId )
                            ";
                    cmd.Parameters.AddWithValue("@userId", userId);
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

        public Listing GetListingById(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                                SELECT *
                                FROM [listing]
                                WHERE id = @id
                            ";
                    cmd.Parameters.AddWithValue("@id", id);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        Listing listing = new Listing();
                        while (reader.Read())
                        {
                            listing.id = reader.GetInt32(reader.GetOrdinal("id"));
                            listing.userId = reader.GetInt32(reader.GetOrdinal("userId"));
                            listing.type = reader.GetString(reader.GetOrdinal("type"));
                            listing.maker = reader.GetString(reader.GetOrdinal("maker"));
                            listing.address = reader.GetString(reader.GetOrdinal("address"));
                            listing.price = reader.GetDouble(reader.GetOrdinal("price"));
                            listing.dateOfListing = reader.GetDateTime(reader.GetOrdinal("dateOfListing"));
                            listing.favorites = reader.GetInt32(reader.GetOrdinal("favorites"));
                            listing.purchased = reader.GetBoolean(reader.GetOrdinal("purchased"));
                            listing.inCart = reader.GetBoolean(reader.GetOrdinal("inCart"));
                        }
                        return listing;
                    }
                }
            }
        }

        public int PostNewListing(Listing listing)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                                INSERT INTO [listing](userId, type, maker, address, price, dateOfListing, favorites, purchased, inCart)
                                VALUES(@userId, @type, @maker, @address, @price, @dateOfListing, @favorites, @purchased, @inCart)
                                SELECT * from [listing] where id = SCOPE_IDENTITY();
                            ";

                    cmd.Parameters.AddWithValue("@userId", listing.userId);
                    cmd.Parameters.AddWithValue("@type", listing.type);
                    cmd.Parameters.AddWithValue("@maker", listing.maker);
                    cmd.Parameters.AddWithValue("@address", listing.address);
                    cmd.Parameters.AddWithValue("@price", listing.price);
                    cmd.Parameters.AddWithValue("@dateOfListing", DateTime.Now);
                    cmd.Parameters.AddWithValue("@favorites", listing.favorites);
                    cmd.Parameters.AddWithValue("@purchased", listing.purchased);
                    cmd.Parameters.AddWithValue("@inCart", listing.inCart);


                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        int id = 0;
                        while (reader.Read())
                        {
                            id = reader.GetInt32(reader.GetOrdinal("id"));
                        }
                        return id;
                    }
                }
            }

        }

        public void Purchased(int listingId)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                                UPDATE [listing]
                                SET purchased = 1
                                WHERE id = @listingId
                            ";

                    cmd.Parameters.AddWithValue("@listingId", listingId);
                    cmd.ExecuteNonQuery();

                }
            }
        } 
        //fixed
        public void UpdateFavorites(int listingId)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                                UPDATE [listing]
                                SET favorites = favorites + 1
                                WHERE id = @listingId
                            ";

                    cmd.Parameters.AddWithValue("@listingId", listingId);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
