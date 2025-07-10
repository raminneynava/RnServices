using Auth;

using MediatR;

namespace IDP.Api.Application.Auth.Query
{
    public record AuthQuery : IRequest<JsonWebToken>
    {
        public required string MobileNumber { get; set; }
        public required int OtpCode { get; set; }
    }
}
