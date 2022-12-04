using MikesCars.Interfaces;
using MikesCars.Models;
using System.Data.SqlClient;
using System.Drawing;

namespace MikesCars.Repositories
{
    public class ImageRepository : IImageRepository
    {
        private readonly IConfiguration _config;

        // The constructor accepts an IConfiguration object as a parameter. This class comes from the ASP.NET framework and is useful for retrieving things out of the appsettings.json file like connection strings.
        public ImageRepository(IConfiguration config)
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

        public void DeleteImage(int listingId)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                                DELETE FROM [image]
                                WHERE listingId = @listingId
                            ";

                    cmd.Parameters.AddWithValue("@listingId", listingId);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<ImageModel> GetListingImages(int listingId)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                                SELECT *
                                FROM [image]
                                WHERE listingId = @listingId
                            ";

                    cmd.Parameters.AddWithValue("@listingId", listingId);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        List<ImageModel> images = new List<ImageModel>();
                        while (reader.Read())
                        {
                            ImageModel image = new ImageModel()
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("id")),
                                ListingId = listingId,
                                Img = reader.GetString(reader.GetOrdinal("img")),
                                DisplayOrder = reader.GetInt32(reader.GetOrdinal("displayOrder"))
                            };
                            images.Add(image);
                        }
                        return images;
                    }
                }
            }
        }

        public void PostNewImage(ImageModel image)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                                INSERT INTO [image](listingId, displayOrder, img)
                                VALUES(@listingId, @displayOrder, @image)
                            ";

                    cmd.Parameters.AddWithValue("@listingId", image.ListingId);
                    cmd.Parameters.AddWithValue("@displayOrder", image.DisplayOrder);
                    cmd.Parameters.AddWithValue("@image", image.Img);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
