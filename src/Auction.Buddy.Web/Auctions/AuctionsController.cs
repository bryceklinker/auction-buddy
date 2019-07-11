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
        private readonly IGetAllAuctionsInteractor _getAllAuctionsInteractor;

        public AuctionsController(ICreateAuctionInteractor createAuctionInteractor, 
            IGetAllAuctionsInteractor getAllAuctionsInteractor)
        {
            _createAuctionInteractor = createAuctionInteractor;
            _getAllAuctionsInteractor = getAllAuctionsInteractor;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _getAllAuctionsInteractor.ExecuteAsync());
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