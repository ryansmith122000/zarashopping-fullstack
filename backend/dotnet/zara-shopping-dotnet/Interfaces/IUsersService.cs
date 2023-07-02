using ZaraShopping.Dtos;
using ZaraShopping.Responses;

namespace ZaraShopping.Interfaces
{
    public interface IUsersService
    {
        int CreateUser(UserAddRequest model);
        List<Users> GetAll();
        ItemResponse<Users> GetById(int id);
    }
}