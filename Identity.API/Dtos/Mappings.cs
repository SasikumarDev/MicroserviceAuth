using AutoMapper;
using Identity.API.Models;

namespace Identity.API.Dtos;

public class Mappings : Profile
{
    public Mappings()
    {
        CreateMap<appUserVmAdd, appUsers>().ReverseMap();
    }
}