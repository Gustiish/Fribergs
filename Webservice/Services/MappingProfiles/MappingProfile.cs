using ApplicationCore.Entities.Identity;
using ApplicationCore.Entities.Models;
using AutoMapper;
using Contracts.DTO;

namespace Webservice.Services.MappingProfiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Car, CarDTO>().ReverseMap();
            CreateMap<CreateCarDTO, Car>();
            CreateMap<ApplicationUser, UserDTO>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ReverseMap();
        }

    }
}
