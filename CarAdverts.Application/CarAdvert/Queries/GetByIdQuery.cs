using CarAdverts.Application.CarAdvert.Dtos;
using MediatR;
using System;

namespace CarAdverts.Application.CarAdvert.Queries
{
    public class GetByIdQuery : IRequest<CarAdvertDto>
    {
        public Guid Id { get; set; }
    }
}
