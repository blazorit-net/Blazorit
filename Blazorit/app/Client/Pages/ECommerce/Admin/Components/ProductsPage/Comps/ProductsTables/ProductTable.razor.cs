using Blazorit.Client.Services.Abstract.Admin.Products;
using Blazorit.SharedKernel.Core.Services.Models.ECommerce.Admin.Products;
using Microsoft.AspNetCore.Components;
using AntDesign.TableModels;
using Microsoft.AspNetCore.Components.Web;

namespace Blazorit.Client.Pages.ECommerce.Admin.Components.ProductsPage.Comps.ProductsTables
{
    public partial class ProductTable
    {
        private IEnumerable<Product> products = new List<Product>();

        [Inject]
        private IProductService ProductService { get; set; } = null!;

        [Parameter]
        public string? Class { get; set; }

        [Parameter]
        public EventCallback<Product> OnRowClick { get; set; } = new();

        protected override async Task OnInitializedAsync()
        {
            products = await ProductService.GetAllProductsAsync();
        }

        private Dictionary<string, object> ProductsTable_RowHandler(RowData<Product> row)
        {
            Action<MouseEventArgs> onClick = async args =>
            {                
                await ProductsTable_ClickRowHandler(row.Data);
            };

            return new Dictionary<string, object>
            {
                { "onclick", onClick }
            };
        }

        private async Task ProductsTable_ClickRowHandler(Product product)
        {
            await OnRowClick.InvokeAsync(product);
        }

        private async Task ProductsTable_ClickRowHandlerAsync(RowData<Product> row)
        {
            await OnRowClick.InvokeAsync(row.Data);
        }
    }
}
