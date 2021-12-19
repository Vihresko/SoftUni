using System.Xml.Serialization;

namespace CarDealer.DTO.ImportDtos
{
    [XmlType("partId")]
    public class ImportCarPartDto
    {
        [XmlAttribute("id")]
        public int Id { get; set; }
    }
}
