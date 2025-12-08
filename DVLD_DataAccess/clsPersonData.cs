using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccess
{
    public class clsPersonData
    {
        public static bool GetPersonInfoByID(int PresonID, ref string NationalNo, ref string FirstName, ref string SecondName, ref string ThirdName, ref string LastName, ref DateTime DateOfBirth, ref short Gendor, ref string Address, ref string Email, ref int NationalityCountryID, ref string Phone
                                    , ref string ImagePath)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            
            SqlCommand command = new SqlCommand("SP_GetPersonInfoByID", connection);
            command.Parameters.AddWithValue("@PersonID", PresonID);

            try
            {
                command.CommandType = CommandType.StoredProcedure;
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;



                    NationalNo = (string)reader["NationalNo"];
                    FirstName = (string)reader["FirstName"];
                    SecondName = (string)reader["SecondName"];
                    if (reader["ThirdName"] != System.DBNull.Value)
                    {
                        ThirdName = (string)reader["ThirdName"];
                    }
                    else
                        ThirdName = "";

                    LastName = (string)reader["LastName"];
                    DateOfBirth = (DateTime)reader["DateOfBirth"];
                    Address = (string)reader["Address"];
                    NationalityCountryID = (int)reader["NationalityCountryID"];
                    Gendor = (byte)reader["Gendor"];
                    if (reader["Email"] != System.DBNull.Value)
                    {
                        Email = (string)reader["Email"];
                    }
                    else
                        Email = "";

                    if (reader["Phone"] != System.DBNull.Value)
                    {
                        Phone = (string)reader["Phone"];
                    }
                    else
                        Phone = "";

                    if (reader["ImagePath"] != System.DBNull.Value)
                    {
                        ImagePath = (string)reader["ImagePath"];
                    }
                    else
                        ImagePath = "";






                }

                else
                {
                    isFound = false;
                }

                reader.Close();

            }

            catch (Exception ex)
            {
                clsEvnetLog.Eventlog(ex.Message);
            }

            finally
            {
                connection.Close();
            }

            return isFound;
        }

        public static bool GetPersonInfoByNationalNo(string NationalNo, ref int PresonID, ref string FirstName, ref string SecondName, ref string ThirdName, ref string LastName, ref DateTime DateOfBirth,ref short  Gendor, ref string Address, ref string Email, ref int NationalityCountryID, ref string Phone
                                    , ref string ImagePath)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

           
            SqlCommand command = new SqlCommand("SP_GetPersonInfoByNationalNo", connection);
            command.Parameters.AddWithValue("@NationalNo", NationalNo);

            try
            {
                command.CommandType = CommandType.StoredProcedure;
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;



                    PresonID = (int)reader["PersonID"];
                    FirstName = (string)reader["FirstName"];
                    SecondName = (string)reader["SecondName"];

                    if (reader["ThirdName"] != System.DBNull.Value)
                    {
                        ThirdName = (string)reader["ThirdName"];
                    }
                    else
                        ThirdName = "";
                    LastName = (string)reader["LastName"];
                    DateOfBirth = (DateTime)reader["DateOfBirth"];
                    Address = (string)reader["Address"];
                    NationalityCountryID = (int)reader["NationalityCountryID"];
                    Gendor = (byte)reader["Gendor"];
                    if (reader["Email"] != System.DBNull.Value)
                    {
                        Email = (string)reader["Email"];
                    }
                    else
                        Email = "";

                    if (reader["Phone"] != System.DBNull.Value)
                    {
                        Phone = (string)reader["Phone"];
                    }
                    else
                        Phone = "";

                    if (reader["ImagePath"] != System.DBNull.Value)
                    {
                        ImagePath = (string)reader["ImagePath"];
                    }
                    else
                        ImagePath = "";






                }

                else
                {
                    isFound = false;
                }

                reader.Close();

            }

            catch (Exception ex)
            {
                clsEvnetLog.Eventlog(ex.Message);
            }

            finally
            {
                connection.Close();
            }

            return isFound;
        }

        public static int AddNewPerson(string NotionalNo, string FirstName, string SecondName, string ThirdName,
           string LastName, DateTime DateOfBirth, int Gendor, string Address, string Phone, string Email, int NationalityCountryID, string ImagePath)
        {
            int PersonID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string Qurey = @"INSERT INTO People(NationalNo,FirstName, SecondName, ThirdName, LastName, DateOfBirth, Gendor, Address, Phone, Email, NationalityCountryID, ImagePath) VALUES (@NationalNo, @FirstName, @SecondName, @ThirdName, @LastName, @DateOfBirth, @Gendor, @Address, @Phone, @Email, @NationalityCountryID, @ImagePath) SELECT SCOPE_IDENTITY()";
            SqlCommand command = new SqlCommand(Qurey, connection);

            command.Parameters.AddWithValue("@NationalNo", NotionalNo);
            command.Parameters.AddWithValue("@FirstName", FirstName);
            command.Parameters.AddWithValue("@SecondName", SecondName);
            
            command.Parameters.AddWithValue("@LastName", LastName);
            command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
            command.Parameters.AddWithValue("@Gendor", Gendor);
            command.Parameters.AddWithValue("@Address", Address);
            command.Parameters.AddWithValue("@Phone", Phone);

            if (ThirdName != "")
            {
                command.Parameters.AddWithValue("@ThirdName", ThirdName);
            }
            else
            {
                command.Parameters.AddWithValue("@ThirdName", System.DBNull.Value);
            }

            if (Email != "")
            {
                command.Parameters.AddWithValue("@Email", Email);
            }
            else
            {
                command.Parameters.AddWithValue("@Email", System.DBNull.Value);
            }

            command.Parameters.AddWithValue("@NationalityCountryID", NationalityCountryID);

            if (ImagePath != "")
            {
                command.Parameters.AddWithValue("@ImagePath", ImagePath);
            }
            else
            {
                command.Parameters.AddWithValue("@ImagePath", System.DBNull.Value);
            }

            try
            {
                command.CommandType = CommandType.StoredProcedure;
                connection.Open();
                object reslut = command.ExecuteScalar();
                if (reslut != null && int.TryParse(reslut.ToString(), out int PersonId))
                {
                    PersonID = PersonId;
                }

            }
            catch (Exception ex)
            {
                clsEvnetLog.Eventlog(ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return PersonID;
        }

        public static bool UpdatePerson(int PersonID, string FirstName, string SecondName,
        string ThirdName, string LastName, string NationalNo, DateTime DateOfBirth,
        short Gendor, string Address, string Phone, string Email,
         int NationalityCountryID, string ImagePath)
        {

            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);


            SqlCommand command = new SqlCommand("SP_UpdatePerson", connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@FirstName", FirstName);
            command.Parameters.AddWithValue("@SecondName", SecondName);

            if (ThirdName != "" && ThirdName != null)
                command.Parameters.AddWithValue("@ThirdName", ThirdName);
            else
                command.Parameters.AddWithValue("@ThirdName", System.DBNull.Value);


            command.Parameters.AddWithValue("@LastName", LastName);
            command.Parameters.AddWithValue("@NationalNo", NationalNo);
            command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
            command.Parameters.AddWithValue("@Gendor", Gendor);
            command.Parameters.AddWithValue("@Address", Address);
            command.Parameters.AddWithValue("@Phone", Phone);

            if (Email != "" && Email != null)
                command.Parameters.AddWithValue("@Email", Email);
            else
                command.Parameters.AddWithValue("@Email", System.DBNull.Value);

            command.Parameters.AddWithValue("@NationalityCountryID", NationalityCountryID);

            if (ImagePath != "" && ImagePath != null)
                command.Parameters.AddWithValue("@ImagePath", ImagePath);
            else
                command.Parameters.AddWithValue("@ImagePath", System.DBNull.Value);


            try
            {
                command.CommandType = CommandType.StoredProcedure;
                connection.Open();
                rowsAffected = command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                clsEvnetLog.Eventlog(ex.Message);
                return false;
            }

            finally
            {
                connection.Close();
            }

            return (rowsAffected > 0);
        }


        public static DataTable GetAllPeople()
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            SqlCommand command = new SqlCommand("SP_GetAllPeople", connection);

            try
            {
                command.CommandType = CommandType.StoredProcedure;
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if(reader.HasRows)
                {
                    dt.Load(reader);
                }
            }
            catch
            {

            }
            finally
            {
                connection.Close();
            }

            return dt;
        }

        public static bool DeletePerson(int PersonID)
        {
            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            

            SqlCommand command = new SqlCommand("SP_DeletePerson", connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {
                command.CommandType = CommandType.StoredProcedure;
                connection.Open();
                rowsAffected = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                clsEvnetLog.Eventlog(ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return (rowsAffected > 0);

        }

        public static bool IsPersonExist(int PersonID)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            
            SqlCommand command = new SqlCommand("IsPersonExistByID", connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {
                command.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = command.ExecuteReader();
                if(reader.Read())

                {
                    isFound = true;
                }
            }
            catch (Exception ex)
            {
                clsEvnetLog.Eventlog(ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return isFound;
        }

        public static bool IsPersonExist(string NationalNo)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            
            SqlCommand command = new SqlCommand("IsPersonExistByNationalNo", connection);

            command.Parameters.AddWithValue("@NationalNo", NationalNo);

            try
            {
                command.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())

                {
                    isFound = true;
                }
            }
            catch (Exception ex)
            {
                clsEvnetLog.Eventlog(ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return isFound;
        }
    }
}
