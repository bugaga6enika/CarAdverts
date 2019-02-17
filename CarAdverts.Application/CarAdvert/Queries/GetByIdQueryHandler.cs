using AutoMapper;
using CarAdverts.Domain.CarAdvert;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CarAdverts.Application.CarAdvert.Queries
{
    public class GetByIdQueryHandler : IRequestHandler<GetByIdQuery, Dtos.CarAdvertDto>
    {
        private readonly ICarAdvertRepository _carAdvertRepository;

        public GetByIdQueryHandler(ICarAdvertRepository carAdvertRepository)
        {
            _carAdvertRepository = carAdvertRepository;
        }

        public async Task<Dtos.CarAdvertDto> Handle(GetByIdQuery request, CancellationToken cancellationToken)
        {
            var carAdvert = await _carAdvertRepository.GetByIdAsync(request.Id);

            return Mapper.Map<Dtos.CarAdvertDto>(carAdvert);
        }
    }
}
