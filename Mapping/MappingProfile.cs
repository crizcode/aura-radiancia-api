using AutoMapper;
using Domain.Entities;
using Infraestructure.Shared;
namespace Persistence.Mapping;

 public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Product, ProductDto>().ReverseMap();
        CreateMap<Category, CategoryDto>().ReverseMap();
        CreateMap<Supplier, SupplierDto>().ReverseMap();
        CreateMap<Person, PersonDto>().ReverseMap();
    }
}