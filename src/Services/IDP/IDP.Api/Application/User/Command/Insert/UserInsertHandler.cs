using AutoMapper;

using IDP.Api.Domain.IRepository;

using MediatR;

namespace IDP.Api.Application.User.Command.Insert;


public record UserInsertHandler : IRequestHandler<UserInsertCommand, bool>
{


    //private readonly IMapper _mapper;
    //public UserInsertHandler(IMapper mapper)
    //{
    //    _mapper = mapper;
    //}
    public async Task<bool> Handle(UserInsertCommand request, CancellationToken cancellationToken)
    {

        //var model = _mapper.Map<Domain.Entities.User>(request);
        //var res = await _repository.Insert(model);
        return true;
    }
}

