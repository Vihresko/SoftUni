using Artillery.Data.Models.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Serialization;

namespace Artillery.DataProcessor.ImportDto
{
    [XmlType("Manufacturer")]
    public class ImportManufacturerDto
    {
        [XmlElement("ManufacturerName")]
        [Required]
        [MinLength(Constant.MANUFACTURER_NAME_MIN_LEN)]
        [MaxLength(Constant.MANUFACTURER_NAME_MAX_LEN)]
        public string ManufacturerName { get; set; }

        [XmlElement("Founded")]
        [Required]
        [MinLength(Constant.FOUNDED_MIN_LEN)]
        [MaxLength(Constant.FOUNDED_MAX_LEN)]
        public string Founded { get; set; }
    }
}
