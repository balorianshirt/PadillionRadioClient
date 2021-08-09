using Autofac;
using Microsoft.Extensions.Configuration;
using PadillionRadio.Data.Interfaces;
using PadillionRadio.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Autofac.Extensions.DependencyInjection;
using System;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.AspNetCore.Http;
using PadillionRadio.Business.Services;

namespace PadillionRadio.Business.Configurations
{
    public class AutofacConfigurationService
    {
        public IConfigurationRoot Configuration { get; set; }
        public static void ConfigureContainer(IServiceCollection services)
        {
            var containerBuilder = new ContainerBuilder();
            containerBuilder.Populate(services);
            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            services.AddTransient(typeof(IUnitOfWork), typeof(UnitOfWork));
            services.AddTransient(typeof(IUserService), typeof(UserService));
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.TryAddTransient<IHttpContextAccessor, HttpContextAccessor>();
            services.AddMemoryCache();
            
            var container = containerBuilder.Build();
            container.Resolve<IServiceProvider>();
        }
    }
}
