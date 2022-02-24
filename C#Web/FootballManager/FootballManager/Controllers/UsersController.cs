using BasicWebServer.Server.Attributes;
using BasicWebServer.Server.Controllers;
using BasicWebServer.Server.HTTP;
using FootballManager.Constrains;
using FootballManager.ViewModels;

namespace FootballManager.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserService userService;
        public UsersController(Request request, IUserService _userService) : base(request)
        {
            userService = _userService;
        }
        public Response Login()
        {
            if (User.IsAuthenticated)
            {
                return Redirect("/");
            }
            return View(new { IsAuthenticated = false });
        }
        [HttpPost]
        public Response Login(LoginFormModel model)
        {
            if (User.IsAuthenticated)
            {
                return View("/");
            }

            Request.Session.Clear();

            var loging = userService.ValidateLoginForm(model);

            if (loging.userId == null)
            {
                return View(new { ErrorMessage = loging.errors, IsAuthenticated = false }, "/Error");
            }

            SignIn(loging.userId.ToString());
            CookieCollection cookies = new CookieCollection();
            cookies.Add(Session.SessionCookieName, Request.Session.Id);

            return Redirect("/Home/Index");
        }
        public Response Register()
        {
            return this.View(new { IsAuthenticated = false });
        }

        [HttpPost]
        public Response Register(RegisterFormModel model)
        {
            (bool isValid, string errors) = userService.ValidateRegisterForm(model);
            if (!isValid)
            {
                
                return View(new { ErrorMessage = errors, IsAuthenticated = false }, "/Error");
            }
            var createStatus = userService.CreateNewUser(model);
            if (!createStatus)
            {
                return View(new { ErrorMessage = errors, IsAuthenticated = false }, "/Error");
            }

            return Redirect("/Users/Login");
        }

        public Response Logout()
        {
            SignOut();
            return Redirect("/");
        }
    }
}
