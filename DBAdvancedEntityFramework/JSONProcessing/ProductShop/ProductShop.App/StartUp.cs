using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace ProductShop.App
{
    using AutoMapper;

    using Data;
    using Models;

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

            //ImportUsers(context);
            //ImportProducts(context);
            //ImportCategories(context);
            //GenerateCategoryProducts(context);

            //ProductsInRange(context);
            //GetSoldProducts(context);
            //GetCategoriesByProductsCount(context);
            //GetUsersAndProducts(context);

        }

        private static void GetUsersAndProducts(ProductShopContext ctx)
        {
            var users = new
            {
                usersCount = ctx.Users.Where(u => u.ProductsSold.Count > 0 && u.ProductsSold.Any(p => p.Buyer != null)).Count(),
                users = ctx.Users.Where(u => u.ProductsSold.Count > 0 && u.ProductsSold.Any(p => p.Buyer != null))
                    .OrderByDescending(u => u.ProductsSold.Count)
                    .ThenBy(u => u.LastName)
                    .Select(u => new
                    {
                        firstName = u.FirstName,
                        lastName = u.LastName,
                        age = u.Age,
                        soldProducts = new
                        {
                            count = u.ProductsSold.Count,
                            products = u.ProductsSold.Select(p => new
                            {
                                name = p.Name,
                                price = p.Price
                            }).ToArray()
                        }
                    }).ToArray()
            };

            var jsonUsers = JsonConvert.SerializeObject(users, new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                NullValueHandling = NullValueHandling.Ignore
            });

            File.WriteAllText("../../../Json/users-and-products.json", jsonUsers);
        }

        private static void GetCategoriesByProductsCount(ProductShopContext ctx)
        {
            var categories = ctx.Categories
                .Select(c => new
                {
                    category = c.Name,
                    productsCount = c.CategoryProducts.Count,
                    averagePrice = c.CategoryProducts.Sum(p => p.Product.Price) / c.CategoryProducts.Count,
                    totalRevenue = c.CategoryProducts.Sum(p => p.Product.Price)
                })
                .OrderByDescending(c => c.productsCount)
                .ToArray();

            var jsonCategories = JsonConvert.SerializeObject(categories, Formatting.Indented);

            File.WriteAllText("../../../Json/categories-by-products.json", jsonCategories);
        }

        private static void GetSoldProducts(ProductShopContext ctx)
        {
            var users = ctx.Users.Where(u => u.ProductsSold.Count >= 1)
                .OrderBy(u => u.LastName)
                .ThenBy(u => u.FirstName)
                .Select(u => new
                {
                    firstName = u.FirstName,
                    lastName = u.LastName,
                    soldProducts = u.ProductsSold.Where(p => p.Buyer != null)
                        .Select(p => new
                        {
                            name = p.Name,
                            price = p.Price,
                            buyerFirstName = p.Buyer.FirstName,
                            buyerLastName = p.Buyer.LastName
                        }).ToArray()
                })
                .ToArray();

            var jsonUsers = JsonConvert.SerializeObject(users, new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                NullValueHandling = NullValueHandling.Ignore
            });

            File.WriteAllText("../../../Json/users-sold-products.json", jsonUsers);
        }

        private static void ProductsInRange(ProductShopContext context)
        {
            var products = context.Products.Where(x => x.Price >= 500 && x.Price <= 1000).OrderBy(x => x.Price)
                .Select(x => new
                {
                    name = x.Name,
                    price = x.Price,
                    seller = x.Seller.FirstName + " " + x.Seller.LastName ?? x.Seller.LastName
                }).ToArray();

            var jsonProducts = JsonConvert.SerializeObject(products, Formatting.Indented);

            File.WriteAllText("../../../Json/products-in-range.json", jsonProducts);
        }

        private static void GenerateCategoryProducts(ProductShopContext context)
        {
            int[] productIds = context.Products.Select(x => x.Id).ToArray();
            int[] categoryIds = context.Categories.Select(x => x.Id).ToArray();

            var rnd = new Random();

            List<CategoryProduct> categoryProducts = new List<CategoryProduct>();

            foreach (var productId in productIds)
            {
                for (int i = 0; i < 3; i++)
                {
                    int index = rnd.Next(0, categoryIds.Length);
                    while (categoryProducts.Any(cp => cp.ProductId == productId && cp.CategoryId == categoryIds[index]))
                    {
                        index = rnd.Next(0, categoryIds.Length);
                    }

                    CategoryProduct categoryProduct = new CategoryProduct()
                    {
                        ProductId = productId,
                        CategoryId = categoryIds[index]
                    };

                    categoryProducts.Add(categoryProduct);
                }
            }

            context.CategoryProducts.AddRange(categoryProducts);
            context.SaveChanges();
        }

        private static void ImportCategories(ProductShopContext context)
        {
            var jsonString = File.ReadAllText("../../../Json/categories.json");

            var deserializedCategories = JsonConvert.DeserializeObject<Category[]>(jsonString);

            List<Category> categories = new List<Category>();

            foreach (var category in deserializedCategories)
            {
                if (IsValid(category))
                {
                    categories.Add(category);
                }
            }

            context.Categories.AddRange(categories);
            context.SaveChanges();
        }

        private static void ImportProducts(ProductShopContext context)
        {
            var jsonString = File.ReadAllText("../../../Json/products.json");

            var deserializedProducts = JsonConvert.DeserializeObject<Product[]>(jsonString);

            List<Product> products = new List<Product>();

            foreach (var product in deserializedProducts)
            {
                if (!IsValid(product))
                {
                    continue;
                }

                var selerId = new Random().Next(57, 99);
                var buyerId = new Random().Next(99, 113);

                var rnd = new Random().Next(1, 4);

                product.SellerId = selerId;
                product.BuyerId = buyerId;

                if (rnd == 3)
                {
                    product.BuyerId = null;
                }

                products.Add(product);
            }

            context.Products.AddRange(products);
            context.SaveChanges();
        }

        private static void ImportUsers(ProductShopContext context)
        {
            var jsonString = File.ReadAllText("../../../Json/users.json");

            var deserializedUsers = JsonConvert.DeserializeObject<User[]>(jsonString);

            List<User> users = new List<User>();

            foreach (var user in deserializedUsers)
            {
                if (IsValid(user))
                {
                    users.Add(user);
                }
            }

            context.Users.AddRange(users);
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
