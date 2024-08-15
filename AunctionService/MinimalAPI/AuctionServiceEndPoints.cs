using AunctionService.Dtos;
using AunctionService.Services;
using Microsoft.AspNetCore.Mvc;

namespace AunctionService.MinimalAPI
{
    public static class AuctionServiceEndPoints
    {
        public static void MapAuctionEndPoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("api/auctions");
            //
            group.MapGet("", GetAllAction);
            //
            group.MapGet("/{id}", GetAuctionById);
            //
            group.MapPost("", CreateAuction);
            //
            group.MapDelete("/{id}", RemoveAuctions);
        }

        [HttpGet]
        public static async Task<IResult> GetAllAction(
            IAuctionService auctionService
            )
        {
            try
            {
                var auctions = await auctionService.GetAllAuction();
                if (auctions == null)
                {
                    return Results.NotFound(new { Message = "There arent any auctions yet" });
                }
                return Results.Ok(auctions);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        [HttpGet("{Id}")]
        public static async Task<IResult> GetAuctionById(
            IAuctionService auctionService, 
            Guid Id)
        {
            try
            {
                var auction = await auctionService.GetById(Id);
                if (auction == null)
                {
                    return Results.NotFound(new { Message = "There isn't any auction with this ID" });
                }
                return Results.Ok(auction);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }


        [HttpPost]

        public static async Task<IResult> CreateAuction(
            IAuctionService auctionService, 
            CreateAuctionDto createDto)
        {
            try
            {
                var auction = await auctionService.CreateAuction(createDto);
                if(auction == null)
                {
                    return Results.BadRequest();
                }
                return Results.Created();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        [HttpDelete]
        public static async Task<IResult> RemoveAuctions(
            IAuctionService auctionService,
            Guid Id
            )
        {
            try
            {
                var result = await auctionService.DeleleAuction(Id);
                if (!result)
                {
                    return Results.NotFound("There isn't any auction with this ID");
                }
                return Results.NoContent();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
    }
}
