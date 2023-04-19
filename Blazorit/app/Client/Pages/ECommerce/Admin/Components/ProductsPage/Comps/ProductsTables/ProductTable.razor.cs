using Blazorit.SharedKernel.Core.Services.Models.ECommerce.Admin.Products;
using Microsoft.AspNetCore.Components;
using AntDesign.TableModels;
using Microsoft.AspNetCore.Components.Web;
using Blazorit.Client.Services.Abstract.ECommerce.Admin.Products;
using AntDesign;
using Blazorit.SharedKernel.Infrastructure.Repositories.Models.ECommerce.Admin.Products;

namespace Blazorit.Client.Pages.ECommerce.Admin.Components.ProductsPage.Comps.ProductsTables
{
    public partial class ProductTable
    {
        private List<Product> products = new List<Product>();
        private IEnumerable<Category> categories = new List<Category>();
        private Category SelectedCategory = new();
        private bool isVisibleAddProductModal = false;
        bool isConfirmLoadingModal = false;
        private Product product = new();
        private Form<Product> formProduct = new();
        

        [Inject]
        private IProductService ProductService { get; set; } = null!;

        [Parameter]
        public string? Class { get; set; }

        [Parameter]
        public EventCallback<Product> OnRowClick { get; set; } = new();

        protected override async Task OnInitializedAsync()
        {
            products = (await ProductService.GetAllProductsAsync()).ToList();            
        }

        private Dictionary<string, object> ProductsTable_RowHandler(RowData<Product> row)
        {
            Action<MouseEventArgs> onClick = async args =>
            {                
                await ProductsTable_ClickRowHandlerAsync(row.Data);
            };

            return new Dictionary<string, object>
            {
                { "onclick", onClick }
            };
        }

        private async Task ProductsTable_ClickRowHandlerAsync(Product product)
        {
            await OnRowClick.InvokeAsync(product);
        }

        //private async Task ProductsTable_ClickRowHandlerAsync(RowData<Product> row)
        //{
        //    await OnRowClick.InvokeAsync(row.Data);
        //}


        private async Task AddProductButton_ClickHandler()
        {
            isVisibleAddProductModal = true;
            categories = (await ProductService.GetCategoriesAsync()).ToList();
        }

        private void AddProductOkButton_ClickHandler()
        {
            formProduct.Submit();           
        }


        private async Task AddProductForm_FinishHandler()
        {
            isConfirmLoadingModal = true;

            product.Category = SelectedCategory.Name;
            await ProductService.AddProductAsync(product);
            isVisibleAddProductModal = false;
            isConfirmLoadingModal = false;
        }


    }
}
