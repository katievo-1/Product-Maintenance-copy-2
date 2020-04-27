﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductMaintenance
{
    class MMABooksDB
    {
        public static SqlConnection GetConnection()
        {
            string connectionString =
                "Data Source=(localdb)\\MSSQLLocalDB;" + 
                "AttachDbFilename=|DataDirectory|MMABooks.mdf;" + 
                "Integrated Security=True";
            SqlConnection connection = new SqlConnection(connectionString);
            return connection;
        }
    }
}
