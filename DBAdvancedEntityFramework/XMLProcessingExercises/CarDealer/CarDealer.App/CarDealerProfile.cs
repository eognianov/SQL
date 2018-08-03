namespace CarDealer.App
{
    using AutoMapper;
    using Dto.Import;
    using Models;

    public class CarDealerProfile : Profile
    {
        public CarDealerProfile()
        {
            this.CreateMap<SupplierImportDto, Supplier>();

            this.CreateMap<PartImportDto, Part>();

            this.CreateMap<CarImportDto, Car>();

            this.CreateMap<CustomerImportDto, Customer>();
        }
    }
}