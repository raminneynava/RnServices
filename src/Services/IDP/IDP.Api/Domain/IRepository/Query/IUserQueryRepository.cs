using IDP.Api.Domain.Entities;

namespace IDP.Api.Domain.IRepository.Query
{
    public interface IUserQueryRepository
    {
        Task<User> GetUserAsync(string mobilenumber);
    }
}
