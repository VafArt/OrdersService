using Microsoft.AspNetCore.Identity;
using OrdersService.Application.Common.Abstractions.CQRS;
using OrdersService.Application.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersService.Application.Authentication.Commands.Revoke
{
    public sealed class RevokeCommandHandler : ICommandHandler<RevokeCommand>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public RevokeCommandHandler(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task Handle(RevokeCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(request.Username);
            if (user == null) throw new NotFoundException("User", request.Username);

            user.RefreshToken = null;

            await _userManager.UpdateAsync(user);
        }
    }
}
