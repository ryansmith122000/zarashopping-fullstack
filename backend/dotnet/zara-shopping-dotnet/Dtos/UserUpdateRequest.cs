using ZaraShopping.Dtos;

namespace ZaraShopping.Dtos
{
    public class UserUpdateRequest : UserAddRequest
    {
        public int Id { get; set; }
    }
}
