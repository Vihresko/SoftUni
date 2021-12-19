using System.Xml.Serialization;

namespace CarDealer.DTO.ImportDtos
{
    [XmlType("Part")]
    public class ImportPartDto
    {
        [XmlElement("name")]
        public string Name { get; set; }

        [XmlElement("price")]
        public string Price { get; set; } //decimal

        [XmlElement("quantity")]

        public string Quantity { get; set; } //int

        [XmlElement("supplierId")]

        public string SupplierId { get; set; } //int


    }
}
