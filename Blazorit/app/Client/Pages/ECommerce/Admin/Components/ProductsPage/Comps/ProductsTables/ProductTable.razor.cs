using Blazorit.SharedKernel.Core.Services.Models.ECommerce.Admin.Products;
using Microsoft.AspNetCore.Components;
using AntDesign.TableModels;
using Microsoft.AspNetCore.Components.Web;
using Blazorit.Client.Services.Abstract.ECommerce.Admin.Products;
using AntDesign;
using Blazorit.SharedKernel.Infrastructure.Repositories.Models.ECommerce.Admin.Products;
using System.Security.Cryptography.X509Certificates;

namespace Blazorit.Client.Pages.ECommerce.Admin.Components.ProductsPage.Comps.ProductsTables
{
    public partial class ProductTable
    {
        private List<Product> products = new List<Product>();
        private IEnumerable<Category> categories = new List<Category>();
        private Category SelectedCategory = new();
        private bool isVisibleInitProductModal = false;
        bool isConfirmLoadingModal = false;
        private Product product = new();
        private Form<Product> formProduct = new();
        private InitProduct InitProduct = InitProduct.Add;
        

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
            InitProduct = InitProduct.Add;
            isVisibleInitProductModal = true;
            categories = (await ProductService.GetCategoriesAsync()).ToList();
        }

        private void AddProductOkButton_ClickHandler()
        {
            formProduct.Submit();           
        }


        private async Task InitProductForm_FinishHandler()
        {
            isConfirmLoadingModal = true;
            product.Category = SelectedCategory.Name;

            switch (InitProduct)
            {
                case InitProduct.Add:
                    await ProductService.AddProductAsync(product);
                    break;
                case InitProduct.Update:
                    product = await ProductService.UpdateProductAsync(product);
                    int index = products.FindIndex(x => x.Id == product.Id);
                    if (index != -1)
                    {
                        products[index] = product;
                    }               
                    
                    break;
            }

            

            isVisibleInitProductModal = false;
            isConfirmLoadingModal = false;
        }


        private async Task DeleteButton_ClickHandler(long id)
        {
            products.Remove(products.FirstOrDefault(y => y.Id == id) ?? new());
        }


        private async Task UpdateButton_ClickHandlerAsync(long id)
        {
            InitProduct = InitProduct.Update;
            product = products.FirstOrDefault(x => x.Id == id) ?? new();
            isVisibleInitProductModal = true;
            categories = (await ProductService.GetCategoriesAsync()).ToList();
            SelectedCategory = categories.FirstOrDefault(x => x.Name == product.Category) ?? new();
        }


        //private async Task UpdateProductForm_FinishHandler()
        //{
        //    isVisibleUpdateProductModal = true;


        //    isVisibleUpdateProductModal = false;
        //    isConfirmLoadingModal = false;
        //}
    }
}
