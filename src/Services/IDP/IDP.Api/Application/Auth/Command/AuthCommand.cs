using MediatR;

namespace IDP.Api.Application.Auth.Command
{
    public class AuthCommand : IRequest<bool>
    {
        public required string MobileNumber { get; set; }
    }
}
