using Artillery.Data.Models.Constants;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace Artillery.DataProcessor.ImportDto
{
    [XmlType("Country")]
    public class ImportCountryDto
    {
        [XmlElement("CountryName")]
        [Required]
        [MinLength(Constant.COUNTRY_NAME_MIN_LEN)]
        [MaxLength(Constant.COUNTRY_NAME_MAX_LEN)]
        public string CountryName { get; set; }

        [XmlElement("ArmySize")]
        [Range(Constant.COUNTRY_ARMY_MIN_SIZE, Constant.COUNTRY_ARMY_MAX_SIZE)]
        public int ArmySize { get; set; }
    }
}
