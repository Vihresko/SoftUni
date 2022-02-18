using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Models
{
    public class ProductPrintModelHead
    {
        //TODO:Validation?
        public string Username { get; set; }
        public bool IsAuthenticated { get; set; } = true;
        public ICollection<ProductPrintModelBody> Products { get; set; } = new List<ProductPrintModelBody>();
    }
    public class ProductPrintModelBody
    {
        //TODO:Validation?
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public string ProductId { get; set; }
    }
}
