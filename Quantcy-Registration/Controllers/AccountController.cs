using Database;
using Microsoft.AspNetCore.Mvc;
using Quantcy_Registration.Models.Login;
using Quantcy_Registration.Models.Register;
using Quantcy_Registration.Models.User;
using Quantcy_Registration.Security;

namespace Quantcy_Registration.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        private readonly UserContext _db;
        public AccountController(ILogger<AccountController> logger, UserContext db)
        {
            _logger = logger;
            _db = db;
        }

        public IActionResult Index()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("username"))) return View("/Views/Register/Register.cshtml");

            return View("/Views/Login/LoginDetails.cshtml");
        }

        public IActionResult RegisterIndex()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("username"))) return View("/Views/Register/Register.cshtml");

            return View("/Views/Register/Register.cshtml");
        }


        [HttpPost]
        public IActionResult Register(RegisterModel m)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    Users? u = _db.users.Where(x => x.username == m.username).FirstOrDefault();
                    if (u != null)
                    {
                        ModelState.AddModelError("Error", "Username already exist.");
                    }
                    else
                    {
                        u = new();
                        u.fullName = m.fullName;
                        u.username = m.username.ToLower();
                        u.password = Encrypt.Crypt(m.password);

                        _db.users.Add(u);
                        _db.SaveChanges();

                        ViewBag.Message = "Register success.";
                        return View("/Views/Login/Login.cshtml", new LoginModel());
                    }
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error", "Please refresh the web and try again.");
            }

            return View("/Views/Register/Register.cshtml", m);
        }

        [HttpPost]
        public IActionResult Login(LoginModel m)
        {
            if (ModelState.IsValid)
            {
                Users? u = _db.users.Where(x => x.username.ToLower() == m.usernameLogin.ToLower() && x.password == Encrypt.Crypt(m.passwordLogin)).FirstOrDefault();
                if (u != null)
                {
                    List<UserModel> listUserModel = new();
                    List<Users> listUsers = _db.users.ToList();

                    foreach (Users user in listUsers)
                    {
                        listUserModel.Add(new UserModel()
                        {
                            fullName = user.fullName,
                            username = user.username,
                            password = Encrypt.Decrypt(user.password)
                        });
                    }

                    return View("/Views/Login/LoginDetails.cshtml", listUserModel);
                }
                else
                {
                    ModelState.AddModelError("Error", "Username/password incorrect.");
                }
            }

            return View("/Views/Login/Login.cshtml", m);
        }


        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return View("/Views/Login/Login.cshtml");
        }
    }
}
