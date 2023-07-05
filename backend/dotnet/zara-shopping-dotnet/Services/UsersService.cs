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
        #region - Add OK -
        public int CreateUser(UserAddRequest model)
        {
            int id = 0;

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();

                using (SqlCommand command = new SqlCommand("[dbo].[Users_Add]", sqlConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    AddCommonParams(model, command.Parameters);

                    SqlParameter idParameter = new SqlParameter("@Id", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(idParameter);

                    command.ExecuteNonQuery();

                    if (idParameter.Value != DBNull.Value && int.TryParse(idParameter.Value.ToString(), out int parsedId))
                    {
                        id = parsedId;
                    }
                }
            }

            return id;
        }

        #endregion

        #region - Log In Not OK -

/*        public ItemResponse<UserLogin> Login(string username, string email, string password)
        {
            using SqlConnection sqlConnection = new(connectionString);
            sqlConnection.Open();
            using SqlCommand command = sqlConnection.CreateCommand();

            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[dbo].[User_Login]";
        }*/
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

        #region - Update OK -
        public void UpdateUser(int id, UserUpdateRequest model)
        {
            model.Id = id;

            using SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();

            using SqlCommand command = sqlConnection.CreateCommand();

            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[dbo].[Users_Update]";

            AddCommonParams(model, command.Parameters);

            command.Parameters.AddWithValue("@Id", model.Id);

            command.ExecuteNonQuery();
        }
        #endregion

        #region - Delete OK -

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

            aUser.Id = reader.GetInt32(startingIndex++);
            aUser.UserName = reader.GetString(startingIndex++);
            aUser.FirstName = reader.GetString(startingIndex++);
            aUser.MiddleName = reader.GetString(startingIndex++);
            aUser.LastName = reader.GetString(startingIndex++);
            aUser.Email = reader.GetString(startingIndex++);
            aUser.Password = reader.GetString(startingIndex++);
            aUser.ProfilePicture = reader.GetString(startingIndex++);
            aUser.DateRegistered = reader.GetDateTime(startingIndex++);
            aUser.Age = reader.GetInt32(startingIndex++);
            aUser.RoleId = reader.GetInt32(startingIndex++);
            aUser.IsDeleted = reader.GetBoolean(startingIndex++);

            return aUser;
        }
        #endregion

        #region - Add Params - OK - 
        private static void AddCommonParams(UserAddRequest model, SqlParameterCollection col)
        {
            col.AddWithValue("@UserName", model.UserName);
            col.AddWithValue("@FirstName", model.FirstName);
            col.AddWithValue("@MiddleName", model.MiddleName);
            col.AddWithValue("LastName", model.LastName);
            col.AddWithValue("@Email", model.Email);
            col.AddWithValue("@Password", model.Password);
            col.AddWithValue("@ProfilePicture", model.ProfilePicture);
            col.AddWithValue("@Age", model.Age);
            col.AddWithValue("@RoleId", model.RoleId);
        }
        #endregion
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
