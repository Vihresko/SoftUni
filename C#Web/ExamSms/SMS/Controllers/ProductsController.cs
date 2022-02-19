using BasicWebServer.Server.Attributes;
using BasicWebServer.Server.Controllers;
using BasicWebServer.Server.HTTP;
using SMS.Models;
using SMS.Services;

namespace SMS.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductService productService;
        public ProductsController(Request request, IProductService _productService) : base(request)
        {
            this.productService = _productService;
        }

        public Response Create()
        {
            if (!User.IsAuthenticated)
            {
                return Redirect("/");
            }
            return View(new  {IsAuthenticated = true });
        }

        [HttpPost]
        public Response Create(ProductForm productForm)
        {
            if (!User.IsAuthenticated)
            {
                return Redirect("/Index");
            }
            var (isValid, errors) = productService.ValidateProductForm(productForm);
            if (!isValid)
            {
                return View(new {ErrorMessage = errors}, "/Error");
            }

            (isValid, errors) = productService.ValidateProductForm(productForm);
            if (!isValid)
            {
                return View(new { ErrorMessage = errors }, "/Error");
            }
            
            productService.CreateProduct(productForm);
            return View(new  {IsAuthenticated = true });
        }

    }
}
