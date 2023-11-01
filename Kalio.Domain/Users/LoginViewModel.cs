using System.ComponentModel.DataAnnotations;

namespace Kalio.Domain.Users
{
    
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 8)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
