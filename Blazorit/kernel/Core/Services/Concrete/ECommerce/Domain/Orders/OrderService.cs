using Blazorit.Core.Services.Abstract.ECommerce.Domain.Orders;
using Blazorit.Infrastructure.Repositories.Abstract.ECommerce;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazorit.Core.Services.Concrete.ECommerce.Domain.Orders
{
    public class OrderService : IOrderService
    {
        private readonly IECommerceRepository _dataRepo;

        public OrderService(IECommerceRepository dataRepo)
        {
            _dataRepo = dataRepo;
        }

        public async Task<bool> CreateOrderFromCart(long userId)
        {
            return await _dataRepo.CreateOrderFromCart(userId);
        }
    }
}
