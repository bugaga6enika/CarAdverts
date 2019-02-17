using AutoMapper.QueryableExtensions;
using CarAdverts.Domain.CarAdvert;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CarAdverts.Application.CarAdvert.Queries
{
    public class GetAllQueryHandler : IRequestHandler<GetAllQuery, IQueryable<Dtos.CarAdvertDto>>
    {
        private readonly ICarAdvertRepository _carAdvertRepository;

        public GetAllQueryHandler(ICarAdvertRepository carAdvertRepository)
        {
            _carAdvertRepository = carAdvertRepository;
        }

        public async Task<IQueryable<Dtos.CarAdvertDto>> Handle(GetAllQuery request, CancellationToken cancellationToken)
        {
            var carAdverts = await _carAdvertRepository.GetAsync(request.OrderBy).ConfigureAwait(false);

            return carAdverts.ProjectTo<Dtos.CarAdvertDto>();
        }
    }
}
