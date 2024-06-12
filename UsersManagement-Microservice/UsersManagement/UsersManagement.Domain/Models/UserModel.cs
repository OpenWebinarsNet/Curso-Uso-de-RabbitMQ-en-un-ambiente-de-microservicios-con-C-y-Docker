using UsersManagement.Domain.Core.Models;

namespace UsersManagement.Domain.Models
{
    public class UserModel : BaseModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }
    }
}