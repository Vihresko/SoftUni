using BasicWebServer.Server.Controllers;
using BasicWebServer.Server.HTTP;
using SMS.Services;

namespace SMS.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductService productService;
        public HomeController(Request request, IProductService _productService) 
            : base(request)
        {
            productService = _productService;
        }

        public Response Index()
        {
            if (!User.IsAuthenticated)
            {
                return View(new { IsAuthenticated = false });
            }
            
            var models = productService.ReturnAllProductsForPrint(User.Id);
            return View(models, "/Home/IndexLoggedIn");
        }
       
    }
}