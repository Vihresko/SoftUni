using Artillery.Data.Models.Constants;
using Artillery.Data.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Artillery.DataProcessor.ImportDto
{
    public class ImportGunDto
    {
        public int ManufacturerId { get; set; }

        [Range(Constant.GUN_WEIGHT_MIN_VALUE, Constant.GUN_WEIGHT_MAX_VALUE)]
        public int GunWeight { get; set; }

        [Range(Constant.BARREL_LEN_MIN_VALUE, Constant.BARREL_LEN_MAX_VALUE)]
        public double BarrelLength { get; set; }

        public int? NumberBuild { get; set; }

        [Range(Constant.GUN_MIN_RANGE, Constant.GUN_MAX_RANGE)]
        public int Range { get; set; }

        [Required]
        public string GunType { get; set; }

        public int ShellId { get; set; }

        public ImportGunCountriesDto[] Countries { get; set; }
    }
}
