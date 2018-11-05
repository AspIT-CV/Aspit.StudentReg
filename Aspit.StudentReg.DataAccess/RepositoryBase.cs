using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace Aspit.StudentReg.DataAccess
{
    /// <summary>
    /// Base class for repositories.
    /// </summary>
    public abstract class RepositoryBase
    {
        /// <summary>
        /// The connection string, used to connect to the persistence server.
        /// </summary>
        protected readonly string connectionString;

        /// <summary>
        /// Base constructor. Call this from deriving constructors.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        public RepositoryBase(string connectionString)
        {
            if(string.IsNullOrEmpty(connectionString))
            {
                throw new DataAccessException("Invalid connection string - was null or empty");
            }
            try
            {
                TryConnect(connectionString);
            }
            catch(DataAccessException)
            {
                throw;
            }
            this.connectionString = connectionString;
        }

        /// <summary>
        /// Attempts to create connection to the server, using the provided connection string.
        /// </summary>
        /// <param name="connectionString"></param>
        internal static void TryConnect(string connectionString)
        {
            try
            {
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                connection.Close();
            }
            catch(Exception e)
            {
                throw new DataAccessException("Connection attempt failed.", e);
            }
        }

        /// <summary>
        /// Executes sql queries against the databse.
        /// </summary>
        /// <param name="sql">The sql query to execute.</param>
        /// <returns>A <see cref="DataSet"/> filled with any data matching the provided sql query.</returns>
        internal DataSet Execute(SqlCommand sql)
        {
            // TODO: check sql parameter.
            DataSet resultSet = default;
            try
            {
                sql.Connection = new SqlConnection(connectionString);
                using(SqlDataAdapter adapter = new SqlDataAdapter(sql))
                {
                    adapter.Fill(resultSet);
                }
            }
            catch(Exception e)
            {
                throw new DataAccessException("Error communicating data with database.", e);
            }
            return resultSet;
        }

        // TODO: static method to retrieve conn string from config file.
    }
}