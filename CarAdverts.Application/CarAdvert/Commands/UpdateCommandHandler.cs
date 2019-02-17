using AutoMapper;
using CarAdverts.Domain.CarAdvert;
using CarAdverts.Domain.Core.Persistence;
using CarAdverts.Infrastructure.Contexts;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CarAdverts.Application.CarAdvert.Commands
{
    internal class UpdateCommandHandler : IRequestHandler<UpdateCommand>
    {
        private readonly ICarAdvertRepository _carAdvertRepository;
        private readonly IUnitOfWork<CarAdvertContext, Domain.CarAdvert.CarAdvert, Guid> _unitOfWork;

        public UpdateCommandHandler(ICarAdvertRepository carAdvertRepository, IUnitOfWork<CarAdvertContext, Domain.CarAdvert.CarAdvert, Guid> unitOfWork)
        {
            _carAdvertRepository = carAdvertRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(UpdateCommand request, CancellationToken cancellationToken)
        {
            var dto = Mapper.Map<Domain.CarAdvert.CarAdvertDto>(request);
            var carAdvertAggregateRoot = await _carAdvertRepository.GetByIdAsync(request.Id).ConfigureAwait(false);
            carAdvertAggregateRoot.Update(dto);
            await _carAdvertRepository.UpdateAsync(carAdvertAggregateRoot).ConfigureAwait(false);
            await _unitOfWork.CommitAsync().ConfigureAwait(false);

            return Unit.Value;
        }
    }
}
