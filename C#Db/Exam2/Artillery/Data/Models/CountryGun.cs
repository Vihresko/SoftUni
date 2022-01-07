using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Artillery.Data.Models
{
    public class CountryGun
    {
        [ForeignKey(nameof(Country))]
        public int CountryId { get; set; }
        [Required]
        public Country Country { get; set; }

        [ForeignKey(nameof(Gun))]
        public int GunId { get; set; }
        [Required]
        public Gun Gun { get; set; }
    }
}
