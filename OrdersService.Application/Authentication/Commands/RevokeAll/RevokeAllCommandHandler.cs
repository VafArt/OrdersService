using Microsoft.AspNetCore.Identity;
using OrdersService.Application.Common.Abstractions.CQRS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersService.Application.Authentication.Commands.RevokeAll
{
    public sealed class RevokeAllCommandHandler : ICommandHandler<RevokeAllCommand>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public RevokeAllCommandHandler(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task Handle(RevokeAllCommand request, CancellationToken cancellationToken)
        {
            var users = _userManager.Users.ToList();
            foreach (var user in users)
            {
                user.RefreshToken = null;
                await _userManager.UpdateAsync(user);
            }
        }
    }
}
