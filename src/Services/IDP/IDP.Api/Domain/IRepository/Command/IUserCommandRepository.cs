using IDP.Api.Domain.Entities;
using IDP.Api.Domain.IRepository.Command.Base;

namespace IDP.Api.Domain.IRepository.Command
{
    public interface IUserCommandRepository : ICommandRepository<User>
    {

    }
}
