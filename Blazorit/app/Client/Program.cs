using Blazorit.Client;
using Blazorit.Client.Providers.Concrete.Identity;
using Blazorit.Client.Services.Abstract.Identity;
using Blazorit.Client.Services.Concrete.Identity;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Blazorit.Client.Services.Abstract.ECommerce.Domain.Carts;
using Blazorit.Client.Services.Abstract.ECommerce.Domain.Data;
using Blazorit.Client.Services.Concrete.ECommerce.Domain.Data;
using Blazorit.Client.Services.Concrete.ECommerce.Domain.Carts;
using Blazorit.Client.Services.Abstract.ECommerce.Domain.Orders;
using Blazorit.Client.Services.Concrete.ECommerce.Domain.Orders;
using Blazorit.Client.Services.Abstract.ECommerce.Domain.Deliveries;
using Blazorit.Client.Services.Concrete.ECommerce.Domain.Deliveries;
using Blazorit.Client.Services.Abstract.ECommerce.Domain.Payments;
using Blazorit.Client.Services.Concrete.ECommerce.Domain.Payments;
using Blazorit.Client.Services.Abstract.ECommerce.Admin.Products;
using Blazorit.Client.Services.Concrete.ECommerce.Admin.Products;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddBlazoredLocalStorage(); //custom add
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddAntDesign(); //AntDesign
builder.Services.AddScoped<IIdentityService, IdentityService>(); //custom add
builder.Services.AddOptions(); //custom add
builder.Services.AddAuthorizationCore(); //custom add
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>(); //custom add

//################################################################
//  ######################--ECOMMERCE--#########################
//################################################################
builder.Services.AddScoped<IDataService, DataService>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IDeliveryService, DeliveryService>();
builder.Services.AddScoped<IPaymentService, PaymentService>();
builder.Services.AddScoped<Blazorit.Client.States.ECommerce.Domain.Carts.CartState>(); //Scoped state for shopcart components

// #######################--admin--###########################
builder.Services.AddScoped<IProductService, ProductService>();
//################################################################
//  ############################################################
//################################################################

await builder.Build().RunAsync();
