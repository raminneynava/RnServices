using IDP.Api.Domain.Entities;
using IDP.Api.Domain.IRepository.Query;
using IDP.Api.Infra.Data;

using Microsoft.EntityFrameworkCore;

namespace IDP.Api.Infra.Repository.Query
{
    public class UserQueryRepository : IUserQueryRepository
    {
        private readonly IdpQueryDbContext _db;
        public UserQueryRepository(IdpQueryDbContext shopQueryDbContext)
        {
            _db = shopQueryDbContext;
        }
        public async Task<User> GetUserAsync(string mobilenumber)
        {
            var userfound = await _db.Tbl_Users.FirstOrDefaultAsync(p => p.MobileNumber == mobilenumber);
            return userfound;
        }
    }
}
