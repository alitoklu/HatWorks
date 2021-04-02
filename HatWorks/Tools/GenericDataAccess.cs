using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace HatWorks.Tools
{
    public class GenericDataAccess
    {

        public DataTable ExecuteSelectCommand(MySqlCommand command)
        {

            DataTable table;
            try
            {
                command.Connection.Open();
                MySqlDataReader reader = command.ExecuteReader();
                table = new DataTable();
                table.Load(reader);
                reader.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                command.Connection.Close();
            }
            return table;
        }


        // execute an update, delete, or insert command
        // and return the number of affected rows
        public int ExecuteNonQuery(MySqlCommand command)
        {
            // The number of affected rows
            int affectedRows = -1;
            // Execute the command making sure the connection gets closed in the end
            try
            {
                // Open the connection of the command
                command.Connection.Open();
                // Execute the command and get the number of affected rows
                affectedRows = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                // Log eventual errors and rethrow them                
                throw ex;
            }
            finally
            {
                // Close the connection
                command.Connection.Close();
            }
            // return the number of affected rows
            return affectedRows;
        }

        // execute a select command and return a single result as a string
        public object ExecuteScalar(MySqlCommand command)
        {
            // The value to be returned
            object value = "";
            // Execute the command making sure the connection gets closed in the end
            try
            {
                // Open the connection of the command
                command.Connection.Open();
                // Execute the command and get the number of affected rows
                value = command.ExecuteScalar();
            }
            catch (Exception ex)
            {
                string hta = ex.Message;
                value = "";
            }
            finally
            {
                // Close the connection
                command.Connection.Close();
            }
            // return the result
            return value;
        }
        public static MySqlCommand CreateCommand()
        {

            // Obtain the database provider name
            // Obtain the database connection string
            string connectionString = ConfigurationManager.ConnectionStrings["SqlConnetionString"].ConnectionString.ToString();
            // Create a new data provider factory
            // Obtain a database specific connection object
            // Obtain a database specific connection object
            MySqlConnection conn = new MySqlConnection();
            // Set the connection string
            conn.ConnectionString = connectionString;
            // Create a database specific command object
            MySqlCommand comm = conn.CreateCommand();
            comm.Connection = conn;
            // Set the command type to stored procedure
            comm.CommandType = CommandType.Text;
            // Return the initialized command object
            return comm;
        }
    }
}
