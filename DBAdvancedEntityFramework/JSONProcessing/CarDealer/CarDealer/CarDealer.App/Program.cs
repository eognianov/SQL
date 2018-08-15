using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using CarDealer.Data;
using CarDealer.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace CarDealer.App
{
    class Program
    {
        static void Main(string[] args)
        {
            var context = new CarDealerContext();


            //ImportSuppliers(context);

            //ImportParts(context);

            //ImportCars(context);

            //ImportPartCars(context);

            //ImportCustomers(context);

            //ImportSales(context);

            //GetOrderedCustomers(context);

            //GetCarsFromToyota(context);

            //GetLocalSuppliers(context);

            //GetCarsWithListOfParts(context);

            //GetSalesWithAppliedDiscount(context);

            //GetTotalSalesByCustomer(context);
        }

        private static void GetSalesWithAppliedDiscount(CarDealerContext ctx)
        {
            var sales = ctx.Sales.Select(s => new
            {
                car = new
                {
                    s.Car.Make,
                    s.Car.Model,
                    s.Car.TravelledDistance
                },
                customerName = s.Customer.Name,
                s.Discount,
                price = s.Car.PartCars.Sum(p => p.Part.Price),
                priceWithDiscount = (s.Car.PartCars.Sum(p => p.Part.Price)) * (Convert.ToDecimal(1 - s.Discount))
            }).ToArray();

            var jsonSales = JsonConvert.SerializeObject(sales, Formatting.Indented);

            File.WriteAllText("../../../Json/Outer/sales-discounts.json", jsonSales);
        }

        private static void GetTotalSalesByCustomer(CarDealerContext ctx)
        {
            var customers = ctx.Customers.Where(c => c.Sales.Count > 0)
                .ToArray()
                .Select(c => new
                {
                    fullName = c.Name,
                    boughtCars = c.Sales.Count,
                    spentMoney = c.Sales.Select(s => s.Car.PartCars.Select(p => p.Part.Price).Sum() * (decimal)(1 - s.Discount)).Sum()
                })
                .OrderByDescending(c => c.spentMoney)
                .ThenByDescending(c => c.boughtCars)
                .ToArray();

            var jsonCustomers = JsonConvert.SerializeObject(customers, Formatting.Indented);

            File.WriteAllText("../../../Json/Outer/customers-total-sales.json", jsonCustomers);
        }

        private static void GetCarsWithListOfParts(CarDealerContext ctx)
        {
            var cars = ctx.Cars.Select(c => new
                {
                    car = new
                    {
                        c.Make,
                        c.Model,
                        c.TravelledDistance
                    },
                    parts = c.PartCars.Select(p => new
                    {
                        p.Part.Name,
                        p.Part.Price
                    }).ToArray()
                })
                .ToArray();

            var jsonCars = JsonConvert.SerializeObject(cars, Formatting.Indented);

            File.WriteAllText("../../../Json/Outer/cars-and-parts.json", jsonCars);
        }


        private static void GetLocalSuppliers(CarDealerContext ctx)
        {
            var suppliers = ctx.Suppliers.Where(s => !s.IsImporter)
                .Select(s => new
                {
                    s.Id,
                    s.Name,
                    PartsCount = s.Parts.Count
                }).ToArray();

            var jsonSuppliers = JsonConvert.SerializeObject(suppliers, Formatting.Indented);

            File.WriteAllText("../../../Json/Outer/local-suppliers.json", jsonSuppliers);
        }

        private static void GetCarsFromToyota(CarDealerContext ctx)
        {
            var cars = ctx.Cars.Where(c => c.Make == "Toyota")
                .OrderBy(c => c.Model)
                .ThenByDescending(c => c.TravelledDistance)
                .Select(c => new
                {
                    c.Id,
                    c.Make,
                    c.Model,
                    c.TravelledDistance
                }).ToArray();

            var jsonCars = JsonConvert.SerializeObject(cars, Formatting.Indented);

            File.WriteAllText("../../../Json/Outer/toyota-cars.json", jsonCars);
        }

        private static void GetOrderedCustomers(CarDealerContext ctx)
        {
            var customers = ctx.Customers
                .OrderBy(c => c.BirthDate)
                .OrderBy(c => c.IsYoungDriver)
                .Select(c => new
                {
                    Id = c.Id,
                    Name = c.Name,
                    BirthDate = c.BirthDate,
                    IsYoungDriver = c.IsYoungDriver,
                    Sales = new List<string>()
                })
                .ToArray();

            var jsonCustomers = JsonConvert.SerializeObject(customers, Formatting.Indented);

            File.WriteAllText("../../../Json/Outer/ordered-customers.json", jsonCustomers);
        }

        private static void ImportSales(CarDealerContext context)
        {
            List<Sale> sales = new List<Sale>();
            List<Customer> customers = context.Customers.ToList();

            double[] discountValues = new[] { 0, 0.05, 0.1, 0.15, 0.2, 0.3, 0.4, 0.5 };
            var carIds = Enumerable.Range(1, 358).OrderBy(x => new Random().Next()).Take(80).ToArray();

            for (int i = 0; i < 80; i++)
            {
                int customerId = new Random().Next(1, 31);
                int carId = carIds[i];
                double discount = discountValues[new Random().Next(discountValues.Length)];

                if (customers.Single(c => c.Id == customerId).IsYoungDriver)
                {
                    discount += 0.05;
                }

                var sale = new Sale()
                {
                    Customer_Id = customerId,
                    Car_Id = carId,
                    Discount = (decimal)discount
                };

                sales.Add(sale);
            }

            context.Sales.AddRange(sales);
            context.SaveChanges();
        }

        private static void ImportCustomers(CarDealerContext context)
        {
            var jsonString = File.ReadAllText("../../../Json/customers.json");

            var deserializedCustomers = JsonConvert.DeserializeObject<Customer[]>(jsonString);

            List<Customer> customers = new List<Customer>();

            foreach (var customer in deserializedCustomers)
            {
                customers.Add(customer);
            }


            context.Customers.AddRange(customers);
            context.SaveChanges();

        }

        private static void ImportPartCars(CarDealerContext context)
        {
            List<PartCar> partCars = new List<PartCar>();

            for (int i = 1; i <= 358; i++)
            {
                var parts = Enumerable.Range(1, context.Parts.Count()).OrderBy(x => new Random().Next()).Take(new Random().Next(10, 21)).ToArray();

                for (int n = 0; n < parts.Length; n++)
                {
                    int partId = parts[n];

                    var partCar = new PartCar()
                    {
                        Car_Id = i,
                        Part_Id = partId
                    };

                    partCars.Add(partCar);
                }
            }

            
            context.PartCars.AddRange(partCars);
            context.SaveChanges();
        }

        private static void ImportCars(CarDealerContext context)
        {
            string jsonString = File.ReadAllText("../../../Json/cars.json");
            var deserializeCars = JsonConvert.DeserializeObject<Car[]>(jsonString);
            List<Car> cars = new List<Car>();

            foreach (var car in deserializeCars)
            {
                if (IsValid(car))
                {
                    cars.Add(car);
                }
                
            }

            context.Cars.AddRange(cars);
            context.SaveChanges();
        }

        private static void ImportParts(CarDealerContext context)
        {
            var jsonString = File.ReadAllText("../../../Json/parts.json");

            var deserializedParts = JsonConvert.DeserializeObject<Part[]>(jsonString);

            List<Part> parts = new List<Part>();

            foreach (var part in deserializedParts)
            {
                if (IsValid(part))
                {
                    int supplierId = new Random().Next(1, context.Suppliers.Count());
                    part.Supplier_Id = supplierId;
                    parts.Add(part);
                }
            }

            context.Parts.AddRange(parts);
            context.SaveChanges();
        }

        private static void ImportSuppliers(CarDealerContext context)
        {
            var jsonString = File.ReadAllText("../../../Json/suppliers.json");

            var deserializeSuppliers = JsonConvert.DeserializeObject<Supplier[]>(jsonString);

            List<Supplier> suppliers = new List<Supplier>();

            foreach (var supplier in deserializeSuppliers)
            {
                if (IsValid(supplier))
                {
                    suppliers.Add(supplier);
                }
            }

            context.Suppliers.AddRange(suppliers);
            context.SaveChanges();
        }

        public static bool IsValid(object obj)
        {
            var validationContext = new System.ComponentModel.DataAnnotations.ValidationContext(obj);
            var results = new List<ValidationResult>();

            return Validator.TryValidateObject(obj, validationContext, results, true);
        }
    }
}
