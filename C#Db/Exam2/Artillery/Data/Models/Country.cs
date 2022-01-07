using Artillery.Data.Models.Constants;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Artillery.Data.Models
{
    public class Country
    {
        public Country()
        {
            this.CountriesGuns = new List<CountryGun>();
        }
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(Constant.COUNTRY_NAME_MAX_LEN)]
        public string CountryName { get; set; }

        [Range(Constant.COUNTRY_ARMY_MIN_SIZE, Constant.COUNTRY_ARMY_MAX_SIZE)]
        public int ArmySize { get; set; }

        public ICollection<CountryGun> CountriesGuns { get; set; }
    }
}
