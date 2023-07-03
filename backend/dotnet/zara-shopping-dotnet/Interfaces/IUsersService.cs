using Microsoft.Data.SqlClient;
using ZaraShopping.Dtos;
using ZaraShopping.Responses;

namespace ZaraShopping.Interfaces
{
    public interface IUsersService
    {
        int CreateUser(UserAddRequest model);
        void Delete(int id);
        List<Users> GetAll();
        ItemResponse<Users> GetById(int id);
        void UpdateUser(UserUpdateRequest model);
    }
}