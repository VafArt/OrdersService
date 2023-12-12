using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrdersService.Application.Authentication;
using OrdersService.Application.Authentication.Commands.RefreshToken;
using OrdersService.Application.Authentication.Commands.Register;
using OrdersService.Application.Authentication.Commands.RegisterAdmin;
using OrdersService.Application.Authentication.Commands.Revoke;
using OrdersService.Application.Authentication.Commands.RevokeAll;
using OrdersService.Application.Authentication.Queries.Login;
using OrdersService.WebApi.Examples.Requests;
using OrdersService.WebApi.Examples.Responses;
using OrdersService.WebApi.Models.Authentication;
using Swashbuckle.AspNetCore.Filters;

namespace OrdersService.WebApi.Controllers
{
    [Route("auth")]
    public class AuthenticateController : BaseController
    {
        private readonly IMapper _mapper;

        public AuthenticateController(IMapper mapper)
        {
            _mapper = mapper;
        }

        /// <summary>
        /// Creates access and refresh tokens
        /// </summary>
        /// <param name="model">User name and password</param>
        /// <returns>Login Vm</returns>
        /// <responce code="200">Success</responce>
        /// <responce code="400">If validation error occurred</responce>
        /// <responce code="409">If user credentials are invalid</responce>
        [SwaggerRequestExample(typeof(LoginDto), typeof(LoginDtoExample))]
        [SwaggerResponseExample(200, typeof(LoginVmExample))]
        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<LoginVm>> Login([FromBody] LoginDto model)
        {
            var loginQuery = _mapper.Map<LoginQuery>(model);
            return Ok(await Mediator.Send(loginQuery));
        }

        /// <summary>
        /// Registers the user
        /// </summary>
        /// <param name="model">User name, email and password</param>
        /// <responce code="200">Success</responce>
        /// <responce code="400">If validation error occurred</responce>
        /// <responce code="409">If user already exists</responce>
        [SwaggerRequestExample(typeof(RegisterDto), typeof(RegisterDtoExample))]
        [HttpPost]
        [Route("registration")]
        public async Task<ActionResult> Register([FromBody] RegisterDto model)
        {
            var registerCommand = new RegisterCommand()
            {
                Email = model.Email,
                Password = model.Password,
                Username = model.Username,
            };
            await Mediator.Send(registerCommand);
            return Ok();
        }

        /// <summary>
        /// Registers the admin
        /// </summary>
        /// <param name="model">User name, email and password</param>
        /// <responce code="200">Success</responce>
        /// <responce code="400">If validation error occurred</responce>
        /// <responce code="409">If user already exists</responce>
        [SwaggerRequestExample(typeof(RegisterDto), typeof(RegisterDtoExample))]
        [HttpPost]
        [Route("registration-admin")]
        public async Task<ActionResult> RegisterAdmin([FromBody] RegisterDto model)
        {
            var registerAdminCommand = new RegisterAdminCommand()
            {
                Username = model.Username,
                Email = model.Email,
                Password = model.Password
            };
            await Mediator.Send(registerAdminCommand);
            return Ok();
        }

        /// <summary>
        /// Refreshes the access and refresh tokens
        /// </summary>
        /// <param name="model">Access and refresh token</param>
        /// <returns>RefreshTokenVm</returns>
        /// <responce code="200">Success</responce>
        /// <responce code="400">If validation error occurred</responce>
        [SwaggerRequestExample(typeof(RefreshTokenDto), typeof(RefreshTokenDtoExample))]
        [SwaggerResponseExample(200, typeof(RefreshTokenVmExample))]
        [HttpPost]
        [Route("refresh-token")]
        public async Task<ActionResult<RefreshTokenVm>> RefreshToken(RefreshTokenDto model)
        {
            var refreshTokenCommand = _mapper.Map<RefreshTokenCommand>(model);
            return Ok(await Mediator.Send(refreshTokenCommand));
        }

        /// <summary>
        /// Revokes refresh token of specified user
        /// </summary>
        /// <param name="username">User name</param>
        /// <responce code="200">Success</responce>
        /// <responce code="404">If user not found</responce>
        [Authorize]
        [HttpPost]
        [Route("revoke/{username}")]
        public async Task<ActionResult> Revoke(string username)
        {
            var revokeCommand = new RevokeCommand(username);
            await Mediator.Send(revokeCommand);
            return Ok();
        }

        /// <summary>
        /// Revokes all refresh tokens if authorized as admin
        /// </summary>
        /// <responce code="200">Success</responce>
        [Authorize(Roles = UserRoles.Admin)]
        [HttpPost]
        [Route("revoke-all")]
        public async Task<ActionResult> RevokeAll()
        {
            var revokeAllCommand = new RevokeAllCommand();
            await Mediator.Send(revokeAllCommand);
            return Ok();
        }
    }
}
