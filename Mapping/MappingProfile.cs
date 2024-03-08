using AutoMapper;
using Domain.Entities;
using Shared;

namespace Persistence
{
     public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<Supplier, SupplierDto>().ReverseMap();
        }
    }
}