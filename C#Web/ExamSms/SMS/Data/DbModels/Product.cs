using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Data.DbModels
{
    public class Product
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        [MinLength(Constant.PRODUCT_NAME_MIN_LENGTH)]
        [MaxLength(Constant.PRODUCT_NAME_MAX_LENGTH)]
        public string Name { get; set; }

        [Range((double)Constant.PRICE_MIN_VALUE, (double)Constant.PRICE_MAX_VALUE)]
        public decimal Price { get; set; }

        [ForeignKey(nameof(Cart))]
        public string CartId { get; set; }
        public Cart Cart {get; set; }

    }
}
