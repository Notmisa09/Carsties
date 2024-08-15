using AuctionService.Data;
using Microsoft.EntityFrameworkCore;
using AunctionService;
using AunctionService.MinimalAPI;
using AunctionService.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AuctionRegService(builder.Configuration);
builder.Services.AddTransient< IAuctionService , AuctionService.Services.AuctionService> ();
builder.Services.AddControllers();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddDbContext<AuctionDbContext>(opt =>
{
    opt.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

var app = builder.Build();

app.UseAuthorization();
app.MapAuctionEndPoints();


try
{
    DbInitializer.IniDb(app);
}
catch (Exception e)
{
    Console.WriteLine(e.Message);
    throw;
}

app.Run();