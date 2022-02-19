using BasicWebServer.Server.Controllers;
using BasicWebServer.Server.HTTP;
using SMS.Services;

namespace SMS.Controllers
{
    public class CartsController : Controller
    {
        private readonly IProductService productService;
        public CartsController(Request request, IProductService _productService) : base(request)
        {
            productService = _productService;
        }

        public Response AddProduct(string productId)
        {
            productService.AddProductToCart(productId, User.Id);
            return Redirect("/Carts/Details");
        }
        
        public Response Details()
        {
            var data = productService.ListAllProductsInCart(User.Id);
            var modelForView = new {Model = data, IsAuthenticated= true};
            return View(modelForView, "/Carts/Details");
        }

        public Response Buy()
        {
            productService.DeleteProductsWhenBuy(User.Id);
            return Redirect("/");
        }
    }
}
