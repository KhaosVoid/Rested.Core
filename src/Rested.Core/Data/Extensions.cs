﻿using Microsoft.Extensions.DependencyInjection;
using Rested.Core.CQRS.Data;
using System.Reflection;

namespace Rested.Core.Data
{
    public static class Extensions
    {
        public static IServiceCollection RegisterProjectionMappings(this IServiceCollection services) =>
            services.AddSingleton(ProjectionRegistration.Initialize());

        public static IServiceCollection RegisterProjectionMappingsFromAssembly(this IServiceCollection services, Assembly assembly) =>
            services.AddSingleton(ProjectionRegistration.Initialize(assembly));

        public static IServiceCollection RegisterProjectionMappingsFromAssemblies(this IServiceCollection services, Assembly[] assemblies) =>
            services.AddSingleton(ProjectionRegistration.Initialize(assemblies));
    }
}
