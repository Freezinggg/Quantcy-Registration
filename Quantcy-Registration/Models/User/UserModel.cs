using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Quantcy_Registration.Models.Register;

namespace Quantcy_Registration.Models.User
{
    public class UserModel
    {
        //public List<RegisterModel> registerModels { get; set; } = new();
        public string fullName { get; set; } = "";

        public string username { get; set; } = "";

        public string password { get; set; } = "";
    }
}
