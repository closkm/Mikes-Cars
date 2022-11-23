using MikesCars.Interfaces;
using System.Data.SqlClient;

namespace MikesCars.Repositories
{
        public class FactRepository : IFact
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
        }
}
