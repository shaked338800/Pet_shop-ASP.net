using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PetStrore.Data;
using PetStrore.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ShopDbContext>();
builder.Services.AddTransient<ShopDbContext>();
builder.Services.AddSingleton<IDataService, DataService>();
var app = builder.Build();
app.UseStaticFiles();
using (var scope = app.Services.CreateScope())
{
    var ctx = scope.ServiceProvider.GetRequiredService<ShopDbContext>();
    ctx.Database.EnsureCreated();
   
}
app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute("Default", "{controller=Home}/{action=HomePage}");
});



app.Run();
