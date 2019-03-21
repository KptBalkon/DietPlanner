using AutoMapper;
using DietPlanner.Core.Domain;
using DietPlanner.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace DietPlanner.Infrastructure.Mappers
{
    public static class AutoMapperConfig
    {
        public static IMapper Initialize()
            => new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Plan, PlanDTO>();
                cfg.CreateMap<User, UserDTO>();

            }).CreateMapper();
    }
}
