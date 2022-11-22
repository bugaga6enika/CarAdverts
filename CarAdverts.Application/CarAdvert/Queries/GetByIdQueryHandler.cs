using AutoMapper;
using CarAdverts.Domain.CarAdvert;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CarAdverts.Application.CarAdvert.Queries;

public class GetByIdQueryHandler : IRequestHandler<GetByIdQuery, Dtos.CarAdvertDto>
{
    private readonly ICarAdvertRepository _carAdvertRepository;
    private readonly IMapper _mapper;

    public GetByIdQueryHandler(ICarAdvertRepository carAdvertRepository, IMapper mapper)
    {
        _carAdvertRepository = carAdvertRepository ?? throw new System.ArgumentNullException(nameof(carAdvertRepository));
        _mapper = mapper ?? throw new System.ArgumentNullException(nameof(mapper));
    }

    public async Task<Dtos.CarAdvertDto> Handle(GetByIdQuery request, CancellationToken cancellationToken)
    {
        var carAdvert = await _carAdvertRepository.GetByIdAsync(request.Id).ConfigureAwait(false);

        return _mapper.Map<Dtos.CarAdvertDto>(carAdvert);
    }
}
