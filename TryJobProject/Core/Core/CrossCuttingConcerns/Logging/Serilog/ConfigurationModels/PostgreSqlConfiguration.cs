﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CrossCuttingConcerns.Logging.Serilog.ConfigurationModels
{
    public class PostgreSqlConfiguration
    {
        public string ConnectionString { get; set; }
        public string TableName { get; set; }
        public bool AutoCreateSqlTable { get; set; }

        public PostgreSqlConfiguration()
        {
            ConnectionString = string.Empty;
            TableName = string.Empty;
        }

        public PostgreSqlConfiguration(string connectionString, string tableName, bool autoCreateSqlTable)
        {
            ConnectionString = connectionString;
            TableName = tableName;
            AutoCreateSqlTable = autoCreateSqlTable;
        }
    }
}
