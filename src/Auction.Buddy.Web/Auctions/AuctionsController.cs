using System.Threading.Tasks;
using Auction.Buddy.Core.Auctions;
using Auction.Buddy.Core.Auctions.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Auction.Buddy.Web.Auctions
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuctionsController : ControllerBase
    {
        private readonly ICreateAuctionInteractor _createAuctionInteractor;

        public AuctionsController(ICreateAuctionInteractor createAuctionInteractor)
        {
            _createAuctionInteractor = createAuctionInteractor;
        }

        [HttpGet("{id:int}", Name = "GetAuctionById")]
        public Task<IActionResult> GetById([FromRoute] int id)
        {
            return Task.FromResult<IActionResult>(Ok());
        }
        
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateAuctionDto dto)
        {
            var auctionDto = await _createAuctionInteractor.ExecuteAsync(dto);
            return CreatedAtRoute("GetAuctionById", new {id = auctionDto.Id}, auctionDto);
        }
    }
}