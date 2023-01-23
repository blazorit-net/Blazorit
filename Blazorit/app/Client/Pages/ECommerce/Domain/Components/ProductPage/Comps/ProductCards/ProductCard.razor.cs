using Blazorit.SharedKernel.Core.Services.Models.ECommerce.Domain.ProductCards;
using Microsoft.AspNetCore.Components;

namespace Blazorit.Client.Pages.ECommerce.Domain.Components.ProductPage.Comps.ProductCards
{
    public partial class ProductCard
    {
        [Parameter] public string? Class { get; set; }

        [Parameter] public ProductCardData Data { get; set; } = new();


    }
}
