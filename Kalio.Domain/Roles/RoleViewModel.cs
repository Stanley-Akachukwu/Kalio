

namespace Kalio.Domain.Roles
{
    public class RoleViewModel
    {
        //public RoleViewModel()
        //{
        //    UsersInRole = new List<(string UserId, string Email)>();
        //}
        public string RoleId { get; set; }
        public string RoleName { get; set; }
       // public List<(string UserId,  string Email)> UsersInRole { get; set; }
        public List<UsersInRole> UsersInRole { get; set; }
    }

    public class UsersInRole
    {
        public string UserId { get; set; }
        public string Email { get; set; }
    }
}
