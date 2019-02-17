using CarAdverts.Domain.CarAdvert;
using CarAdverts.Domain.Core.Persistence;
using CarAdverts.Infrastructure.Contexts;
using System;
using System.Threading.Tasks;

namespace CarAdverts.Infrastructure.UnitOfWork
{
    public class CarAdvertUnitOfWork : IUnitOfWork<CarAdvertContext, CarAdvert, Guid>
    {
        private readonly CarAdvertContext _context;

        public CarAdvertUnitOfWork(IContext<CarAdvert, Guid> context)
        {
            _context = context as CarAdvertContext;
        }

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}