using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConnectionString;
using static System.Net.Mime.MediaTypeNames;

namespace DVLD_DataAccess
{
    public class clsDataAccess
    {
        //Method To Read Data From Database

        public static DataTable SelectData(string Qurey, SqlParameter[] parameters)
        {
            DataTable dataTable = new DataTable();


            try
            {
                using (SqlConnection connection = new SqlConnection(clsConnectionString.connectionString))
                using (SqlCommand command = new SqlCommand(Qurey, connection))
                { 
                        command.CommandType = CommandType.StoredProcedure;

                        if (parameters != null)
                        {
                            for (int i = 0; i < parameters.Length; i++)
                            {
                                command.Parameters.Add(parameters[i]);
                            }
                        }

                            connection.Open();
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dataTable);
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"SQL Error: {ex.Message}");
                // Optionally rethrow or log
            }
            catch (Exception ex)
            {
                // Handle exception (e.g., log the error)
                Console.WriteLine("Error: " + ex.Message);
            }

            return dataTable;
        }

        //Method To Read Data From Database With Return Values with ExecuteReader

       




        //Method To Insert,Update,Delete From Database

        public static bool ExecuteCommand(string Qurey, SqlParameter[] parameters)
        {

            try {
                using (SqlConnection connection = new SqlConnection(clsConnectionString.connectionString))
                using (SqlCommand command = new SqlCommand(Qurey, connection))
                {

                
                    command.CommandType = CommandType.StoredProcedure;
                    connection.Open();

                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }
                    command.ExecuteNonQuery();
                    return true;
                }
            }
            catch (SqlException ex)
            {
                // More specific exception type for SQL errors
                Console.WriteLine($"SQL Error: {ex.Message}");
                // Optionally: throw or log
                return false;
            }
            catch (Exception ex)
            {
                // Handle exception (e.g., log the error)
                Console.WriteLine("Error: " + ex.Message);
                return false;
            }
        }

        public static int ExecuteCommandWithReturn(string Qurey, SqlParameter[] parameters)
        {
            int result = -1;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsConnectionString.connectionString))
                using (SqlCommand command = new SqlCommand(Qurey, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }
                    connection.Open();
                    object returnValue = command.ExecuteScalar();
                    if (returnValue != null && int.TryParse(returnValue.ToString(), out int intValue))
                    {
                        result = intValue;
                    }
                }
            }
            catch (SqlException ex)
            {
                // More specific exception type for SQL errors
                Console.WriteLine($"SQL Error: {ex.Message}");
                // Optionally: throw or log
            }
            catch (Exception ex)
            {
                // Handle exception (e.g., log the error)
                Console.WriteLine("Error: " + ex.Message);
            }
            return result;
        }
    }
}
