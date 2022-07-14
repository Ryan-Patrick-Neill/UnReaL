using Microsoft.EntityFrameworkCore;
using UnReaL.Database;
using UnReaL.Repository;

var builder = WebApplication.CreateBuilder(args);
var root = Directory.GetCurrentDirectory();

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddMvc();
builder.Services.AddDbContext<UnReaLContext>(options 
    => options.UseSqlServer($"Server = (localdb)\\mssqllocaldb; Database = {root}\\UnReaL.mdf; Trusted_Connection = True;"));
builder.Services.AddScoped<IUnReaLService, UnReaLService>();
builder.Services.AddScoped<IBijectionService, BijectionService>();

builder.Configuration.SetBasePath(root);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/UnReaL/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=UnReaL}/{action=Create}/{id?}");

app.Run();
