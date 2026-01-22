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
    public class clsLicenseClasses
    {

        public static bool GetLicenseClasses(int id, ref string ClassName, ref string ClassDesc
            ,ref int MinAllowedAge,ref int DefaultValidityLength,ref float fees)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsConnectionString.connectionString);

            string query = @"select * from LicenseClasses where LicenseClassID = @LicenseClassID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LicenseClassID", id);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    // The record was found
                    isFound = true;



                    ClassName = (string)reader["ClassName"];
                    ClassDesc = (string)reader["ClassDescription"];
                    MinAllowedAge = Convert.ToInt32(reader["MinimumAllowedAge"]);
                    DefaultValidityLength = Convert.ToInt32(reader["DefaultValidityLength"]);
                    fees = Convert.ToSingle(reader["ClassFees"]);

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

        public static int AddNewLicenseClasses( string ClassName, string ClassDesc
            , int MinAllowedAge, int DefaultValidityLength, float fees)
        {
            //this function will return the new Application Type id if succeeded and -1 if not.
            int id = -1;

            SqlConnection connection = new SqlConnection(clsConnectionString.connectionString);

            string query = @"INSERT INTO [dbo].[LicenseClasses]
                                         ([ClassName]
                                         ,[ClassDescription]
                                         ,[MinimumAllowedAge]
                                         ,[DefaultValidityLength]
                                         ,[ClassFees])
                                   VALUES
                                         (@ClassName
                                         ,@ClassDescription
                                         ,@MinimumAllowedAge
                                         ,@DefaultValidityLength
                                         ,@ClassFees)
                                  SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ClassName", ClassName);
            command.Parameters.AddWithValue("@ClassDescription", ClassDesc);
            command.Parameters.AddWithValue("@MinimumAllowedAge", MinAllowedAge);
            command.Parameters.AddWithValue("@DefaultValidityLength", DefaultValidityLength);
            command.Parameters.AddWithValue("@ClassFees", fees);

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
        public static bool UpdateLicenseClasses(int id, string ClassName, string ClassDesc
            , int MinAllowedAge, int DefaultValidityLength, float fees)
        {
            //this function will return true if succeeded and false if not.
            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsConnectionString.connectionString);
            string query = @"UPDATE [dbo].[LicenseClasses]
                                   SET [ClassName] = @ClassName
                                      ,[ClassDescription] = @ClassDescription
                                      ,[MinimumAllowedAge] = @MinimumAllowedAge
                                      ,[DefaultValidityLength] = @DefaultValidityLength
                                      ,[ClassFees] = @ClassFees
                                 WHERE LicenseClassID = @LicenseClassID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LicenseClassID", id);
            command.Parameters.AddWithValue("@ClassName", ClassName);
            command.Parameters.AddWithValue("@ClassDescription", ClassDesc);
            command.Parameters.AddWithValue("@MinimumAllowedAge", MinAllowedAge);
            command.Parameters.AddWithValue("@DefaultValidityLength", DefaultValidityLength);
            command.Parameters.AddWithValue("@ClassFees", fees);


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

        public static DataTable GetAllLicenseClasses()
        {

            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsConnectionString.connectionString);

            string query = @"SELECT [LicenseClassID] as 'License Class ID'
                                        ,[ClassName] as 'Class Name'
                                        ,[ClassDescription] as 'Class Description'
                                        ,[MinimumAllowedAge] as 'Minimum Allowed Age'
                                        ,[DefaultValidityLength] as 'Default Validity Length'
                                        ,[ClassFees] as 'Class Fees'
                                    FROM [dbo].[LicenseClasses]";

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
