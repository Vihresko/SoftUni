using Artillery.Data.Models.Constants;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Artillery.Data.Models
{
    public class Manufacturer
    {
        public Manufacturer()
        {
            this.Guns = new List<Gun>();
        }
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(Constant.MANUFACTURER_NAME_MAX_LEN)]
        public string ManufacturerName  { get; set; }

        [Required]
        //TODO: Unique
        [MaxLength(Constant.FOUNDED_MAX_LEN)]
        public string Founded { get; set; }

        public ICollection<Gun> Guns { get; set; }
    }
}
