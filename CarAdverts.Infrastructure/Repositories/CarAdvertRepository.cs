using CarAdverts.Domain.CarAdvert;
using CarAdverts.Domain.Core.Persistence;
using CarAdverts.Infrastructure.Specifications;
using System;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace CarAdverts.Infrastructure.Repositories
{
    public class CarAdvertRepository : Repository<CarAdvert, Guid>, ICarAdvertRepository
    {
        public CarAdvertRepository(IContext<CarAdvert, Guid> context) : base(context)
        {
        }       
    }
}
