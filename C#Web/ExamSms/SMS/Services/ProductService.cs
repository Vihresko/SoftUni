using Microsoft.EntityFrameworkCore;
using SMS.Data;
using SMS.Data.DbModels;
using SMS.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SMS.Services
{
    using static SMS.Data.Constant;
    public class ProductService : IProductService
    {
        private readonly SMSDbContext data;
        public ProductService(SMSDbContext dbContext)
        {
            this.data = dbContext;
        }
        public void CreateProduct(ProductForm productForm)
        {
            Product newProduct = new Product()
            {
                Name = productForm.Name,
                Price = productForm.Price,
            };
            
            data.Products.Add(newProduct);
            data.SaveChanges();
        }

        public ProductPrintModelHead ReturnAllProductsForPrint(string userId)
        {
            var userName = data.Users.Where(u => u.Id == userId).Select(u => u.Username).FirstOrDefault();
            var userProducts = data.Products.ToArray();

            var allPrdouctsInfo = new List<ProductPrintModelBody>();
            foreach (var product in userProducts)
            {
                var Product = new ProductPrintModelBody()
                {
                    ProductName = product.Name,
                    ProductPrice = product.Price,
                    ProductId = product.Id,
                };
                allPrdouctsInfo.Add(Product);
            }

            var model = new ProductPrintModelHead()
            {
                   IsAuthenticated = true,
                   Username = userName,
                   Products = allPrdouctsInfo
            };

            return (model);
        }

        public (bool isValid, string errors) ValidateProductForm(ProductForm productForm)
        {
            bool isValid = true;
            StringBuilder errors = new StringBuilder();
            if(string.IsNullOrWhiteSpace(productForm.Name) ||productForm.Name.Length < PRODUCT_NAME_MIN_LENGTH || productForm.Name.Length > PRODUCT_NAME_MAX_LENGTH)
            {
                 isValid = false;
                 errors.AppendLine($"Product name must be between {PRODUCT_NAME_MIN_LENGTH} and {PRODUCT_NAME_MAX_LENGTH}  symbols!");
            }

            if(productForm.Price < PRICE_MIN_VALUE || productForm.Price > PRICE_MAX_VALUE)
            {
                isValid = false;
                errors.AppendLine($"Product price must be between {PRICE_MIN_VALUE} and {PRICE_MAX_VALUE} value!");
            }

            return (isValid, errors.ToString());
        }

        public void AddProductToCart(string productId, string userId)
        {
            var productForAdd = data.Products.FirstOrDefault(p => p.Id == productId);
            var cart = this.data.Carts.Where(c => c.User.Id == userId)
                                      .Include(c => c.Products)
                                      .FirstOrDefault();

            cart.Products.Add(productForAdd);           
            this.data.SaveChanges();
        }

        public ICollection<CartViewModel> ListAllProductsInCart(string userId)
        {
            var cartProduts = data.Products.Where(p => p.Cart.User.Id == userId).ToArray();
            var result = new List<CartViewModel>();
            foreach (var product in cartProduts)
            {
                var piece = new CartViewModel()
                {
                    ProductName = product.Name,
                    ProductPrice = product.Price
                };
                result.Add(piece);
            }
          return result;
        }

        public void DeleteProductsWhenBuy(string userId)
        {
            var user = data.Users.Where(u => u.Id == userId)
                                 .Include(u => u.Cart)
                                 .ThenInclude(c => c.Products)
                                 .FirstOrDefault();

            user.Cart.Products.Clear();
            data.SaveChanges();
        }
    }
}
