using Artillery.Data.Models.Constants;
using Artillery.Data.Models.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Artillery.Data.Models
{
    public class Gun
    {
        public Gun()
        {
            this.CountriesGuns = new List<CountryGun>();
        }
        [Key]
        public int Id { get; set; }

        [ForeignKey(nameof(Manufacturer))]
        public int ManufacturerId { get; set; }

        [Required]
        public Manufacturer Manufacturer { get; set; }

        [Range(Constant.GUN_WEIGHT_MIN_VALUE, Constant.GUN_WEIGHT_MAX_VALUE)]
        public int GunWeight { get; set; }

        [Range(Constant.BARREL_LEN_MIN_VALUE, Constant.BARREL_LEN_MAX_VALUE)]
        public double BarrelLength { get; set; }

        public int? NumberBuild { get; set; }

        [Range(Constant.GUN_MIN_RANGE, Constant.GUN_MAX_RANGE)]
        public int Range  { get; set; }

        [Required]
        public GunType GunType { get; set; }

        [ForeignKey(nameof(Shell))]
        public int ShellId { get; set; }
        [Required]
        public Shell Shell { get; set; }

        public ICollection<CountryGun> CountriesGuns  { get; set; }
    }
}
