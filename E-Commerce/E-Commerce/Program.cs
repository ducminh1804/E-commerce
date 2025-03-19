using E_Commerce.Controllers;
using E_Commerce.Data;
using E_Commerce.Filter;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDistributedMemoryCache();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(3000);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<EcommerceContext>(optitons =>
{
    optitons.UseSqlServer(builder.Configuration.GetConnectionString("Ecommerce"));
});

builder.Services.AddScoped<FilterPayment>(); // Đăng ký filter với DI

builder.Services.AddControllers(options =>
{
    options.Filters.Add<FilterPayment>(); // Đăng ký filter toàn cục
});


var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.UseSession();



app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=HangHoa}/{action=Index}/{id?}");


    endpoints.MapControllerRoute(
       name: "payment",
       pattern: "api/Payments/payment-method",
       defaults: new { controller = "Payment", action = "PaymentMethod" });

});

app.Run();
//http://localhost:5159/api/Payment/payment-callback?
//http://localhost:5159/{controller}/{action}
