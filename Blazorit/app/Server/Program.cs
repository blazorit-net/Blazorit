
using Blazorit.Infrastructure.DBStorages.BlazoritDB.EF;
using Blazorit.Server.Services.Abstract.Identity;
using Blazorit.Server.Services.Concrete.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
////using Microsoft.AspNetCore.Cors.Infrastructure;
////using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//For Parallels calls methods and BlazorServer (https://learn.microsoft.com/en-us/ef/core/dbcontext-configuration/#using-a-dbcontext-factory-eg-for-blazor)
//Not builder.Services.AddDbContext (scoped)
builder.Services.AddDbContextFactory<BlazoritContext>(options => {
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();


////builder.Services.AddEndpointsApiExplorer(); //custom add
////builder.Services.AddSwaggerGen(); //custom add

/***custom add - start***/
builder.Services.AddScoped<Blazorit.Infrastructure.Repositories.Abstract.Identity.IIdentityRepository, Blazorit.Infrastructure.Repositories.Concrete.Identity.IdentityRepository>();
builder.Services.AddScoped<Blazorit.Core.Services.Abstract.Identity.IIdentityService, Blazorit.Core.Services.Concrete.Identity.IdentityService>();
builder.Services.AddScoped<IIdentityService, IdentityService>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options => {
        options.TokenValidationParameters = new TokenValidationParameters {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey =
                new SymmetricSecurityKey(System.Text.Encoding.UTF8
                .GetBytes(builder.Configuration.GetSection("AppSettings:SecurityKey").Value ?? string.Empty)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });
builder.Services.AddHttpContextAccessor();
/***custom add - end***/

var app = builder.Build();

////app.UseSwaggerUI(); //custom add

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.UseWebAssemblyDebugging();
} else {
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

////app.UseSwagger(); //custom add

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication(); //custom add
app.UseAuthorization(); //custom add


app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
