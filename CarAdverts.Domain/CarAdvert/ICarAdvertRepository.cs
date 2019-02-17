using CarAdverts.Domain.Core.Persistence;
using System;

namespace CarAdverts.Domain.CarAdvert
{
    public interface ICarAdvertRepository : IRepository<CarAdvert, Guid>
    {
    }
}
