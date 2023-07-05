using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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

        #region - Post OK -
        [HttpPost("add")]
        public ActionResult<ItemResponse<int>> Create(UserAddRequest model)
        {
            ObjectResult result = null;

            try
            {
                int id = _service.CreateUser(model);
                ItemResponse<int> response = new ItemResponse<int>() { Item = id };

                result = Created201(response);
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

        #region - Get All OK -
        [HttpGet("getall")]
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

        #region - Get By Id OK -
        [HttpGet("getbyid/{id:int}")]

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

        #region - Update OK -
        [HttpPut("update/{id:int}")]
        public ActionResult<SuccessResponse> Update(int id, UserUpdateRequest model)
        {
            int code = 200;
            BaseResponse response = null;

            try
            {

                string salt = BCrypt.Net.BCrypt.GenerateSalt();
                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(model.Password, salt);
                model.Password = hashedPassword;

                _service.UpdateUser(id, model);

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

        #region - Delete OK -
        [HttpDelete("delete/{id:int}")]
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



    } // end of controller, don't put anything here.
} // end of namespace, don't put anything here.