using IDP.Api.Domain.Dto;
using IDP.Api.Domain.IRepository.Command.Base;
using static System.Net.WebRequestMethods;

namespace IDP.Api.Domain.IRepository.Command
{
    public interface IOtpRedisRepository : ICommandRepository<Otp>
    {
        Task<Otp> Getdata(string mobile);

    }
}
