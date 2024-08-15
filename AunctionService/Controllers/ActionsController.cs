//using AuctionService.Data;
//using AunctionService.Dtos;
//using AutoMapper;
//using Microsoft.AspNetCore.Mvc;

//namespace AuctionService.Controllers;

//[ApiController]
//[Route("api/auctions")]

//public class ActionsController : ControllerBase
//{
//    private readonly AuctionDbContext _context;
//    private readonly Services.AuctionService _service;
//    private readonly IMapper _mapper;

//    public ActionsController(AuctionDbContext context,
//     IMapper mapper, 
//     Services.AuctionService service)
//    {
//        _service = service;
//        _context = context;
//        _mapper = mapper;
//    }

//    [HttpGet]
//    public async Task<ActionResult<List<AuctionDto>>> GetAllAuctions()
//    {
//        var auctions = await _service.GetAllAuction();
//        if(auctions == null) return NotFound();
//        return auctions;
//    }

//    [HttpGet("{id}")]
//    public async Task<ActionResult<AuctionDto>> GetById(Guid id)
//    {
//        var auction = await _service.GetById(id);
//        if(auction == null) return NotFound("No entities were found");
//        return auction;
//    }

//    [HttpPost]
//    public async Task<ActionResult<AuctionDto>> CreateAuction(CreateAuctionDto CauctionDto)
//    {
//        var auction = await _service.CreateAuction(CauctionDto);
//        if (auction == null) return BadRequest("Could not save changes to the DB");
//        return CreatedAtAction(nameof(GetById), new { auction.Id }, _mapper.Map<AuctionDto>(auction));
//    }

//    [HttpPost("{id}")]
//    public async Task<ActionResult> UpdateAuction(Guid id, UpdateAuctionDto AuctionDto)
//    {
//        var result = await _service.UpdateAuction(id, AuctionDto);
//        if (result) return Created();
//        return BadRequest("Problem saving changes");
//    }


//    [HttpDelete("{id}")]
//    public async Task<ActionResult> DeleteAuction(Guid id)
//    {
//        var result = await _service.DeleleAuction(id);
//        if (!result) return BadRequest("Could not delete this auction");
//        return Ok();
//    }
//}


