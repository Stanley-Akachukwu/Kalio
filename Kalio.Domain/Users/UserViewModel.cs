
using System.Data;
using System.Security.Claims;

namespace Kalio.Domain.Users
{
    public class UserViewModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Role { get; set; }
        public List<string> Roles { get; set; }
        public string Email { get; set; }
        public List<Claim> Claims { get; set; }
    }
}
