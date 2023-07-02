using TestToSQL.Dtos;

namespace TestToSQL.Services
{
    public interface IUsersService
    {
        int CreateUser(UserAddRequest model);
        List<Users> GetAll();
    }
}