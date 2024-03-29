﻿
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace ServerAPI.DBContext
{
    class MysqlHelper
    {
        List<MySqlParameter> parameters = new List<MySqlParameter>();
        string query = "";
        public MysqlHelper AddParameter(MySqlParameter parameter)
        {
            parameters.Add(parameter);
            return this;
        }

        public MysqlHelper SetQuery(string query)
        {
            parameters = new List<MySqlParameter>();
            this.query = query;
            return this;
        }

        public MySqlDataReader ExecuteReader()
        {
            return MySqlHelper.ExecuteReader(ConnectionString.connectionString, query, parameters.ToArray());
        }

        public void ExecuteNonQuery()
        {
            MySqlHelper.ExecuteNonQuery(ConnectionString.connectionString, query, parameters.ToArray());
        }

    }
}
