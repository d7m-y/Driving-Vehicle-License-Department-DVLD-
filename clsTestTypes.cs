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
    public class clsTestTypes
    {
        public static bool GetTestType(int id, ref string title, ref string typeDesc, ref float fees)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsConnectionString.connectionString);

            string query = @"select * from TestTypes
                            where TestTypeID = @TestTypeID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@TestTypeID", id);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    // The record was found
                    isFound = true;



                    title = (string)reader["TestTypeTitle"];
                    typeDesc = (string)reader["TestTypeDescription"];
                    fees = Convert.ToSingle(reader["TestTypeFees"]);

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

        public static int AddNewTestType(string title, string typeDesc, float fees)
        {
            //this function will return the new Application Type id if succeeded and -1 if not.
            int id = -1;

            SqlConnection connection = new SqlConnection(clsConnectionString.connectionString);

            string query = @"INSERT INTO [dbo].[TestTypes]
                               ([TestTypeTitle]
                               ,[TestTypeDescription]
                               ,[TestTypeFees])
                         VALUES
                               (@TestTypeTitle
                               ,@TestTypeDescription
                               ,@TestTypeFees)
                                  SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@TestTypeTitle", title);
            command.Parameters.AddWithValue("@TestTypeDescription", typeDesc);
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
        public static bool UpdateTestType(int id, string Title, string typeDesc,
           float fees)
        {
            //this function will return true if succeeded and false if not.
            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsConnectionString.connectionString);
            string query = @"UPDATE [dbo].[TestTypes]
                           SET [TestTypeTitle] =       @TestTypeTitle
                              ,[TestTypeDescription] = @TestTypeDescription
                              ,[TestTypeFees] =        @TestTypeFees
                         WHERE TestTypeID = @TestTypeID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@TestTypeID", id);
            command.Parameters.AddWithValue("@TestTypeTitle", Title);
            command.Parameters.AddWithValue("@TestTypeDescription", typeDesc);
            command.Parameters.AddWithValue("@TestTypeFees", fees);


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

        public static DataTable GetAllTestTypes()
        {

            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsConnectionString.connectionString);

            string query = @"SELECT [TestTypeID] as 'ID'
                                 ,[TestTypeTitle] as 'Title'
                                 ,[TestTypeDescription] as 'Description'
                                 ,[TestTypeFees] as 'Fees'
                             FROM [dbo].[TestTypes]";

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
