using AutoMapper;
using OrdersService.Application.Authentication.Commands.RefreshToken;
using OrdersService.Application.Common.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersService.WebApi.Models.Authentication
{
    public class RefreshTokenDto : IMapWith<RefreshTokenCommand>
    {
        public string? AccessToken { get; set; }

        public string? RefreshToken { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<RefreshTokenDto, RefreshTokenCommand>()
                .ForMember(refreshTokenCommand => refreshTokenCommand.AccessToken,
                opt => opt.MapFrom(refreshTokenDto => refreshTokenDto.AccessToken))
                .ForMember(refreshTokenCommand => refreshTokenCommand.RefreshToken,
                opt => opt.MapFrom(refreshTokenDto => refreshTokenDto.RefreshToken));
        }
    }
}
