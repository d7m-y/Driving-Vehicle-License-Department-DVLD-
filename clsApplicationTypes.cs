using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConnectionString;

namespace DVLD_DataAccess
{
    public class clsApplicationTypes
    {

        public static bool GetApplicationType(int id, ref string title, ref float fees)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsConnectionString.connectionString);

            string query = @"select * from ApplicationTypes where ApplicationTypeID = @ApplicationTypeID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ApplicationTypeID", id);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    // The record was found
                    isFound = true;


                    
                    title = (string)reader["ApplicationTypeTitle"];
                    fees =Convert.ToSingle(reader["ApplicationFees"]);
                    
                }
                else
                {
                    // The record was not found
                    isFound = false;
                }

                reader.Close();


            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                isFound = false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }

        public static int AddNewApplicationType( string Title,
           float fees)
        {
            //this function will return the new Application Type id if succeeded and -1 if not.
            int id = -1;

            SqlConnection connection = new SqlConnection(clsConnectionString.connectionString);

            string query = @"INSERT INTO [dbo].[ApplicationTypes]
                                  ([ApplicationTypeTitle]
                                  ,[ApplicationFees])
                            VALUES
                                  (@ApplicationTypeTitle
                                  ,@ApplicationFees)
                                  SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);
            
            command.Parameters.AddWithValue("@ApplicationTypeTitle", Title);
            command.Parameters.AddWithValue("@ApplicationFees", fees);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();


                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    id = insertedID;
                }
            }

            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);

            }

            finally
            {
                connection.Close();
            }


            return id;
        }
        public static bool UpdateApplicationType(int id,string Title,
           float fees)
        {
            //this function will return true if succeeded and false if not.
            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsConnectionString.connectionString);
            string query = @"UPDATE [dbo].[ApplicationTypes]
                       SET [ApplicationTypeTitle] = @ApplicationTypeTitle
                          ,[ApplicationFees] = @ApplicationFees
                     WHERE ApplicationTypeID = @ApplicationTypeID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ApplicationTypeID", id);
            command.Parameters.AddWithValue("@ApplicationTypeTitle", Title);
            command.Parameters.AddWithValue("@ApplicationFees", fees);
            

            try
            {
                connection.Open();
                rowsAffected = command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);
                return false;
            }

            finally
            {
                connection.Close();
            }

            return (rowsAffected > 0);
        }

        public static DataTable GetAllApplicationTypes()
        {

            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsConnectionString.connectionString);

            string query = @"SELECT [ApplicationTypeID] as 'ID'
                                 ,[ApplicationTypeTitle] as 'Title' 
                                 ,[ApplicationFees] as 'Fees'
                             FROM [dbo].[ApplicationTypes]";

            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)

                {
                    dt.Load(reader);
                }

                reader.Close();


            }

            catch (Exception ex)
            {
                // Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return dt;

        }
    }
}
