namespace Theatre.DataProcessor
{
    using Newtonsoft.Json;
    using System;
    using System.Linq;
    using Theatre.Data;
    using System.Xml.Serialization;
    using Theatre.DataProcessor.ExportDto;
    using System.Text;
    using System.IO;
    using System.Globalization;
    using Microsoft.EntityFrameworkCore;

    public class Serializer
    {
        public static string ExportTheatres(TheatreContext context, int numbersOfHalls)
        {
            var result = context.Theatres.Where(t => t.NumberOfHalls > numbersOfHalls)
                                         .Where(t => t.Tickets.Count >= 20)
                                         .ToArray()
                    .Select(t => new
                    {
                        Name = t.Name,
                        Halls = t.NumberOfHalls,
                        TotalIncome = t.Tickets.Where(t => t.RowNumber <= 5).Select(t => t.Price).Sum(),
                        Tickets = t.Tickets.Where(t => t.RowNumber <= 5).Select(t => new
                        {
                            Price = decimal.Parse(t.Price.ToString("f2")),
                            RowNumber = t.RowNumber
                        }).OrderByDescending(p => p.Price)

                    }).OrderByDescending(t => t.Halls).ThenBy(t => t.Name);

            string json = JsonConvert.SerializeObject(result, Formatting.Indented);
            return json.TrimEnd();
        }

        public static string ExportPlays(TheatreContext context, double rating)
        {
            XmlRootAttribute root = new XmlRootAttribute("Plays");
            XmlSerializer serializer = new XmlSerializer(typeof(ExportPlayDto[]), root);
            XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
            namespaces.Add(string.Empty, string.Empty);
            StringBuilder sb = new StringBuilder();
            using StringWriter writer = new StringWriter(sb);

            var result = context.Plays.ToArray().Where(p => p.Rating <= rating)
                      .Select(p => new ExportPlayDto()
                      {
                          Title = p.Title,
                          Duration = p.Duration.ToString("c", CultureInfo.InvariantCulture),
                          Rating = p.Rating == 0 ? "Premier" : p.Rating.ToString(),
                          Genre = p.Genre.ToString(),
                          Actors = p.Casts.Where(c => c.IsMainCharacter == true)
                                          .Select(a => new ExportPlayActorDto()
                                          {
                                              FullName = a.FullName,
                                              MainCharacter = $"Plays main character in '{a.Play.Title}'."
                                          }).OrderByDescending(a=> a.FullName).ToArray()
                      }).OrderBy(p => p.Title).ThenByDescending(p => p.Genre).ToArray();

            serializer.Serialize(writer, result, namespaces);
            return sb.ToString().TrimEnd();
        }
    }
}
