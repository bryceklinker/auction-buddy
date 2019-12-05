using System;
using System.Threading.Tasks;
using Auction.Buddy.Core.Auctions;
using Auction.Buddy.Core.Auctions.Commands;
using Auction.Buddy.Core.Auctions.Queries;
using Auction.Buddy.Core.Auctions.ViewModels;
using Auction.Buddy.Core.Common;
using Auction.Buddy.Core.Common.Commands;
using Auction.Buddy.Core.Common.Queries;
using Auction.Buddy.Core.Common.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Auction.Buddy.Api.Auctions
{
    [ApiController]
    [Route("auctions")]
    public class AuctionsController : ControllerBase
    {
        private readonly CommandBus _commandBus;
        private readonly QueryBus _queryBus;

        public AuctionsController(CommandBus commandBus, QueryBus queryBus)
        {
            _commandBus = commandBus;
            _queryBus = queryBus;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateAuctionViewModel viewModel)
        {
            var command = new CreateAuctionCommand(viewModel.Name, viewModel.AuctionDate);
            var result = await _commandBus.ExecuteAsync<CreateAuctionCommand, AuctionId>(command);
            return CreatedAtRoute("GetAuctionById", new {id = result.Result.ToString()}, null);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var query = new GetAuctionsListQuery();
            var result = await _queryBus.ExecuteAsync<GetAuctionsListQuery, ListViewModel<AuctionListItemViewModel>>(query);
            return Ok(result);
        }

        [HttpGet("{id}", Name = "GetAuctionById")]
        public Task<IActionResult> GetById([FromRoute] string id)
        {
            throw new NotImplementedException();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] string id, [FromBody] UpdateAuctionViewModel viewModel)
        {
            var auctionId = IdentityBase.FromExistingId<AuctionId>(id);
            var command = new UpdateAuctionCommand(auctionId, viewModel.Name, viewModel.AuctionDate);
            await _commandBus.ExecuteAsync(command);
            return Ok();
        }
    }
}