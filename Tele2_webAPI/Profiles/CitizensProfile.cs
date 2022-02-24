using Tele2API.Models;
using Tele2API.DTO;
using AutoMapper;

namespace Tele2API.Profiles
{
    public class CitizensProfile : Profile
    {
        public CitizensProfile()
        {
            CreateMap<Citizen, CitizenDTO>();
        }
    }
}
