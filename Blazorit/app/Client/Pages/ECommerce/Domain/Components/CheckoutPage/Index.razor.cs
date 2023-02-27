using Blazorit.Client.Services.Abstract.ECommerce.Domain.Deliveries;
using Blazorit.SharedKernel.Infrastructure.Repositories.Models.ECommerce.Domain.Deliveries;
using Microsoft.AspNetCore.Components;

namespace Blazorit.Client.Pages.ECommerce.Domain.Components.CheckoutPage
{
    public partial class Index
    {
        private DeliveryMethod selectedDeliveryMethod = new();

        [Parameter] 
        public string? Class { get; set; }

    }
}
