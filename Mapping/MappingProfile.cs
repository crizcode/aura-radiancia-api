using AutoMapper;
using Domain.Entities;
using Shared;

namespace Persistence
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductDto>()
             .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category != null ? src.Category.Name : null))
             .ForMember(dest => dest.SupplierName, opt => opt.MapFrom(src => src.Supplier != null ? src.Supplier.Name : null))
             .ForMember(dest => dest.Descripcion, opt => opt.MapFrom(src => src.Descripcion))
             .ForMember(dest => dest.Precio, opt => opt.MapFrom(src => src.Precio))
             .ForMember(dest => dest.Stock, opt => opt.MapFrom(src => src.Stock))
             .ForMember(dest => dest.CreationDate, opt => opt.MapFrom(src => src.CreationDate));

            CreateMap<ProductDto, Product>()
                .ForMember(dest => dest.Category, opt => opt.Ignore())
                .ForMember(dest => dest.Supplier, opt => opt.Ignore())
                .ForMember(dest => dest.Descripcion, opt => opt.MapFrom(src => src.Descripcion))
                .ForMember(dest => dest.Precio, opt => opt.MapFrom(src => src.Precio))
                .ForMember(dest => dest.Stock, opt => opt.MapFrom(src => src.Stock))
                .ForMember(dest => dest.CreationDate, opt => opt.MapFrom(src => src.CreationDate));




            CreateMap<Category, CategoryDto>();
            CreateMap<CategoryDto, Category>();

            CreateMap<Supplier, SupplierDto>();
            CreateMap<SupplierDto, Supplier>();

        }
    }
}
