using CarAdverts.Domain.CarAdvert;
using CarAdverts.Domain.Core.Persistence;
using System;

namespace CarAdverts.Infrastructure.Repositories
{
    public class CarAdvertRepository : Repository<CarAdvert, Guid>, ICarAdvertRepository
    {
        public CarAdvertRepository(IContext<CarAdvert, Guid> context) : base(context)
        {
        }
    }
}
