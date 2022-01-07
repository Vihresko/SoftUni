
namespace Artillery.DataProcessor
{
    using Artillery.Data;
    using Artillery.DataProcessor.ExportDto;
    using Newtonsoft.Json;
    using System;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;
    public class Serializer
    {
        public static string ExportShells(ArtilleryContext context, double shellWeight)
        {
            var result = context.Shells.Where(s => s.ShellWeight > shellWeight).ToArray()
                         .Select(s => new
                         {
                             ShellWeight = s.ShellWeight,
                             Caliber = s.Caliber,
                             Guns = s.Guns.Where(gt => gt.GunType.ToString() == "AntiAircraftGun").Select(g => new
                             {
                                 GunType = g.GunType.ToString(),
                                 GunWeight = g.GunWeight,
                                 BarrelLength = g.BarrelLength,
                                 Range = g.Range > 3000 ? "Long-range" : "Regular range"
                             }).OrderByDescending(g => g.GunWeight)
                         }).OrderBy(s => s.ShellWeight).ToArray();

            string json = JsonConvert.SerializeObject(result, Formatting.Indented);
            return json;
        }

        public static string ExportGuns(ArtilleryContext context, string manufacturer)
        {
            var result = context.Guns.Where(g => g.Manufacturer.ManufacturerName == manufacturer)
                 .Select(g => new ExportGunDto
                 {
                     Manufacturer = g.Manufacturer.ManufacturerName,
                     GunType = g.GunType.ToString(),
                     BarrelLength = g.BarrelLength,
                     GunWeight = g.GunWeight,
                     Range = g.Range,
                     Countries = g.CountriesGuns
                     .Where(cg => cg.Country.ArmySize > 4500000)
                     .Select(cg => new ExportGunCountrieDto
                     {
                         Country = cg.Country.CountryName,
                         ArmySize = cg.Country.ArmySize
                     }).OrderBy(cg => cg.ArmySize).ToArray()
                 }).OrderBy(g => g.BarrelLength).ToArray();

            XmlRootAttribute root = new XmlRootAttribute("Guns");
            XmlSerializer serializer = new XmlSerializer(typeof(ExportGunDto[]), root);
            XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
            namespaces.Add(string.Empty, string.Empty);
            StringBuilder sb = new StringBuilder();
            using StringWriter sw = new StringWriter(sb);
            serializer.Serialize(sw, result, namespaces);
            return sw.ToString();
        }
    }
}
