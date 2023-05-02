using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using OrdersService.Application.Common.Abstractions.CQRS;
using OrdersService.Application.Common.Exceptions;
using OrdersService.Application.Common.Services.Token;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace OrdersService.Application.Authentication.Commands.RefreshToken
{
    public sealed class RefreshTokenCommandHandler : ICommandHandler<RefreshTokenCommand, RefreshTokenVm>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        private readonly ITokenService _tokenService;

        public RefreshTokenCommandHandler(UserManager<ApplicationUser> userManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
        }

        public async  Task<RefreshTokenVm> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {

            var principal = _tokenService.GetPrincipalFromExpiredToken(request.AccessToken);
            if (principal == null) throw new InvalidTokenException(request.AccessToken);

            var user = await _userManager.FindByNameAsync(principal.Identity.Name);

            if (user == null || user.RefreshToken != request.RefreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
            {
                throw new InvalidTokenException(request.AccessToken);
            }

            var newAccessToken = _tokenService.GenerateAccessToken(principal.Claims.ToList());
            var newRefreshToken = _tokenService.GenerateRefreshToken();

            user.RefreshToken = newRefreshToken;
            await _userManager.UpdateAsync(user);

            return new RefreshTokenVm
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(newAccessToken),
                RefreshToken = newRefreshToken
            };
        }
    }
}
