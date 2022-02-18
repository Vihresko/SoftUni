namespace SharedTrip.Controllers
{
    using BasicWebServer.Server.Attributes;
    using BasicWebServer.Server.Controllers;
    using BasicWebServer.Server.HTTP;
    using SharedTrip.Models;
    using SharedTrip.Services;

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

            return View(new {IsAuthenticated = false });
        }

        [HttpPost]
        public Response Login(LoginPostModel model)
        {
            if (User.IsAuthenticated)
            {
                return View("/");
            }

            Request.Session.Clear();

            (string userId, string errors) = userService.LoginValidate(model);
            if (userId == null)
            {
                return View(new { ErrorMessage = errors }, "/Error");
            }

            SignIn(userId);
            CookieCollection cookies = new CookieCollection();
            cookies.Add(Session.SessionCookieName, Request.Session.Id);
            return Redirect("/");
        }

        public Response Register()
        {
            if (User.IsAuthenticated)
            {
                return Redirect("/");
            }
            return View(new { IsAuthenticated = false });
        }

        [HttpPost]
        public Response Register(RegisterPostModel model)
        {
            (bool isValid, string errors) = userService.ValidateRegisterPostmodel(model);
            if (!isValid)
            {
                return View(new { ErrorMessage = errors }, "/Error");
            }
            (bool isAvailableUsername, string error) = userService.CreateUser(model);
            {
                if (!isAvailableUsername)
                {
                    return View(new { ErrorMessage = error }, "/Error");
                }
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
