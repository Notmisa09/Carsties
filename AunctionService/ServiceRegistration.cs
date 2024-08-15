using AuctionService.Data;
using System.Reflection;

namespace AunctionService
{
    public static class ServiceRegistration
    {
        public static void AuctionRegService(this IServiceCollection service , IConfiguration configuration)
        {
            service.AddTransient<AuctionDbContext>();
            service.AddAutoMapper(Assembly.GetExecutingAssembly());
            service.AddTransient<AuctionService.Services.AuctionService>();
        }
    }
}
