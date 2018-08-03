using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using ProductShop.App.Dto;
using ProductShop.App.Dto.Import;
using ProductShop.Models;

namespace ProductShop.App
{
    public class ProductShopProfile:Profile
    {
        public ProductShopProfile()
        {
            CreateMap<UserDto, User>();
            CreateMap<ProductDto, Product>();
            CreateMap<CategoryDto, Category>();
        }
    }
}
