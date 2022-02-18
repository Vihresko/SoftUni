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
            return View();
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

            
            productService.CreateProduct(productForm, User.Id);
            return View(userLogStatus());
        }

        public object userLogStatus()
        {
            if (User.IsAuthenticated)
            {
                return new { IsAuthenticated = true };
            }
            return new { IsAuthenticated = false };
        }
    }
}
