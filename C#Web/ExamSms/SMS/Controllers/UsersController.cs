using BasicWebServer.Server.Attributes;
using BasicWebServer.Server.Controllers;
using BasicWebServer.Server.HTTP;
using SMS.Models;
using SMS.Services;

namespace SMS.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserService userService;
        public UsersController(Request request, IUserService _userService)
            : base(request)
        {
            this.userService = _userService;
        }

        public Response Login()
        {
            if (User.IsAuthenticated)
            {
                return Redirect("/");
            }
            return View(new {IsAuthenticated = false });
        }

        [HttpPost]
        public Response Login(User user)
        {
            if (User.IsAuthenticated)
            {
                return View("/");
            }

            Request.Session.Clear();

            var loging = userService.ValidateLoginForm(user);

            if (loging.userId == null)
            {
                return View(new { ErrorMessage = loging.errors, IsAuthenticated = false }, "/Error");
            }
            SignIn(loging.userId.ToString());
            CookieCollection cookies = new CookieCollection();
            cookies.Add(Session.SessionCookieName,Request.Session.Id);

            return Redirect("/");
        }
        public Response Register()
        {
            return View();
        }

        [HttpPost]
        public Response Register(User userPostForm)
        {
            var validator = userService.ValidateRegisterForm(userPostForm);
            if (!validator.isValid)
            {
                return View(new { ErrorMessage = validator.errors, IsAuthenticated = false }, "/Error");
            }
            userService.CreateNewUser(userPostForm);

            return Redirect("/Users/Login");
        }

        public Response Logout()
        {
            SignOut();
            return Redirect("/");
        }

    }
}
