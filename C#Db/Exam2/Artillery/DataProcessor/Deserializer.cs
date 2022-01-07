namespace Artillery.DataProcessor
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;
    using Artillery.Data;
    using Artillery.Data.Models;
    using Artillery.Data.Models.Enums;
    using Artillery.DataProcessor.ImportDto;
    using Newtonsoft.Json;

    public class Deserializer
    {
        private const string ErrorMessage =
                "Invalid data.";
        private const string SuccessfulImportCountry =
            "Successfully import {0} with {1} army personnel.";
        private const string SuccessfulImportManufacturer =
            "Successfully import manufacturer {0} founded in {1}.";
        private const string SuccessfulImportShell =
            "Successfully import shell caliber #{0} weight {1} kg.";
        private const string SuccessfulImportGun =
            "Successfully import gun {0} with a total weight of {1} kg. and barrel length of {2} m.";

        public static string ImportCountries(ArtilleryContext context, string xmlString)
        {
            XmlRootAttribute root = new XmlRootAttribute("Countries");
            XmlSerializer serializer = new XmlSerializer(typeof(ImportCountryDto[]), root);
            StringReader reader = new StringReader(xmlString);
            StringBuilder sb = new StringBuilder();
            var dtos = (ImportCountryDto[])serializer.Deserialize(reader);

            ICollection<Country> countries = new List<Country>();

            foreach (var dto in dtos)
            {
                if (!IsValid(dto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                Country country = new Country
                {
                    CountryName = dto.CountryName,
                    ArmySize = dto.ArmySize
                };
                countries.Add(country);
                sb.AppendFormat(SuccessfulImportCountry, country.CountryName, country.ArmySize);
                sb.AppendLine();
            }

            context.Countries.AddRange(countries);
            context.SaveChanges();
            return sb.ToString().TrimEnd();
        }

        public static string ImportManufacturers(ArtilleryContext context, string xmlString)
        {
            XmlRootAttribute root = new XmlRootAttribute("Manufacturers");
            XmlSerializer serializer = new XmlSerializer(typeof(ImportManufacturerDto[]), root);
            StringReader reader = new StringReader(xmlString);
            StringBuilder sb = new StringBuilder();
            ImportManufacturerDto[] dtos = (ImportManufacturerDto[])serializer.Deserialize(reader);

            ICollection<Manufacturer> manufacturers = new List<Manufacturer>();

            foreach (var dto in dtos)
            {
                if (!IsValid(dto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                Manufacturer manufacturer = new Manufacturer
                {
                    ManufacturerName = dto.ManufacturerName,
                    Founded = dto.Founded
                };

                if(manufacturers.Any(e=> e.ManufacturerName == manufacturer.ManufacturerName))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }
                manufacturers.Add(manufacturer);
                string[] foundedParts = manufacturer.Founded.Split(", ");
                string townName = foundedParts[foundedParts.Length - 2];
                string countryName = foundedParts[foundedParts.Length - 1];
                sb.AppendFormat(SuccessfulImportManufacturer, manufacturer.ManufacturerName, (townName + ", " + countryName));
                sb.AppendLine();
            }

            context.Manufacturers.AddRange(manufacturers);
            context.SaveChanges();
            return sb.ToString().TrimEnd();

        }

        public static string ImportShells(ArtilleryContext context, string xmlString)
        {
            XmlRootAttribute root = new XmlRootAttribute("Shells");
            XmlSerializer serializer = new XmlSerializer(typeof(ImportShellDto[]), root);
            StringReader reader = new StringReader(xmlString);
            StringBuilder sb = new StringBuilder();
            var dtos = (ImportShellDto[])serializer.Deserialize(reader);
            ICollection<Shell> shells = new List<Shell>();

            foreach (var dto in dtos)
            {
                if (!IsValid(dto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                Shell shell = new Shell
                {
                    ShellWeight = dto.ShellWeight,
                    Caliber = dto.Caliber
                };
                shells.Add(shell);
                sb.AppendFormat(SuccessfulImportShell, shell.Caliber, shell.ShellWeight);
                sb.AppendLine();
            }
            context.Shells.AddRange(shells);
            context.SaveChanges();
            return sb.ToString().TrimEnd();

        }

        public static string ImportGuns(ArtilleryContext context, string jsonString)
        {
            ImportGunDto[] dtos = JsonConvert.DeserializeObject<ImportGunDto[]>(jsonString);
            ICollection<Gun> guns = new List<Gun>();
            StringBuilder sb = new StringBuilder();

            foreach (var dto in dtos)
            {
                if (!IsValid(dto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                try
                {
                    GunType isValidEnum = (GunType)Enum.Parse(typeof(GunType), dto.GunType);
                }
                catch
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }
                
                Gun gun = new Gun
                {
                    ManufacturerId = dto.ManufacturerId,
                    GunWeight = dto.GunWeight,
                    BarrelLength = dto.BarrelLength,
                    NumberBuild = dto.NumberBuild,
                    Range = dto.Range,
                    GunType = (GunType)Enum.Parse(typeof(GunType), dto.GunType),
                    ShellId = dto.ShellId,

                };

                ICollection<CountryGun> cgs = new List<CountryGun>();
                foreach (var country in dto.Countries)
                {
                    CountryGun cg = new CountryGun
                    {
                        CountryId = country.Id,
                        Gun = gun
                    };
                    cgs.Add(cg);
                }

                gun.CountriesGuns = cgs;
                guns.Add(gun);
                sb.AppendFormat(SuccessfulImportGun, gun.GunType.ToString(), gun.GunWeight, gun.BarrelLength);
                sb.AppendLine();
            }

            context.Guns.AddRange(guns);
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
