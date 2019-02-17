using CarAdverts.Domain.CarAdvert;
using CarAdverts.Domain.Core.Persistence;
using CarAdverts.Infrastructure.Contexts;
using CarAdverts.Infrastructure.Repositories;
using CarAdverts.Infrastructure.UnitOfWork;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace CarAdverts.Application.Configuratoins
{
    public static class IoC
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddDbContext<CarAdvertContext>();
            services.AddScoped<ICarAdvertRepository, CarAdvertRepository>();
            services.AddScoped<IUnitOfWork<CarAdvertContext, CarAdvert, Guid>, CarAdvertUnitOfWork>();
            services.AddScoped<IContext<CarAdvert, Guid>, CarAdvertContext>();
        }
    }
}
