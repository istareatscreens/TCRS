using Dapper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCRS_db
{
    public class DataAccess : IDataAccess
    {
        //get Data queries
        public List<T> LoadData<T, U>(string sql, U parameters, string connectionString)
        {
            using (IDbConnection connection = new MySqlConnection(connectionString))
            {
                //Not returning directly to allow for easier debugging
                connection.Open();
                List<T> rows = connection.Query<T>(sql, parameters).ToList();

                return rows;
            }
        }

        public IEnumerable<T> GetAll<T>(string connectionString, T Model)
        {

            using (IDbConnection connection = new MySqlConnection(connectionString))
            {

                string sqlQuery = @"Select * FROM Person";
                return connection.Query<T>(sqlQuery);
            }
        }

        public void SaveData<U>(string sql, U parameters, string connectionString)
        {
            using (IDbConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                connection.Execute(sql, parameters);
            }

        }

    }
}
