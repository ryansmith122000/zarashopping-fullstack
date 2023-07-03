using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Data;
using ZaraShopping.Dtos;
using ZaraShopping.Interfaces;
using ZaraShopping.Responses;

namespace ZaraShopping.Services
{
    public class UsersService : IUsersService
    {
        private readonly string connectionString; // connectionString from appSettings.json

        public UsersService(string connectionString)
        {
            this.connectionString = connectionString; // assigns the connectionString parameter to the readonly field
        }


        #region - Update Not OK -
        public void UpdateUser(UserUpdateRequest model)
        {
            using SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();

            using SqlCommand command = sqlConnection.CreateCommand();

            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[dbo].[Users_Update]";

            UpdateCommonParams(model, command.Parameters);

/*            SqlParameter idParameter = new SqlParameter("@Id", SqlDbType.Int);

            idParameter.Value = model.Id;*/
            command.Parameters.AddWithValue("@Id", model.Id);


            command.ExecuteNonQuery();

            sqlConnection.Close();
        }

        #endregion

        #region - Add OK -
        public int CreateUser(UserAddRequest model)
        {
            using SqlConnection sqlConnection = new(connectionString); // creating a SQL connection
            sqlConnection.Open(); // opening the SQL connection

            using SqlCommand command = sqlConnection.CreateCommand();

            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[dbo].[Users_Add]";

            AddCommonParams(model, command.Parameters);
            command.Parameters.Add("@Id", SqlDbType.Int).Direction = ParameterDirection.Output;

            command.ExecuteNonQuery();

            int id = (int)command.Parameters["@Id"].Value;

            return id;
        }
        #endregion

        #region - Get All OK -
        public List<Users> GetAll()
        {
            using SqlConnection sqlConnection = new(connectionString);
            sqlConnection.Open();
            using SqlCommand command = sqlConnection.CreateCommand();

            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[dbo].[Users_SelectAll]";

            List<Users> list = new List<Users>();

            using SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Users aUser = MapSingleUser(reader);
                list.Add(aUser);
            }
            return list;
        }
        #endregion

        #region - Get By Id OK -

        public ItemResponse<Users> GetById(int id)
        {
            using SqlConnection sqlConnection = new(connectionString);
            sqlConnection.Open();
            using SqlCommand command = sqlConnection.CreateCommand();

            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[dbo].[Users_SelectById]";
            command.Parameters.AddWithValue("@Id", id);

            using SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                Users user = MapSingleUser(reader);
                return new ItemResponse<Users>
                {
                    Item = user
                };
            }
            else
            {
                return new ItemResponse<Users>
                { IsSuccessful = false };
            }
        }
        #endregion



        #region - Delete Not OK -

        public void Delete(int id)
        {
            using SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
            using SqlCommand command = sqlConnection.CreateCommand();

            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[dbo].[Users_Delete]";

            command.Parameters.AddWithValue("@Id", id);

            command.ExecuteNonQuery();
        }


        #endregion

        #region - Map For Get - OK - 
        private static Users MapSingleUser(IDataReader reader)
        {
            Users aUser = new Users();

            int startingIndex = 0;

            aUser.Id = reader.IsDBNull(startingIndex) ? 0 : reader.GetInt32(startingIndex++);
            aUser.Name = reader.IsDBNull(startingIndex) ? null : reader.GetString(startingIndex++);
            aUser.Email = reader.IsDBNull(startingIndex) ? null : reader.GetString(startingIndex++);
            aUser.Password = reader.IsDBNull(startingIndex) ? null : reader.GetString(startingIndex++);
            aUser.ProfilePicture = reader.IsDBNull(startingIndex) ? null : reader.GetString(startingIndex++);
            aUser.Gender = reader.IsDBNull(startingIndex) ? null : reader.GetString(startingIndex++);
            aUser.RoleName = reader.IsDBNull(startingIndex) ? null : reader.GetString(startingIndex++);

            // Check for null value before assigning to IsDeleted
            if (!reader.IsDBNull(startingIndex))
            {
                aUser.IsDeleted = reader.GetBoolean(startingIndex);
            }
            startingIndex++; // Move to the next index

            aUser.Street = reader.IsDBNull(startingIndex) ? null : reader.GetString(startingIndex++);
            aUser.LineTwo = reader.IsDBNull(startingIndex) ? null : reader.GetString(startingIndex++);
            aUser.City = reader.IsDBNull(startingIndex) ? null : reader.GetString(startingIndex++);
            aUser.State = reader.IsDBNull(startingIndex) ? null : reader.GetString(startingIndex++);
            aUser.ZipCode = reader.IsDBNull(startingIndex) ? 0 : reader.GetInt32(startingIndex++);
            aUser.Country = reader.IsDBNull(startingIndex) ? null : reader.GetString(startingIndex++);
            aUser.DateOfBirth = reader.IsDBNull(startingIndex) ? DateTime.MinValue : reader.GetDateTime(startingIndex++);





            return aUser;
        }








        #endregion

        #region - AddCommonParams - OK - 
        private static void AddCommonParams(UserAddRequest model, SqlParameterCollection col)
        {
            col.AddWithValue("@Name", model.Name);
            col.AddWithValue("@Email", model.Email);
            col.AddWithValue("@Password", model.Password);
            col.AddWithValue("@ProfilePicture", model.ProfilePicture);
            col.AddWithValue("@DateOfBirth", model.DateOfBirth); // this needs to be filled with a certain format or else SQL goes nuts
            col.AddWithValue("@Gender", model.Gender);
            col.AddWithValue("@Street", model.Street);
            col.AddWithValue("@LineTwo", model.LineTwo);
            col.AddWithValue("@City", model.City);
            col.AddWithValue("@State", model.State);
            col.AddWithValue("@ZipCode", model.ZipCode);
            col.AddWithValue("@Country", model.Country);
            col.AddWithValue("@RoleId", model.RoleId);
            col.AddWithValue("@CreatedBy", model.CreatedBy);
        }
        #endregion

        private static void UpdateCommonParams(UserUpdateRequest model, SqlParameterCollection col)
        {
            col.AddWithValue("@Name", model.Name);
            col.AddWithValue("@Email", model.Email);
            col.AddWithValue("@Password", model.Password);
            col.AddWithValue("@ProfilePicture", model.ProfilePicture);
            col.AddWithValue("@DateOfBirth", model.DateOfBirth); // this needs to be filled with a certain format or else SQL goes nuts
            col.AddWithValue("@Gender", model.Gender);
            col.AddWithValue("@Street", model.Street);
            col.AddWithValue("@LineTwo", model.LineTwo);
            col.AddWithValue("@City", model.City);
            col.AddWithValue("@State", model.State);
            col.AddWithValue("@ZipCode", model.ZipCode);
            col.AddWithValue("@Country", model.Country);
        }
    }

    /*    To implement a login functionality in C#, you can follow these general steps:
     *    
                1. Collect the user's login credentials, typically the username/email and password, through a login form or input fields.

                2. Validate the user's input and ensure that the required fields are not empty.

                3. Query the database to retrieve the user record based on the provided username/email.
                      Verify the user's password by comparing the hashed password stored in the database with the provided password after hashing it using the same algorithm.
                      You can use the BCrypt.Net library to hash and compare passwords, similar to what we discussed earlier.

                5. If the password matches, generate a secure session token or authentication token for the user.

                6. You can use technologies like JSON Web Tokens (JWT) or ASP.NET Core Identity for token-based authentication.
                7. Store the generated token securely on the server or return it as a response to the client.

                8. Typically, you would store the token in a server-side session or return it in the response header or body to be stored in a client-side cookie or local storage.
                    Redirect the user to the authenticated area of your application or return a success response indicating successful login.*/

}
