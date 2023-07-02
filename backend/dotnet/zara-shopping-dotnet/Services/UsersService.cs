﻿using Azure;
using Humanizer;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Identity.Client.Extensions.Msal;
using NuGet.Common;
using System.Buffers.Text;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Runtime.Intrinsics.X86;
using System.Security.Principal;
using System.Threading;
using TestToSQL.Dtos;
using TestToSQL.Interfaces;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using TestToSQL.Models;

namespace TestToSQL.Services
{
    public class UsersService : IUsersService
    {
        private readonly string connectionString; // connectionString from appSettings.json

        public UsersService(string connectionString)
        {
            this.connectionString = connectionString; // assigns the connectionString parameter to the readonly field
        }

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