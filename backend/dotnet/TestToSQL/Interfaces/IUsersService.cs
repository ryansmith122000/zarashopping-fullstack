using TestToSQL.Dtos;

namespace TestToSQL.Interfaces
{
    public interface IUsersService
    {
        int CreateUser(UserAddRequest model);
    }
}