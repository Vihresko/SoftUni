namespace Theatre.DataProcessor
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Theatre.Data;
    using System.Xml.Serialization;
    using Theatre.DataProcessor.ImportDto;
    using System.IO;
    using Theatre.Data.Models;
    using System.Text;
    using System.Globalization;
    using Theatre.Data.Models.Enums;
    using System.Linq;
    using Newtonsoft.Json;

    public class Deserializer
    {
        private const string ErrorMessage = "Invalid data!";

        private const string SuccessfulImportPlay
            = "Successfully imported {0} with genre {1} and a rating of {2}!";

        private const string SuccessfulImportActor
            = "Successfully imported actor {0} as a {1} character!";

        private const string SuccessfulImportTheatre
            = "Successfully imported theatre {0} with #{1} tickets!";

        public static string ImportPlays(TheatreContext context, string xmlString)
        {
            StringBuilder sb = new StringBuilder();

            XmlRootAttribute root = new XmlRootAttribute("Plays");
            XmlSerializer serializer = new XmlSerializer(typeof(ImportPlayDto[]), root);
            using StringReader sr = new StringReader(xmlString);

            ImportPlayDto[] playsDtos = (ImportPlayDto[])serializer.Deserialize(sr);

            HashSet<Play> plays = new HashSet<Play>();

            foreach (var dto in playsDtos)
            {
                if (!IsValid(dto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                bool isDurationValid = TimeSpan.TryParseExact(dto.Duration, "c", CultureInfo.InvariantCulture, TimeSpanStyles.None, out TimeSpan duration);

                if(!isDurationValid || duration.TotalHours < 1)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                bool isGenreValid = Enum.IsDefined(typeof(Genre), dto.Genre);
                if (!isGenreValid)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                Play play = new Play()
                {
                    Title = dto.Title,
                    Duration = duration,
                    Rating = dto.Rating,
                    Genre = (Genre)Enum.Parse(typeof(Genre), dto.Genre),
                    Description = dto.Description,
                    Screenwriter = dto.Screenwriter
                };

                plays.Add(play);
                sb.AppendLine($"Successfully imported {play.Title} with genre {play.Genre} and a rating of {play.Rating}!");
            }

            context.Plays.AddRange(plays);
            context.SaveChanges();
            return sb.ToString().TrimEnd();
        }

        public static string ImportCasts(TheatreContext context, string xmlString)
        {
            StringBuilder sb = new StringBuilder();

            XmlRootAttribute root = new XmlRootAttribute("Casts");
            XmlSerializer serializer = new XmlSerializer(typeof(ImportCast[]), root);
            using StringReader sr = new StringReader(xmlString);

            ImportCast[] castsDtos = (ImportCast[])serializer.Deserialize(sr);

            ICollection<Cast> casts = new HashSet<Cast>();
            foreach (var dto in castsDtos)
            {
                if (!IsValid(dto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                if(dto.IsMainCharacter.ToLower() != "true" && dto.IsMainCharacter.ToLower() != "false")
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }


                Cast cast = new Cast()
                {
                    FullName = dto.FullName,
                    IsMainCharacter = bool.Parse(dto.IsMainCharacter),
                    PhoneNumber = dto.PhoneNumber,
                    PlayId = dto.PlayId
                };
                string characterRole = cast.IsMainCharacter == false ? "lesser" : "main";
               
                casts.Add(cast);
                sb.AppendLine($"Successfully imported actor {cast.FullName} as a {characterRole} character!");
            }

            context.Casts.AddRange(casts);
            context.SaveChanges();
            return sb.ToString().TrimEnd();
        }

        public static string ImportTtheatersTickets(TheatreContext context, string jsonString)
        {
            StringBuilder sb = new StringBuilder();
            ImportProjectionDto[] projDtos = JsonConvert.DeserializeObject<ImportProjectionDto[]>(jsonString);

            ICollection<Theatre> theaters = new HashSet<Theatre>();

            foreach (var dto in projDtos)
            {
                if (!IsValid(dto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                Theatre theatre = new Theatre()
                {
                    Name = dto.Name,
                    NumberOfHalls = dto.NumberOfHalls,
                    Director = dto.Director,
                };

                foreach (var ticket in dto.Tickets)
                {
                    if (!IsValid(ticket))
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }
                    bool hasThatPlay = context.Plays.Any(p => p.Id == ticket.PlayId);
                    if (!hasThatPlay)
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }
                    theatre.Tickets.Add(new Ticket()
                    {
                        Price = ticket.Price,
                        RowNumber = ticket.RowNumber,
                        PlayId = ticket.PlayId
                    });
                }

                theaters.Add(theatre);
                sb.AppendLine($"Successfully imported theatre {theatre.Name} with #{theatre.Tickets.Count} tickets!");
            }

            context.Theatres.AddRange(theaters);
            context.SaveChanges();
            return sb.ToString().TrimEnd();
        }


        private static bool IsValid(object obj)
        {
            var validator = new ValidationContext(obj);
            var validationRes = new List<ValidationResult>();

            var result = Validator.TryValidateObject(obj, validator, validationRes, true);
            return result;
        }
    }
}
