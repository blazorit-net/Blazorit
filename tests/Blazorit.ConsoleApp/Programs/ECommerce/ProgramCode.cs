using Blazorit.Infrastructure.Repositories.Abstract.ECommerce;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazorit.ConsoleApp.Programs.ECommerce
{
    internal class ProgramCode
    {
        private readonly IECommerceRepository _ecomRepo;

        internal ProgramCode(IECommerceRepository ecomRepo)
        {
            _ecomRepo = ecomRepo;
        }


        internal async Task Main()
        {
            var result = await _ecomRepo.AddProductToCart(1, 7000.ToString(), 4);
        }
    }
}
