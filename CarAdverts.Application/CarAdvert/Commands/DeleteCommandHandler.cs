using CarAdverts.Domain.CarAdvert;
using CarAdverts.Domain.Core.Persistence;
using CarAdverts.Infrastructure.Contexts;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CarAdverts.Application.CarAdvert.Commands
{
    internal class DeleteCommandHandler : IRequestHandler<DeleteCommand>
    {
        private readonly ICarAdvertRepository _carAdvertRepository;
        private readonly IUnitOfWork<CarAdvertContext, Domain.CarAdvert.CarAdvert, Guid> _unitOfWork;

        public DeleteCommandHandler(ICarAdvertRepository carAdvertRepository, IUnitOfWork<CarAdvertContext, Domain.CarAdvert.CarAdvert, Guid> unitOfWork)
        {
            _carAdvertRepository = carAdvertRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(DeleteCommand request, CancellationToken cancellationToken)
        {
            await _carAdvertRepository.DeleteAsync(request.Id);
            await _unitOfWork.CommitAsync();

            return Unit.Value;
        }
    }
}
