using Blazorit.Client.Services.Abstract.Admin.Products;
using Blazorit.SharedKernel.Core.Services.Models.ECommerce.Admin.Products;
using Microsoft.AspNetCore.Components;

namespace Blazorit.Client.Pages.ECommerce.Admin.Components.ProductsPage
{
    public partial class Index
    {

        private string description = string.Empty;

        [Parameter]
        public string? Class { get; set; }


        private async Task ProductsTable_RowClickHandler(Product product)
        {
            await InvokeAsync(() => description = product.Description);
        }
    }
}
