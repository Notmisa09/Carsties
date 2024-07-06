using AuctionService.Entities;
using AunctionService.Data;
using AunctionService.Dtos;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AunctionService.Controllers;
[ApiController]
[Route("api/auctions")]

public class ActionsController : ControllerBase
{
    private readonly AuctionDbContext _context;
    private readonly IMapper _mapper;

    public ActionsController(AuctionDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<List<AuctionDto>>> GetAllAuctions()
    {
        var auctions = await _context.Aunctions.Include(x => x.Item)
            .OrderBy(x => x.Item.Make)
            .ToListAsync();

        return _mapper.Map<List<AuctionDto>>(auctions);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<AuctionDto>> GetById(Guid id)
    {
        var auction = await _context.Aunctions.
            Include(x => x.Item)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (auction == null) return NotFound();
        return _mapper.Map<AuctionDto>(auction);
    }

    [HttpPost]
    public async Task<ActionResult<AuctionDto>> CreateAuction(CreateAuctionDto CauctionDto)
    {
        var auction = _mapper.Map<Auction>(CauctionDto);
        auction.Seller = "text";
        await _context.Aunctions.AddAsync(auction);

        var result = await _context.SaveChangesAsync() > 0;

        if (!result) return BadRequest("Could not save changes to the DB");

        return CreatedAtAction(nameof(GetById), new { auction.Id }, _mapper.Map<AuctionDto>(auction));
    }

    [HttpPost("{id}")]
    public async Task<ActionResult> UpdateAuction(Guid id, UpdateAuctionDto AuctionDto)
    {
        var auction = await _context.Aunctions.Include(x => x.Item)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (auction == null) return NotFound();

        auction.Item.Make = AuctionDto.Make ?? auction.Item.Make;
        auction.Item.Model = AuctionDto.Model ?? auction.Item.Model;
        auction.Item.Color = AuctionDto.Color ?? auction.Item.Color;
        auction.Item.Mileage = AuctionDto.Mileage ?? auction.Item.Mileage;
        auction.Item.Year = AuctionDto.Year ?? auction.Item.Year;

        var result = await _context.SaveChangesAsync() > 1;

        if (result) return Created();
        return BadRequest("Problem saving changes");
    }



    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteAuction(Guid id)
    {
        var auction = await _context.Aunctions.Where(x => x.Id == id)
            .Include(x => x.Item)
            .FirstOrDefaultAsync(x => x.Id == id);
        if (auction == null) return NotFound();

        _context.Aunctions.Remove(auction);
        var result = await _context.SaveChangesAsync() > 1;
        
        if (!result) return BadRequest("Could not delete this auction");

        return Ok();
    }
}


