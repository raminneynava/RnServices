using Asp.Versioning;

using IDP.Api.Application.Auth.Command;
using IDP.Api.Application.Auth.Query;

using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace IDP.Api.Controllers.V1
{
    [ApiController]
    [ApiVersion(2)]
    [ApiVersion(1)]
    [Route("api/v{v:apiVersion}/Auth")]
    public class AuthController : ControllerBase
    {
        public readonly IMediator _mediator;
        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] AuthQuery authQuery)
        {
            var res = await _mediator.Send(authQuery);
            return Ok(res);

        }
        /// <summary>
        /// ثبت نام و ارسال کد به شماره موبایل
        /// </summary>
        /// <param name="authQuery"></param>
        /// <returns></returns>
        [HttpPost("RegisterAndSendOtp")]
        public async Task<IActionResult> RegisterAndSendOtp([FromBody] AuthCommand authCommand)
        {
            var res = await _mediator.Send(authCommand);

            return Ok(res);
        }
    }
}
