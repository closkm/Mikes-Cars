﻿using MikesCars.Interfaces;
using System.Data.SqlClient;

namespace MikesCars.Repositories
{
    public class CartRepository : ICart
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
    }
}
