using AutoMapper;
using IDP.Api.Application.Auth.Command;

namespace IDP.Api.Application.Helper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Add as many of these lines as you need to map your objects
            CreateMap<AuthCommand, IDP.Api.Domain.Entities.User>().ReverseMap();

        }
    }
}
