namespace CarDealer.App
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml;
    using System.Xml.Serialization;
    using AutoMapper;
    using Data;
    using Dto.Import;
    using Dto.Export;
    using Models;
    using ValidationContext = System.ComponentModel.DataAnnotations.ValidationContext;

    public class StartUp
    {
        public static void Main()
        {
            CarDealerContext dbContext = new CarDealerContext();
            MapperConfiguration config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<CarDealerProfile>();
            });

            IMapper mapper = config.CreateMapper();

            using (dbContext)
            {
                ImportSuppliers(dbContext, mapper);
                ImportParts(dbContext, mapper);
                ImportCars(dbContext, mapper);
                ImportPartCars(dbContext);
                ImportCustomers(dbContext, mapper);
                ImportSales(dbContext);

                GetCarsWithDistance(dbContext);
                GetAllFerrariCars(dbContext);
                GetLocalSuppliers(dbContext);
                GetCarsWithParts(dbContext);
                GetTotalSalesByCustomers(dbContext);
                GetSalesWithDiscount(dbContext);
            }
        }

        private static void GetSalesWithDiscount(CarDealerContext dbContext)
        {
            SalesExportDto[] sales = dbContext
                .Sales
                .Select(s => new SalesExportDto()
                {
                    Make = s.Car.Make,
                    Model = s.Car.Model,
                    TravelledDistance = s.Car.TravelledDistance,
                    CustomerName = s.Customer.Name,
                    Price = s.Car.PartCars.Select(pc => pc.Part.Price).Sum(),
                    Discount = s.Discount
                })
                .ToArray();

            StringBuilder sb = new StringBuilder();
            XmlSerializerNamespaces xmlNamespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
            XmlSerializer serializer = new XmlSerializer(typeof(SalesExportDto[]), new XmlRootAttribute("sales"));
            serializer.Serialize(new StringWriter(sb), sales, xmlNamespaces);

            File.WriteAllText("../../../Files/Export/sales-discounts.xml", sb.ToString());
        }

        private static void GetTotalSalesByCustomers(CarDealerContext dbContext)
        {
            CustomerExportDto[] customers = dbContext
                .Customers
                .Where(c => c.Sales.Count >= 1)
                .Select(c => new CustomerExportDto()
                {
                    Name = c.Name,
                    BoughtCars = c.Sales.Count,
                    SpentMoney = c.Sales.Select(s => s.Car.PartCars.Select(pc => pc.Part).Sum(pc => pc.Price)).Sum()
                })
                .ToArray();

            StringBuilder sb = new StringBuilder();
            XmlSerializerNamespaces xmlNamespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
            XmlSerializer serializer = new XmlSerializer(typeof(CustomerExportDto[]), new XmlRootAttribute("customers"));
            serializer.Serialize(new StringWriter(sb), customers, xmlNamespaces);

            File.WriteAllText("../../../Files/Export/customers-total-sales.xml", sb.ToString());
        }

        private static void GetCarsWithParts(CarDealerContext dbContext)
        {
            CarPartsDto[] cars = dbContext
                .Cars
                .Select(c => new CarPartsDto()
                {
                    Made = c.Make,
                    Model = c.Model,
                    TravelledDistance = c.TravelledDistance,
                    PartExportDtos = c.PartCars
                        .Select(pc => new PartExportDto()
                        {
                            Name = pc.Part.Name,
                            Price = pc.Part.Price
                        })
                        .ToArray()
                })
                .ToArray();

            StringBuilder sb = new StringBuilder();
            XmlSerializerNamespaces xmlNamespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
            XmlSerializer serializer = new XmlSerializer(typeof(CarPartsDto[]), new XmlRootAttribute("cars"));
            serializer.Serialize(new StringWriter(sb), cars, xmlNamespaces);

            File.WriteAllText("../../../Files/Export/cars-and-parts.xml", sb.ToString());
        }

        private static void GetLocalSuppliers(CarDealerContext dbContext)
        {
            LocalSupplierDto[] suppliers = dbContext
                .Suppliers
                .Select(s => new LocalSupplierDto()
                {
                    Id = s.Id,
                    Name = s.Name,
                    PartsCount = s.Parts.Count
                })
                .ToArray();

            StringBuilder sb = new StringBuilder();
            XmlSerializerNamespaces xmlNamespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
            XmlSerializer serializer = new XmlSerializer(typeof(LocalSupplierDto[]), new XmlRootAttribute("suppliers"));
            serializer.Serialize(new StringWriter(sb), suppliers, xmlNamespaces);

            File.WriteAllText("../../../Files/Export/local-suppliers.xml", sb.ToString());
        }

        private static void GetAllFerrariCars(CarDealerContext dbContext)
        {
            CarFerrariDto[] cars = dbContext
                .Cars
                .Where(c => c.Make == "Ferrari")
                .Select(c => new CarFerrariDto()
                {
                    Id = c.Id,
                    Model = c.Model,
                    TravelledDistance = c.TravelledDistance
                })
                .ToArray();

            StringBuilder sb = new StringBuilder();
            XmlSerializerNamespaces xmlNamespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
            XmlSerializer serializer = new XmlSerializer(typeof(CarFerrariDto[]), new XmlRootAttribute("cars"));
            serializer.Serialize(new StringWriter(sb), cars, xmlNamespaces);

            File.WriteAllText("../../../Files/Export/ferrari-cars.xml", sb.ToString());
        }

        private static void GetCarsWithDistance(CarDealerContext dbContext)
        {
            CarExportDto[] cars = dbContext
                .Cars
                .Where(c => c.TravelledDistance >= 2000000)
                .Select(c => new CarExportDto()
                {
                    Make = c.Make,
                    Model = c.Model,
                    TravelledDistance = c.TravelledDistance
                })
                .OrderBy(dto => dto.Make)
                .ThenBy(dto => dto.Model)
                .ToArray();

            StringBuilder sb = new StringBuilder();
            XmlSerializerNamespaces xmlNamespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
            XmlSerializer serializer = new XmlSerializer(typeof(CarExportDto[]), new XmlRootAttribute("cars"));
            serializer.Serialize(new StringWriter(sb), cars, xmlNamespaces);

            File.WriteAllText("../../../Files/Export/cars-distance.xml", sb.ToString());
        }

        private static void ImportSales(CarDealerContext dbContext)
        {
            int[] discounts = new[] { 0, 5, 10, 15, 20, 30, 40, 50 };
            int[] carIds = dbContext
                .Cars
                .Select(c => c.Id)
                .ToArray();

            int[] customerIds = dbContext
                .Customers
                .Select(c => c.Id)
                .ToArray();

            List<Sale> sales = new List<Sale>();
            Random random = new Random();
            for (int i = 0; i < carIds.Length; i++)
            {
                int carId = random.Next(1, carIds.Length);
                int customerId = random.Next(1, customerIds.Length);
                int discount = discounts[random.Next(0, discounts.Length)];

                Sale sale = new Sale()
                {
                    Discount = discount,
                    CarId = carId,
                    CustomerId = customerId
                };

                sales.Add(sale);
            }

            dbContext.Sales.AddRange(sales);
            dbContext.SaveChanges();
        }

        private static void ImportCustomers(CarDealerContext dbContext, IMapper mapper)
        {
            string xmlString = File.ReadAllText("../../../Files/Import/customers.xml");
            XmlSerializer serializer = new XmlSerializer(typeof(CustomerImportDto[]), new XmlRootAttribute("customers"));
            CustomerImportDto[] deserializedCustomers = (CustomerImportDto[])serializer.Deserialize(new StringReader(xmlString));

            List<Customer> customers = new List<Customer>();
            foreach (CustomerImportDto customerDto in deserializedCustomers)
            {
                if (!IsValid(customerDto))
                {
                    continue;
                }

                Customer customer = mapper.Map<Customer>(customerDto);
                customers.Add(customer);
            }

            dbContext.Customers.AddRange(customers);
            dbContext.SaveChanges();
        }

        private static void ImportPartCars(CarDealerContext dbContext)
        {
            int numberOfCars = dbContext.Cars.Count();

            List<PartCar> partCars = new List<PartCar>();
            for (int i = 1; i <= numberOfCars; i++)
            {
                PartCar partCar = new PartCar();
                partCar.CarId = i;
                partCar.PartId = new Random().Next(1, 132);

                partCars.Add(partCar);
            }

            dbContext.PartCars.AddRange(partCars);
            dbContext.SaveChanges();
        }

        private static void ImportCars(CarDealerContext dbContext, IMapper mapper)
        {
            string xmlString = File.ReadAllText("../../../Files/Import/cars.xml");
            XmlSerializer serializer = new XmlSerializer(typeof(CarImportDto[]), new XmlRootAttribute("cars"));
            CarImportDto[] deserialedCarsImport = (CarImportDto[])serializer.Deserialize(new StringReader(xmlString));

            List<Car> cars = new List<Car>();
            foreach (CarImportDto carDto in deserialedCarsImport)
            {
                if (!IsValid(carDto))
                {
                    continue;
                }

                Car car = mapper.Map<Car>(carDto);
                cars.Add(car);
            }

            dbContext.Cars.AddRange(cars);
            dbContext.SaveChanges();
        }

        private static void ImportParts(CarDealerContext dbContext, IMapper mapper)
        {
            string xmlString = File.ReadAllText("../../../Files/Import/parts.xml");
            XmlSerializer serializer = new XmlSerializer(typeof(PartImportDto[]), new XmlRootAttribute("parts"));
            PartImportDto[] deserialedParts = (PartImportDto[])serializer.Deserialize(new StringReader(xmlString));

            List<Part> parts = new List<Part>();
            foreach (PartImportDto partDto in deserialedParts)
            {
                if (!IsValid(partDto))
                {
                    continue;
                }

                Part part = mapper.Map<Part>(partDto);
                int supplierId = new Random().Next(1, 32);
                part.SupplierId = supplierId;
                parts.Add(part);
            }

            dbContext.Parts.AddRange(parts);
            dbContext.SaveChanges();
        }

        private static void ImportSuppliers(CarDealerContext dbContext, IMapper mapper)
        {
            string xmlString = File.ReadAllText("../../../Files/Import/suppliers.xml");
            XmlSerializer serializer = new XmlSerializer(typeof(SupplierImportDto[]), new XmlRootAttribute("suppliers"));
            SupplierImportDto[] deserializedSuppliers = (SupplierImportDto[])serializer.Deserialize(new StringReader(xmlString));

            List<Supplier> suppliers = new List<Supplier>();
            foreach (SupplierImportDto supplierDto in deserializedSuppliers)
            {
                if (!IsValid(supplierDto))
                {
                    continue;
                }

                Supplier supplier = mapper.Map<Supplier>(supplierDto);
                suppliers.Add(supplier);
            }

            dbContext.Suppliers.AddRange(suppliers);
            dbContext.SaveChanges();
        }

        public static bool IsValid(object obj)
        {
            ValidationContext validationContext = new ValidationContext(obj);
            List<ValidationResult> validationResults = new List<ValidationResult>();

            return Validator.TryValidateObject(obj, validationContext, validationResults, true);
        }
    }
}