using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OrdersService.Application.Authentication;
using OrdersService.Application.Authentication.Commands.RefreshToken;
using OrdersService.Application.Authentication.Commands.Register;
using OrdersService.Application.Authentication.Commands.RegisterAdmin;
using OrdersService.Application.Authentication.Commands.Revoke;
using OrdersService.Application.Authentication.Commands.RevokeAll;
using OrdersService.Application.Authentication.Queries.Login;
using OrdersService.Application.Common.Services.Token;
using OrdersService.WebApi.Models.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto model)
        {
            var loginQuery = _mapper.Map<LoginQuery>(model);
            return Ok(await Mediator.Send(loginQuery));
        }

        [HttpPost]
        [Route("registration")]
        public async Task<IActionResult> Register([FromBody] RegisterDto model)
        {
            var registerCommand = new RegisterCommand(model.Username, model.Email, model.Password);
            await Mediator.Send(registerCommand);
            return Ok();
        }

        [HttpPost]
        [Route("registration-admin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterDto model)
        {
            var registerAdminCommand = new RegisterAdminCommand(model.Username, model.Email, model.Password);
            await Mediator.Send(registerAdminCommand);
            return Ok();
        }

        [HttpPost]
        [Route("refresh-token")]
        public async Task<IActionResult> RefreshToken(RefreshTokenDto model)
        {
            var refreshTokenCommand = _mapper.Map<RefreshTokenCommand>(model);
            return Ok(await Mediator.Send(refreshTokenCommand));
        }

        [Authorize]
        [HttpPost]
        [Route("revoke/{username}")]
        public async Task<IActionResult> Revoke(string username)
        {
            var revokeCommand = new RevokeCommand(username);
            await Mediator.Send(revokeCommand);
            return Ok();
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpPost]
        [Route("revoke-all")]
        public async Task<IActionResult> RevokeAll()
        {
            var revokeAllCommand = new RevokeAllCommand();
            await Mediator.Send(revokeAllCommand);
            return Ok();
        }
    }
}
