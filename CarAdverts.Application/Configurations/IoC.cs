using CarAdverts.Application.CarAdvert.Queries;
using CarAdverts.Application.Configurations.AutoMapper.Profiles;
using CarAdverts.Domain.CarAdvert;
using CarAdverts.Domain.Core.Persistence;
using CarAdverts.Infrastructure.Contexts;
using CarAdverts.Infrastructure.Repositories;
using CarAdverts.Infrastructure.UnitOfWork;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace CarAdverts.Application.Configurations;

public static class IoC
{
    public static void RegisterServices(IServiceCollection services)
    {
        services.AddDbContext<CarAdvertContext>();
        services.AddScoped<ICarAdvertRepository, CarAdvertRepository>();
        services.AddScoped<IUnitOfWork<CarAdvertContext, Domain.CarAdvert.CarAdvert, Guid>, CarAdvertUnitOfWork>();
        services.AddScoped<IContext<Domain.CarAdvert.CarAdvert, Guid>, CarAdvertContext>();

        services.AddMediatR(typeof(GetByIdQueryHandler));

        services.AddAutoMapper((configurator) =>
        {
            configurator.AddProfile<CarAdvertDomainToDtoProfile>();
        });
    }
}

