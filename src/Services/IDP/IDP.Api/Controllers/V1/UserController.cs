using Asp.Versioning;

using IDP.Api.Application.User.Command.Insert;
using IDP.Api.Controllers.BaseController;

using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace IDP.Api.Controllers.V1
{
    [Route("api/v{V:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion(1)]
    public class UserController : IBaseController
    {
        public readonly IMediator _mediator;
        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }
        /// <summary>
        ///   ورود اطلاعات کاربر
        /// </summary>
        /// <returns></returns>

        [MapToApiVersion(1)]
        [HttpPost("Insert")]
        public async Task<IActionResult> Insert([FromBody] UserInsertCommand command)
        {
            var res = await _mediator.Send(command);

            return Ok();
        }
    }
}
