using System.Xml.Serialization;

namespace CarDealer.DTO.ImportDtos
{
    [XmlType("Supplier")]
    public class ImportSuplierDto
    {
        [XmlElement("name")]
        public string Name { get; set; }

        //InXml all is strings. Value will be parsed later.(In that case - boolean)
        [XmlElement("isImporter")]
        public string IsImporter { get; set; }
    }
}
