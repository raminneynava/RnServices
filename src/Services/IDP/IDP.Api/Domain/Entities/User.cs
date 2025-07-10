using IDP.Api.Domain.Entities.BaseEntites;

namespace IDP.Api.Domain.Entities
{
    public class User : baseEntity
    {

        public string Username { get; set; }

        public string? PasswordHash { get; set; }

        public string? Salt { get; set; }
        public string? MobileNumber { get; set; }
    }
}
