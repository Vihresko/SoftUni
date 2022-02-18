using BasicWebServer.Server.Attributes;
using BasicWebServer.Server.Controllers;
using BasicWebServer.Server.HTTP;
using SMS.Models;
using SMS.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            var model = productService.ListAllProductsInCart(User.Id);
            return View(model, "/Carts/Details");
        }

        public Response Buy()
        {
            productService.DeleteProductsWhenBuy(User.Id);
            return Redirect("/");
        }
    }
}
