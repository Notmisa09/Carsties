using AuctionService.Entities;
using AunctionService.Dtos;

namespace AunctionService.Services
{
    public interface IAuctionService
    {
        Task<List<AuctionDto>> GetAllAuction();
        Task<AuctionDto> GetById(Guid id);
        Task<Auction> CreateAuction(CreateAuctionDto CauctionDto);
        Task<bool> UpdateAuction(Guid id, UpdateAuctionDto AuctionDto);
        Task<bool> DeleleAuction(Guid id);
    }
}
