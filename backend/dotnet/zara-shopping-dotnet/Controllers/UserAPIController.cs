using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using ZaraShopping.Dtos;
using ZaraShopping.Interfaces;
using ZaraShopping.Responses;
using ZaraShopping.Utilities;

namespace ZaraShopping.Controllers
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

        #region - Update Not OK -
        [HttpPut]
        public ActionResult<SuccessResponse> Update(UserUpdateRequest model)
        {
            int code = 200;
            BaseResponse response = null;

            try
            {

                string salt = BCrypt.Net.BCrypt.GenerateSalt();
                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(model.Password, salt);
                model.Password = hashedPassword;

                _service.UpdateUser(model);

                response = new SuccessResponse();
            }

            catch (Exception ex)
            {
                code = 500;
                response = new ErrorResponse(ex.Message);
            }

            return StatusCode(code, response);
        }

        #endregion

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

        #region - Delete OK -
        [HttpDelete("{id:int}")]
        public ActionResult<SuccessResponse> DeleteById(int id)
        {
            int code = 200;
            BaseResponse response = null;

            try
            {
                _service.Delete(id);

                response = new SuccessResponse();
            }
            catch (Exception ex)
            {
                code = 500;
                response = new ErrorResponse(ex.Message);
            }
            return StatusCode(code, response);
        }
        #endregion

        #region - Get All OK -

        [HttpGet("all")]
        public ActionResult<ItemResponse<Users>> GetAll()
        {
            int iCode = 200;
            BaseResponse response = null;

            try
            {
                List<Users> list = _service.GetAll();

                if (list == null)
                {
                    iCode = 404;
                    response = new ErrorResponse("User(s) Not Found.");
                }
                else
                {
                    response = new ItemsResponse<Users> { Items = list };
                }
            }
            catch (Exception ex)
            {
                iCode = 500;
                base.Logger.LogError(ex.ToString());
                response = new ErrorResponse($"Generic Error: {ex.Message}");
            }
            return StatusCode(iCode, response);

        }
        #endregion

        #region - Get By Id Not OK -

        [HttpGet("{id:int}")]

        public ActionResult<ItemResponse<Users>> GetById(int id)
        {
            int iCode = 200;
            BaseResponse response = null;

            try
            {
                ItemResponse<Users> itemResponse = _service.GetById(id);
                if (itemResponse.Item == null)
                {
                    iCode = 404;
                    response = new ErrorResponse($"User(s) with the existing Id of {id} Not Found.");

                }
                else
                {
                    response = itemResponse;
                }
            }
            catch (Exception ex)
            {
                iCode = 500;
                base.Logger.LogError(ex.ToString());
                response = new ErrorResponse($"Generic Error: {ex.Message}");
            }
            return StatusCode(iCode, response);
        }
        #endregion

    } // end of controller, don't put anything here.
} // end of namespace, don't put anything here.