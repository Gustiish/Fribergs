using System.ComponentModel.DataAnnotations;

namespace Contracts.DTO
{
    public sealed class CreateUserDTO
    {

        public required string Email { get; set; }
        public required string Password { get; set; }
        [Compare("Password", ErrorMessage = "Password do not match")]
        public required string ConfirmPassword { get; set; }
    }
}
