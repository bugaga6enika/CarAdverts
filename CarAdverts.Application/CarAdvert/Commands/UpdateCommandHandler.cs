using AutoMapper;
using CarAdverts.Domain.CarAdvert;
using CarAdverts.Domain.Core.Persistence;
using CarAdverts.Infrastructure.Contexts;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CarAdverts.Application.CarAdvert.Commands;

internal class UpdateCommandHandler : IRequestHandler<UpdateCommand>
{
    private readonly ICarAdvertRepository _carAdvertRepository;
    private readonly IUnitOfWork<CarAdvertContext, Domain.CarAdvert.CarAdvert, Guid> _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateCommandHandler(ICarAdvertRepository carAdvertRepository, IUnitOfWork<CarAdvertContext, Domain.CarAdvert.CarAdvert, Guid> unitOfWork, IMapper mapper)
    {
        _carAdvertRepository = carAdvertRepository ?? throw new ArgumentNullException(nameof(carAdvertRepository));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<Unit> Handle(UpdateCommand request, CancellationToken cancellationToken)
    {
        var dto = _mapper.Map<CarAdvertDto>(request);
        var carAdvertAggregateRoot = await _carAdvertRepository.GetByIdAsync(request.Id).ConfigureAwait(false);
        carAdvertAggregateRoot.Update(dto);
        await _carAdvertRepository.UpdateAsync(carAdvertAggregateRoot).ConfigureAwait(false);
        await _unitOfWork.CommitAsync().ConfigureAwait(false);

        return Unit.Value;
    }
}
