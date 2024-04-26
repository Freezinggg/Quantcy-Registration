using System.ComponentModel.DataAnnotations;

namespace Quantcy_Registration.Models.Login
{
    public class LoginModel
    {
        public LoginModel()
        {
            usernameLogin = "";
            passwordLogin = "";
        }
        [Required(ErrorMessage = "Username cannot be empty.")]
        public string usernameLogin { get; set; } = "";

        [Required(ErrorMessage = "Password cannot be empty.")]
        public string passwordLogin { get; set; } = "";
    }
}
