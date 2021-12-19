using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Serialization;

namespace Theatre.DataProcessor.ImportDto
{
    [XmlType("Cast")]
    public class ImportCast
    {
        [XmlElement("FullName")]
        [Required]
        [MinLength(4)]
        [MaxLength(30)]
        [StringLength(30, MinimumLength = 4)]
        public string FullName { get; set; }

        [XmlElement("IsMainCharacter")]
        [Required]
        public string IsMainCharacter { get; set; }

        [XmlElement("PhoneNumber")]
        [Required]
        [StringLength(15)]
        [RegularExpression(@"^[+]{1}44-[0-9]{2}-[0-9]{3}-[0-9]{4}$")]
        public string PhoneNumber { get; set; }

        [XmlElement("PlayId")]
        [Required]
        public int PlayId { get; set; }
    }
}
