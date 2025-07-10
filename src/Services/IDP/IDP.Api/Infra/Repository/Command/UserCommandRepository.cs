using IDP.Api.Domain.Entities;
using IDP.Api.Domain.IRepository.Command;
using IDP.Api.Infra.Data;

namespace IDP.Api.Infra.Repository.Command
{
    public class UserCommandRepository : CommandRepository<User>, IUserCommandRepository
    {
        private readonly IdpCommandDbContext shopCommandDbContext;

        public UserCommandRepository(IdpCommandDbContext context) : base(context)
        {
            shopCommandDbContext = context;

        }

    }
}
