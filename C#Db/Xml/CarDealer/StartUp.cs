namespace CarDealer
{
    using CarDealer.Data;
    using CarDealer.DTO.ExportDtos;
    using CarDealer.DTO.ImportDtos;
    using CarDealer.Models;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;
    using ExportCarForSaleDto = DTO.ImportDtos.ExportCarForSaleDto;

    public class StartUp
    {
        public static void Main(string[] args)
        {
            CarDealerContext context = new CarDealerContext();
            // context.Database.EnsureDeleted();
            // context.Database.EnsureCreated();
            // Console.WriteLine("Succsesfully delete and then create database.");
            //
            // string supliersFromXml = File.ReadAllText("../../../Datasets/suppliers.xml");
            // Console.WriteLine(ImportSuppliers(context, supliersFromXml));
            // string partsXml = File.ReadAllText("../../../Datasets/parts.xml");
            // Console.WriteLine(ImportParts(context, partsXml));
            // string carsXml = File.ReadAllText("../../../Datasets/cars.xml");
            // Console.WriteLine(ImportCars(context, carsXml));
            // string customersXml = File.ReadAllText("../../../Datasets/customers.xml");
            // Console.WriteLine(ImportCustomers(context, customersXml));
            // string salesXml = File.ReadAllText("../../../Datasets/sales.xml");
            // Console.WriteLine(ImportSales(context, salesXml));
            //Console.WriteLine(GetCarsWithDistance(context));
            Console.WriteLine(GetSalesWithAppliedDiscount(context));
        }

        public static string ImportSuppliers(CarDealerContext context, string inputXml)
        {
            XmlRootAttribute xmlRoot = new XmlRootAttribute("Suppliers");
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(ImportSuplierDto[]), xmlRoot);
            using StringReader stringReader = new StringReader(inputXml);
            ImportSuplierDto[] dtos = (ImportSuplierDto[])xmlSerializer.Deserialize(stringReader);
            ICollection<Supplier> suppliers = new HashSet<Supplier>();
            foreach (ImportSuplierDto supplier in dtos)
            {
                Supplier s = new Supplier()
                {
                    Name = supplier.Name,
                    IsImporter = bool.Parse(supplier.IsImporter)
                };
                suppliers.Add(s);
            }
            context.Suppliers.AddRange(suppliers);
            context.SaveChanges();
            return $"Successfully imported {suppliers.Count}";
        }

        public static string ImportParts(CarDealerContext context, string inputXml)
        {
            XmlRootAttribute root = new XmlRootAttribute("Parts"); //set root for serializator
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(ImportPartDto[]), root); //settings of serializator
            using StringReader stringReader = new StringReader(inputXml); //to read Xml
            ImportPartDto[] dtos = (ImportPartDto[])xmlSerializer.Deserialize(stringReader); //xml desirizlie to array
            ICollection<Part> parts = new HashSet<Part>();     //To collect all dtos
            int[] availablSuppIds = context.Suppliers.Select(s => s.Id).ToArray();
            foreach (ImportPartDto part in dtos)
            {
                Part p = new Part()
                {
                    Name = part.Name,
                    Price = decimal.Parse(part.Price),
                    Quantity = int.Parse(part.Quantity),
                    SupplierId = int.Parse(part.SupplierId)
                };

                if (availablSuppIds.Contains(p.SupplierId))
                {
                    parts.Add(p);
                }
            }
            context.Parts.AddRange(parts);
            context.SaveChanges();

            return $"Successfully imported {parts.Count}";        
        }

        public static string ImportCars(CarDealerContext context, string inputXml)
        {
            //Here must be ImportCarDtos but it bugs when copy file to exportDto !!!
            XmlRootAttribute root = new XmlRootAttribute("Cars");
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(ExportCarForSaleDto[]),root);
            using StringReader reader = new StringReader(inputXml);
            ExportCarForSaleDto[] carsDtos = (ExportCarForSaleDto[])xmlSerializer.Deserialize(reader);
            ICollection<Car> cars = new HashSet<Car>();

            foreach (var carDto in carsDtos)
            {
                Car car = new Car()
                {
                    Make = carDto.Make,
                    Model = carDto.Model,
                    TravelledDistance = carDto.TraveledDistance
                };

                ICollection<PartCar> carParts = new HashSet<PartCar>();
                foreach (int partId in carDto.Parts.Select(p => p.Id).Distinct())
                {
                    Part part = context.Parts.Find(partId);
                    if(part == null)
                    {
                        continue;
                    }
                    PartCar partCar = new PartCar()
                    {
                        Car = car,
                        Part = part
                    };
                    carParts.Add(partCar);
                }
                car.PartCars = carParts;
                cars.Add(car);
            }
            context.Cars.AddRange(cars);
            context.SaveChanges();
            return $"Successfully imported {cars.Count}";
        }

        public static string ImportCustomers(CarDealerContext context, string inputXml)
        {
            XmlRootAttribute root = new XmlRootAttribute("Customers");
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(ImportCustomerDto[]),root);
            using StringReader reader = new StringReader(inputXml);
            ImportCustomerDto[] dtos = (ImportCustomerDto[])xmlSerializer.Deserialize(reader);
            ICollection<Customer> customers = new HashSet<Customer>();
            foreach (var dto in dtos)
            {
                Customer c = new Customer()
                {
                    Name = dto.Name,
                    BirthDate = dto.BirthDate,
                    IsYoungDriver = bool.Parse(dto.IsYoungDriver)
                };
                customers.Add(c);
            }
            context.Customers.AddRange(customers);
            context.SaveChanges();
            return $"Successfully imported {customers.Count}";
        }

        public static string ImportSales(CarDealerContext context, string inputXml)
        {
            XmlRootAttribute root = new XmlRootAttribute("Sales");
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(ImportSaleDto[]), root);
            StringReader reader = new StringReader(inputXml);
            ImportSaleDto[] dtos = (ImportSaleDto[])xmlSerializer.Deserialize(reader);
            ICollection<Sale> sales = new HashSet<Sale>();

            foreach (var dto in dtos)
            {
                Car needCar = context.Cars.Find(dto.CarId);
                if(needCar == null)
                {
                    continue;
                }
                Sale s = new Sale()
                {
                    Car = needCar,
                    CustomerId = dto.CustomerId,
                    Discount = dto.Discount
                };
                sales.Add(s);
            }
            context.Sales.AddRange(sales);
            context.SaveChanges();
            return $"Successfully imported {sales.Count}";
        }

        public static string GetCarsWithDistance(CarDealerContext context)
        {
            XmlRootAttribute root = new XmlRootAttribute("cars");
            XmlSerializer xmlserializer = new XmlSerializer(typeof(ExportCarDto[]), root);

            XmlSerializerNamespaces xmlSerializerNamespaces = new XmlSerializerNamespaces();
            xmlSerializerNamespaces.Add(String.Empty, String.Empty); //Remove something in the begining

            StringBuilder sb = new StringBuilder(); //StringWriter-a need it
            using StringWriter writer = new StringWriter(sb);

            ExportCarDto[] carsDtos = context.Cars.Where(d => d.TravelledDistance > 2000000).OrderBy(m => m.Make).ThenBy(m => m.Model).Take(10).Select(c => new ExportCarDto()
            {
                Make = c.Make,
                Model = c.Model,
                TravelledDistance = c.TravelledDistance.ToString()
            }).ToArray();

            xmlserializer.Serialize(writer, carsDtos, xmlSerializerNamespaces); //write in StringBuilder
            return sb.ToString();
        }

        public static string GetSalesWithAppliedDiscount(CarDealerContext context)
        {
            StringBuilder sb = new StringBuilder();
            StringWriter writer = new StringWriter(sb);

            XmlRootAttribute root = new XmlRootAttribute("sales");
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(SaleWithDiscountDto[]),root);

            XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
            namespaces.Add(String.Empty, String.Empty);

            SaleWithDiscountDto[] sales = context.Sales.Select(s => new SaleWithDiscountDto
            {
                Car = new DTO.ExportDtos.ExportCarForSaleDto()
                {
                   Make = s.Car.Make,
                   Model = s.Car.Model,
                   TravelledDistance = s.Car.TravelledDistance.ToString()
                },
                CustomerName = s.Customer.Name,
                Discount = s.Discount.ToString("f2"),
                Price = s.Car.PartCars.Sum(pc => pc.Part.Price).ToString("f2"),
                PriceWithDiscount = ((s.Car.PartCars.Sum(pc => pc.Part.Price)) -((s.Car.PartCars.Sum(pc => pc.Part.Price)) * (s.Discount/100))).ToString("f2")
            }).ToArray();

            xmlSerializer.Serialize(writer, sales, namespaces);
            return sb.ToString();
        }
    }
}