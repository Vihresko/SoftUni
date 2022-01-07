using Artillery.Data.Models.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Serialization;

namespace Artillery.DataProcessor.ImportDto
{
    [XmlType("Shell")]
    public class ImportShellDto
    {
        [XmlElement("ShellWeight")]
        [Range(Constant.SHELL_WEIGHT_MIN_VALUE, Constant.SHELL_WEIGHT_MAX_VALUE)]
        public double ShellWeight { get; set; }

        [XmlElement("Caliber")]
        [Required]
        [MinLength(Constant.CALIBER_TEXT_MIN_LEN)]
        [MaxLength(Constant.CALIBER_TEXT_MAX_LEN)]
        public string Caliber { get; set; }
    }
}
