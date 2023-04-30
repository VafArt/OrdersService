using AutoMapper;
using OrdersService.Application.Common.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OrdersService.Tests
{
    public static class MapperGenerator
    {
        public static IMapper CreateMapper()
        {
            var configuration = new MapperConfiguration(config =>
            {
                config.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly()));
                config.AddProfile(new AssemblyMappingProfile(typeof(AssemblyMappingProfile).Assembly));
            });
            return new Mapper(configuration);
        }
    }
}
