using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OnlineMenu.Application.Order.Dto;

namespace OnlineMenu.Application.Order
{
    public class GetOrderByIdQuerry: IRequest<OrderDto>
    {
        public GetOrderByIdQuerry(int id)
        {

        }
    }

    internal sealed class GetOrderByIdQuerryHandler : IRequestHandler<GetOrderByIdQuerry, OrderDto>
    {
        Task<OrderDto> IRequestHandler<GetOrderByIdQuerry, OrderDto>.Handle(GetOrderByIdQuerry request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
