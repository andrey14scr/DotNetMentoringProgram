using AutoMapper;
using WebApi.DAL.Models;
using WebApi.DTO;

namespace WebApi.BL.Mapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<ProductDto, Product>().ReverseMap();
        CreateMap<CategoryDto, Category>().ReverseMap();
    }
}