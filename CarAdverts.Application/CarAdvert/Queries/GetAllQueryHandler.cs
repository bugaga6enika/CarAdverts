using AutoMapper;
using AutoMapper.QueryableExtensions;
using CarAdverts.Application.CarAdvert.Dtos;
using CarAdverts.Domain.CarAdvert;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CarAdverts.Application.CarAdvert.Queries;

public class GetAllQueryHandler : IRequestHandler<GetAllQuery, IQueryable<Dtos.CarAdvertDto>>
{
    private readonly ICarAdvertRepository _carAdvertRepository;
    private readonly IMapper _mapper;

    public GetAllQueryHandler(ICarAdvertRepository carAdvertRepository, IMapper mapper)
    {
        _carAdvertRepository = carAdvertRepository ?? throw new System.ArgumentNullException(nameof(carAdvertRepository));
        _mapper = mapper ?? throw new System.ArgumentNullException(nameof(mapper));
    }

    public async Task<IQueryable<Dtos.CarAdvertDto>> Handle(GetAllQuery request, CancellationToken cancellationToken)
    {
        var carAdverts = await _carAdvertRepository.GetAsync(request.OrderBy).ConfigureAwait(false);       

        return _mapper.ProjectTo<Dtos.CarAdvertDto>(carAdverts);
    }
}
