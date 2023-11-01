
using Kalio.Domain.Roles;

namespace Kalio.Domain.Roles.Claims
{
    public class ClaimViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
       // public string Role { get; set; }
        public string RoleId { get; set; }
        public string ClaimType { get; set; }
    }
}
