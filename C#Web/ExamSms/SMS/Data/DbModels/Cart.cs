using System;
using System.Collections.Generic;

namespace SMS.Data.DbModels
{
    public class Cart
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public User User { get; set; }

        public ICollection<Product> Products { get; set; } = new HashSet<Product>();

    }
}
