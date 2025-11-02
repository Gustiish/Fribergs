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
        }

    }
}
