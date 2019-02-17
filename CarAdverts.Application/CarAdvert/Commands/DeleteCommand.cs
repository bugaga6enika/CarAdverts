using CarAdverts.Domain.CarAdvert;
using MediatR;
using System;

namespace CarAdverts.Application.CarAdvert.Commands
{
    public class DeleteCommand : IRequest
    {
        public Guid Id { get; set; }        
    }
}
