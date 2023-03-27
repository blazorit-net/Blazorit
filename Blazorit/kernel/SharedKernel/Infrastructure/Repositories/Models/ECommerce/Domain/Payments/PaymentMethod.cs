using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazorit.SharedKernel.Infrastructure.Repositories.Models.ECommerce.Domain.Payments
{
    public class PaymentMethod
    {
        public long Id { get; set; }

        public string Method { get; set; } = string.Empty;

        /// <summary>
        /// Is Cash On Delivery
        /// </summary>
        public bool IsCOD { get; set; }

        /// <summary>
        /// Order by this property
        /// </summary>
        public long Ordby { get; set; }

        /// <summary>
        /// Check fields of Payment method
        /// </summary>
        public bool IsOkMethod
        {
            get
            {
                if (Id > 0)
                {
                    return true;
                }

                return false;
            }
        }
    }
}
