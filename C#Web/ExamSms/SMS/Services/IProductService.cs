using SMS.Models;
using System.Collections.Generic;

namespace SMS.Services
{
    public interface IProductService
    {
        public (bool isValid, string errors) ValidateProductForm(ProductForm productForm);
        public void CreateProduct(ProductForm productForm);

        public ProductPrintModelHead ReturnAllProductsForPrint(string user);

        public void AddProductToCart(string productId, string userId);

        public ICollection<CartViewModel> ListAllProductsInCart(string userId);

        public void DeleteProductsWhenBuy(string userId);

    }
}
