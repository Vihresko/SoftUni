using Artillery.Data.Models.Constants;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Artillery.Data.Models
{
    public class Shell
    {
        public Shell()
        {
            this.Guns = new List<Gun>();
        }
        [Key]
        public int Id { get; set; }

        [Range(Constant.SHELL_WEIGHT_MIN_VALUE, Constant.SHELL_WEIGHT_MAX_VALUE)]
        public double ShellWeight { get; set; }

        [Required]
        [MaxLength(Constant.CALIBER_TEXT_MAX_LEN)]
        public string Caliber { get; set; }

        public ICollection<Gun> Guns { get; set; }
    }
}
