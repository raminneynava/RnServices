using MediatR;

namespace IDP.Api.Application.User.Command.Insert;

public record UserInsertCommand() : IRequest<bool>
{
    public required string FullName { get; set; }

    public required string CodNumber { get; set; }
}
