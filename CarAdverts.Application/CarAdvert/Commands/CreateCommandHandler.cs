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
    internal class CreateCommandHandler : IRequestHandler<CreateCommand, Dtos.CarAdvertDto>
    {
        private readonly ICarAdvertRepository _carAdvertRepository;
        private readonly IUnitOfWork<CarAdvertContext, Domain.CarAdvert.CarAdvert, Guid> _unitOfWork;

        public CreateCommandHandler(ICarAdvertRepository carAdvertRepository, IUnitOfWork<CarAdvertContext, Domain.CarAdvert.CarAdvert, Guid> unitOfWork)
        {
            _carAdvertRepository = carAdvertRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Dtos.CarAdvertDto> Handle(CreateCommand request, CancellationToken cancellationToken)
        {
            var dto = Mapper.Map<Domain.CarAdvert.CarAdvertDto>(request);
            var carAdvertAggregateRoot = Domain.CarAdvert.CarAdvert.Create(dto);
            var carAdvertEntity = await _carAdvertRepository.Create(carAdvertAggregateRoot);
            await _unitOfWork.CommitAsync();

            return Mapper.Map<Dtos.CarAdvertDto>(carAdvertEntity);
        }
    }
}
