using AuctionService.Data;
using AuctionService.Entities;
using AunctionService.Dtos;
using AunctionService.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AuctionService.Services
{
    public class AuctionService : IAuctionService
    {
        private readonly IMapper _mapper;
        private readonly AuctionDbContext _context;

        public AuctionService(IMapper mapper, AuctionDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<List<AuctionDto>> GetAllAuction(){

            var auction = await _context.Aunctions.Include(x => x.Item).ToListAsync();
            return _mapper.Map<List<AuctionDto>>(auction);
        }        

        public async Task<AuctionDto> GetById(Guid id){
             var auction = await _context.Aunctions
             .Include(x => x.Item)
             .FirstOrDefaultAsync(x => x.Id == id);

             return _mapper.Map<AuctionDto>(auction);
        }

        public async Task<Auction> CreateAuction(CreateAuctionDto CauctionDto)
        {
            var auction = _mapper.Map<Auction>(CauctionDto);
            auction.Seller = "text";
            await _context.Aunctions.AddAsync(auction);

            var result = await _context.SaveChangesAsync() > 0;

            if(!result)
            {
                return null;
            }
            else
            {
                return auction;
            }

        }

        public async Task<bool> UpdateAuction(Guid id, UpdateAuctionDto AuctionDto)
        {
            var auction = await _context.Aunctions.Include(x => x.Item)
            .FirstOrDefaultAsync(x => x.Id == id);

            if(auction == null) { return false; }

            auction.Item.Make = AuctionDto.Make ?? auction.Item.Make;
            auction.Item.Model = AuctionDto.Model ?? auction.Item.Model;
            auction.Item.Color = AuctionDto.Color ?? auction.Item.Color;
            auction.Item.Mileage = AuctionDto.Mileage ?? auction.Item.Mileage;
            auction.Item.Year = AuctionDto.Year ?? auction.Item.Year;

            var result = await _context.SaveChangesAsync() > 1;

            return result;
        }

        public async Task<bool> DeleleAuction(Guid id)
        {
            var auction = await _context.Aunctions.Where(x => x.Id == id)
            .Include(x => x.Item)
            .FirstOrDefaultAsync(x => x.Id == id);
            
            if (auction == null) return false;

            _context.Aunctions.Remove(auction);
            var result = await _context.SaveChangesAsync() > 1;
            return result;
        }

    }
}
