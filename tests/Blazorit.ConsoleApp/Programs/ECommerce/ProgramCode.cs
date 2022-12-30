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
            //var result = await _ecomRepo.AddProductToCart(1, 7000.ToString(), 4);
            //var result = await _ecomRepo.AddProduct("some-sku", "Test2 not existing product", "rub", 1111.567m, "some text2", "robot");
            var result = await _ecomRepo.AddProduct("Test3 not existing product", "rub", 1111.567m, "some text2", "robot");
        
        }
    }
}
