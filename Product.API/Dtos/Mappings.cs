using AutoMapper;

namespace Product.API.Dtos;

public class Mappings : Profile
{
    public Mappings()
    {
        CreateMap<ProductAddVm,Models.Product>()
        .ForMember(des => des.Name, moption => moption.MapFrom(src => src.Name))
        .ForMember(des => des.Description, moption => moption.MapFrom(src => src.Description))
        .ForMember(des => des.Price, moption => moption.MapFrom(src => src.Price))
        .ForMember(des => des.Images, moption => moption.Ignore())
        .ForMember(des => des.Createdby, moption => moption.Ignore())
        .ReverseMap();
    }
}