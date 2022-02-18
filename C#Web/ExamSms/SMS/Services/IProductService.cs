using SMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Services
{
    public interface IProductService
    {
        public (bool isValid, string errors) ValidateProductForm(ProductForm productForm);
        public void CreateProduct(ProductForm productForm, string userId);

        public ProductPrintModelHead ReturnAllProductsForPrint(string user);

        public void AddProductToCart(string productId, string userId);

        public ICollection<CartViewModel> ListAllProductsInCart(string userId);

        public void DeleteProductsWhenBuy(string userId);

    }
}
