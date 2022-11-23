using MikesCars.Interfaces;
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
    }
}
