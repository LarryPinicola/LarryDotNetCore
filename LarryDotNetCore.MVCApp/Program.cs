using LarryDotNetCore.MVCApp.EFDbContext;
using LarryDotNetCore.MVCApp.Interfaces;
using Microsoft.EntityFrameworkCore;
using Refit;
using RestSharp;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AppDBContext>(options =>
{
    string? connectionString = builder.Configuration.GetConnectionString("DbConnection");
    options.UseSqlServer(connectionString);
}, ServiceLifetime.Transient, ServiceLifetime.Transient);

#region 
//RefitConfiguration
builder.Services.AddRefitClient<IBlogApi>()
    .ConfigureHttpClient(c => c.BaseAddress = new Uri(builder.Configuration.GetSection("RestApiUrl").Value!));

#endregion

#region 
//HttpClientConfiguration
//builder.Services.AddScoped<HttpClient>(); // In this usage, baseAddress ko httpclient controller mhr private declare ya ml

builder.Services.AddScoped(x => new HttpClient // baseAddress ko Configuration mhr register lote
{
    BaseAddress = new Uri(builder.Configuration.GetSection("RestApiUrl").Value!)
});

#endregion

#region 
//RestClientConfiguration
builder.Services.AddScoped(x => new RestClient(builder.Configuration.GetSection("RestApiUrl").Value!));

#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
