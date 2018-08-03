using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using AutoMapper;
using ProductShop.Data;
using ProductShop.Models;
using DataAnotations = System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Xml;
using ProductShop.App.Dto.Export;
using ProductShop.App.Dto.Import;
using ProductDto = ProductShop.App.Dto.Import.ProductDto;

namespace ProductShop.App
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ProductShopProfile>();

            });

            var mapper = config.CreateMapper();

            var context = new ProductShopContext();

            //ProcessUsers(mapper, context);
            //ProcessProducts(mapper, context);
            //ProcessCategories(mapper, context);
            //ProcessCategoryProducts(context);

            //ProductsInRange(context);
            //GetSoldProducts(context);
            //GetCategoriesByProductsCount(context);
            //GetUsersAndProducts(context);
            
        }

        private static void GetUsersAndProducts(ProductShopContext dbContext)
        {
            var users = new UserRootDto()
            {
                Count = dbContext.Users.Count(),
                Users = dbContext
                    .Users
                    .Where(u => u.ProductSold.Count >= 1)
                    .OrderByDescending(u => u.ProductSold.Count)
                    .Select(u => new UserExportDto()
                    {
                        FirstName = u.FirstName,
                        LastName = u.LastName,
                        Age = u.Age.ToString(),
                        Products = new ProductSoldRootDto()
                        {
                            Count = u.ProductSold.Count,
                            ProductSoldDtos = u.ProductSold.Select(s => new SoldProductDTO()
                                {
                                    Name = s.Name,
                                    Price = s.Price
                                })
                                .ToArray()
                        }
                    })
                    .ToArray()
            };

            StringBuilder sb = new StringBuilder();
            XmlSerializer serializer = new XmlSerializer(typeof(UserRootDto), new XmlRootAttribute("users"));
            XmlSerializerNamespaces xmlNamespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
            serializer.Serialize(new StringWriter(sb), users, xmlNamespaces);

            File.WriteAllText("../../../Xml//users-and-products.xml", sb.ToString());
        }

        private static void GetCategoriesByProductsCount(ProductShopContext dbContext)
        {
            CategoryExportDto[] categories = dbContext
                .Categories
                .Select(c => new CategoryExportDto()
                {
                    Name = c.Name,
                    ProductCount = c.CategoryProducts.Count,
                    AveragePrice = c.CategoryProducts.Average(cp => cp.Product.Price),
                    TotalRevenue = c.CategoryProducts.Sum(cp => cp.Product.Price)
                })
                .OrderByDescending(c => c.ProductCount)
                .ToArray();

            StringBuilder sb = new StringBuilder();
            XmlSerializer serializer = new XmlSerializer(typeof(CategoryExportDto[]), new XmlRootAttribute("categories"));
            XmlSerializerNamespaces xmlNamespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
            serializer.Serialize(new StringWriter(sb), categories, xmlNamespaces);

            File.WriteAllText("../../../Xml/categories-by-products.xml", sb.ToString());
        }

        private static void GetSoldProducts(ProductShopContext dbContext)
        {
            SellerUserDto[] users = dbContext
                .Users
                .Where(u => u.ProductSold.Count >= 1)
                .Select(u => new SellerUserDto()
                {
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Products = u.ProductSold
                        .Select(ps => new SoldProductDTO()
                        {
                            Name = ps.Name,
                            Price = ps.Price
                        })
                        .ToArray()
                })
                .OrderBy(u => u.LastName)
                .ThenBy(u => u.FirstName)
                .ToArray();

            StringBuilder sb = new StringBuilder();
            XmlSerializer serializer = new XmlSerializer(typeof(SellerUserDto[]), new XmlRootAttribute("users"));
            XmlSerializerNamespaces xmlNamespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
            serializer.Serialize(new StringWriter(sb), users, xmlNamespaces);

            File.WriteAllText("../../../Xml/users-sold-products.xml", sb.ToString());
        }

        private static void ProductsInRange(ProductShopContext context)
        {
            var products = context.Products.Where(x => x.Price >= 1000 && x.Price <= 2000 && x.Buyer != null)
                .OrderByDescending(p => p.Price).Select(x => new Dto.Export.ProductDto
                {
                    Name = x.Name,
                    Price = x.Price,
                    Buyer = x.Buyer.FirstName + " " + x.Buyer.LastName ?? x.Buyer.LastName
                }).ToArray();

            var xmlNamespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
            var serializer = new XmlSerializer(typeof(Dto.Export.ProductDto[]), new XmlRootAttribute("products"));
            var sb = new StringBuilder();
            serializer.Serialize(new StringWriter(sb), products, xmlNamespaces);

            File.WriteAllText("../../../Xml/products-in-range.xml", sb.ToString());
        }

        private static void ProcessCategoryProducts(ProductShopContext dbContext)
        {
            int[] productIds = dbContext
                .Products
                .Select(p => p.Id)
                .ToArray();

            int[] categoryIds = dbContext
                .Categories
                .Select(c => c.Id)
                .ToArray();

            Random random = new Random();
            List<CategoryProduct> categoryProducts = new List<CategoryProduct>();
            foreach (int product in productIds)
            {
                for (int i = 0; i < 3; i++)
                {
                    int index = random.Next(0, categoryIds.Length);
                    while (categoryProducts.Any(cp => cp.ProductId == product && cp.CategoryId == categoryIds[index]))
                    {
                        index = random.Next(0, categoryIds.Length);
                    }

                    CategoryProduct categoryProduct = new CategoryProduct() { CategoryId = categoryIds[index], ProductId = product };
                    categoryProducts.Add(categoryProduct);
                }
            }

            dbContext.CategoryProducts.AddRange(categoryProducts);
            dbContext.SaveChanges();
        }

        private static void ProcessCategories(IMapper mapper, ProductShopContext context)
        {
            string xmlString = File.ReadAllText("../../../Xml/categories.xml");
            XmlSerializer serializer = new XmlSerializer(typeof(CategoryDto[]), new XmlRootAttribute("categories"));
            CategoryDto[] deserializedCategories = (CategoryDto[])serializer.Deserialize(new StringReader(xmlString));

            List<Category> categories = new List<Category>();
            foreach (CategoryDto categoryDto in deserializedCategories)
            {
                if (!isValid(categoryDto))
                {
                    continue;
                }

                Category category = mapper.Map<Category>(categoryDto);
                categories.Add(category);
            }

            context.Categories.AddRange(categories);
            context.SaveChanges();
        }

        private static void ProcessUsers(IMapper mapper, ProductShopContext context)
        {
            var xmlString = File.ReadAllText("../../../Xml/users.xml");

            var serializer = new XmlSerializer(typeof(UserDto[]), new XmlRootAttribute("users"));

            var deserializedUsers = (UserDto[])serializer.Deserialize(new StringReader(xmlString));

            List<User> users = new List<User>();

            foreach (var userDto in deserializedUsers)
            {
                if (!isValid(userDto))
                {
                    continue;
                }

                var user = mapper.Map<User>(userDto);

                users.Add(user);
            }


            context.Users.AddRange(users);
            context.SaveChanges();
        }

        private static void ProcessProducts(IMapper mapper, ProductShopContext context)
        {
            var xmlString = File.ReadAllText("../../../Xml/products.xml");

            var serializer = new XmlSerializer(typeof(ProductDto[]), new XmlRootAttribute("products"));

            var deserializedProducts = (ProductDto[])serializer.Deserialize(new StringReader(xmlString));

            List<Product> products = new List<Product>();

            var counter = 1;

            foreach (var productDto in deserializedProducts)
            {
                if (!isValid(productDto))
                {
                    continue;
                }

                var product = mapper.Map<Product>(productDto);

                var buyerId = new Random().Next(1, 30);
                var sellerId = new Random().Next(31, 56);

                product.BuyerId = buyerId;
                product.SellerId = sellerId;

                if (counter == 4)
                {
                    product.BuyerId = null;
                    counter = 0;
                }

                products.Add(product);

                counter++;
            }


            context.Products.AddRange(products);
            context.SaveChanges();
        }

        public static bool isValid(object obj)
        {
            var validationContext = new DataAnotations.ValidationContext(obj);
            var validationResults = new List<DataAnotations.ValidationResult>();
            return DataAnotations.Validator.TryValidateObject(obj, validationContext, validationResults, true);
        }
    }
}
