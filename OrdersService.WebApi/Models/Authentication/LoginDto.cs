using AutoMapper;
using OrdersService.Application.Authentication.Queries.Login;
using OrdersService.Application.Common.Mappings;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersService.WebApi.Models.Authentication
{
    public class LoginDto : IMapWith<LoginQuery>
    {
        [Required(ErrorMessage = "Введите имя пользователя")]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "Введите пароль!")]
        public string Password { get; set; } = string.Empty;

        public void Mapping(Profile profile)
        {
            profile.CreateMap<LoginDto, LoginQuery>()
                .ForMember(loginDto => loginDto.Username,
                opt => opt.MapFrom(loginQuery => loginQuery.Username))
                .ForMember(loginDto => loginDto.Password,
                opt => opt.MapFrom(loginQuery => loginQuery.Password));
        }
    }
}
