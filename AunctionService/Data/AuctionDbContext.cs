using AuctionService.Entities;
using Microsoft.EntityFrameworkCore;

namespace AunctionService.Data
{
    public class AuctionDbContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<Auction> Aunctions { get; set;}
    }
}
