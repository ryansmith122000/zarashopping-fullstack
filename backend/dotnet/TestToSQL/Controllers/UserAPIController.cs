using BCrypt.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TestToSQL.Dtos;
using TestToSQL.Interfaces;
using TestToSQL.Models;
using TestToSQL.Responses;
using TestToSQL.Utilities;

namespace TestToSQL.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserAPIController : BaseAPIController
    {
        private readonly IUsersService _service;
        private readonly ILogger<UserAPIController> _logger;

        public UserAPIController(IUsersService service, ILogger<UserAPIController> logger) : base(logger)
        {
            _service = service;
            _logger = logger;
        }

        #region - Post OK -

        [HttpPost]
        public ActionResult<ItemResponse<int>> CreateUser(UserAddRequest model)
        {
            ActionResult<ItemResponse<int>> result = null;

            try
            {

                if (!DateFormatValidator.IsValidDateFormat(model.DateOfBirth))
                {
                    string errorMessage = "Please enter a date in the correct format: MM/DD/YYYY, DD/MM/YYYY, or YYYY/MM/DD";
                    result = BadRequest(errorMessage);
                }
                else if (model.RoleId == 0)
                {
                    string errorMessage = "Please choose an appropriate role.";
                    result = BadRequest(errorMessage);
                }
                else
                {
                    string salt = BCrypt.Net.BCrypt.GenerateSalt();
                    string hashedPassword = BCrypt.Net.BCrypt.HashPassword(model.Password, salt);
                    model.Password = hashedPassword;

                    int id = _service.CreateUser(model);
                    ItemResponse<int> response = new() { Item = id };

                    result = Created201(response);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                ErrorResponse response = new ErrorResponse(ex.Message);

                result = StatusCode(500, response);
            }

            return result;
        }

        #endregion
    }
}
