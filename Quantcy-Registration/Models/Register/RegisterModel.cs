using System.ComponentModel.DataAnnotations;

namespace Quantcy_Registration.Models.Register
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Username cannot be empty.")]
        public string fullName { get; set; } = "";

        [Required(ErrorMessage = "Username cannot be empty.")]
        public string username { get; set; } = "";

        [Required(ErrorMessage = "Password cannot be empty.")]
        public string password { get; set; } = "";
    }
}
