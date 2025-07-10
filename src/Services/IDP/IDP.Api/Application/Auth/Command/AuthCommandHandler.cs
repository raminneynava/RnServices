using AutoMapper;
using IDP.Api.Domain.IRepository.Command;
using IDP.Api.Domain.IRepository.Query;

using IntegrationEvents;

using MassTransit;

using MediatR;

namespace IDP.Api.Application.Auth.Command
{
    public class AuthCommandHandler : IRequestHandler<AuthCommand, bool>
    {
        private readonly IOtpRedisRepository _otpRedisRepository;
        private readonly IUserCommandRepository _userCommandRepository;
        private readonly IUserQueryRepository _userQueryRepository;
        private readonly IMapper _mapper;
        //private readonly ICapPublisher _capBus;
        private readonly IPublishEndpoint _publishEndpoint;
        public AuthCommandHandler(IOtpRedisRepository otpRedisRepository,
            IUserCommandRepository userCommandRepository,
            IPublishEndpoint publishEndpoint,
            IUserQueryRepository userQueryRepository, IMapper mapper)
        {
            _otpRedisRepository = otpRedisRepository;
            _userCommandRepository = userCommandRepository;
            _userQueryRepository = userQueryRepository;
            _mapper = mapper;
            _publishEndpoint = publishEndpoint;
            //_capBus = capPublisher;
        }
        public async Task<bool> Handle(AuthCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var userobj = _mapper.Map<Domain.Entities.User>(request);
                var user = await _userQueryRepository.GetUserAsync(request.MobileNumber);
                if (user == null)
                {
                    Random random = new Random();
                    var code = random.Next(1000, 10000);
                    //ارسال پیامک به سرویس نوتفیکیشن
                    //await _capBus.PublishAsync<AuthCommand>("otpevent", new AuthCommand
                    //{
                    //    MobileNumber = request.MobileNumber,
                    //});
                    await _publishEndpoint.Publish<OtpEvent>(new OtpEvent
                    {

                        MobileNumber = request.MobileNumber,
                        OtpCode = code.ToString(),
                    });

                    userobj.Username = request.MobileNumber;
                    var res = await _userCommandRepository.Insert(userobj);
                    await _otpRedisRepository.Insert(new Domain.Dto.Otp { UserName = userobj.MobileNumber, OtpCode = code, IsUse = false });
                }
                else
                {
                    Random random = new Random();
                    var code = random.Next(1000, 10000);
                    //ارسال پیامک به سزویس نوتفیکیشن
                    //await _capBus.PublishAsync<AuthCommand>("otpevent", new AuthCommand
                    //{
                    //    MobileNumber = request.MobileNumber,
                    //});
                    await _publishEndpoint.Publish<OtpEvent>(new OtpEvent
                    {

                        MobileNumber = request.MobileNumber,
                        OtpCode = code.ToString(),
                    });
                    userobj.Username = request.MobileNumber;
                    await _otpRedisRepository.Insert(new Domain.Dto.Otp { UserName = user.MobileNumber, OtpCode = code, IsUse = false });
                }

                return true;
            }
            catch (Exception ex)
            {

                throw;
            }

        }
    }
}
