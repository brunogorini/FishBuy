using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace FishBuy.Application.Automapper
{
    public static class AutomapperExtensions
    {
        public static void AddAutoMapper(this IServiceCollection services, Profile profile)
        {
            var config = new MapperConfiguration(mc =>
            {
                mc.AddProfile(profile);
            });

            IMapper mapper = config.CreateMapper();

            services.AddSingleton(mapper);
        }

    }
}
