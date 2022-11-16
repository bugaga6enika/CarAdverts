using AutoMapper;
using CarAdverts.Domain.CarAdvert;
using CarAdverts.Domain.Core.Persistence;
using CarAdverts.Infrastructure.Contexts;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CarAdverts.Application.CarAdvert.Commands;

internal class CreateCommandHandler : IRequestHandler<CreateCommand, Dtos.CarAdvertDto>
{
    private readonly ICarAdvertRepository _carAdvertRepository;
    private readonly IUnitOfWork<CarAdvertContext, Domain.CarAdvert.CarAdvert, Guid> _unitOfWork;
    private readonly IMapper _mapper;

    public CreateCommandHandler(ICarAdvertRepository carAdvertRepository, IUnitOfWork<CarAdvertContext, Domain.CarAdvert.CarAdvert, Guid> unitOfWork, IMapper mapper)
    {
        _carAdvertRepository = carAdvertRepository ?? throw new ArgumentNullException(nameof(carAdvertRepository));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<Dtos.CarAdvertDto> Handle(CreateCommand request, CancellationToken cancellationToken)
    {
        var dto = _mapper.Map<CarAdvertDto>(request);
        var carAdvertAggregateRoot = Domain.CarAdvert.CarAdvert.Create(dto);
        var carAdvertEntity = await _carAdvertRepository.CreateAsync(carAdvertAggregateRoot).ConfigureAwait(false);
        await _unitOfWork.CommitAsync();

        return _mapper.Map<Dtos.CarAdvertDto>(carAdvertEntity);
    }
}
