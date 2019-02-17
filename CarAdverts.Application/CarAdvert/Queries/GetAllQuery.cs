using CarAdverts.Application.CarAdvert.Dtos;
using MediatR;
using System.Linq;

namespace CarAdverts.Application.CarAdvert.Queries
{
    public class GetAllQuery : IRequest<IQueryable<CarAdvertDto>>
    {
        public string OrderBy { get; set; } = "Id";
    }
}
