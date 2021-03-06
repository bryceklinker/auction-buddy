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
    [Route("auctions/{auctionId}/items")]
    public class AuctionItemsController : ControllerBase
    {
        private readonly CommandBus _commandBus;
        private readonly QueryBus _queryBus;

        public AuctionItemsController(CommandBus commandBus, QueryBus queryBus)
        {
            _commandBus = commandBus;
            _queryBus = queryBus;
        }

        [HttpPost]
        public async Task<IActionResult> AddItem([FromRoute] string auctionId,
            [FromBody] AddAuctionItemViewModel viewModel)
        {
            var id = IdentityBase.FromExistingId<AuctionId>(auctionId);
            var item = new AuctionItem(viewModel.Name, viewModel.Donor, viewModel.Description, viewModel.Quantity);
            var command = new AddAuctionItemCommand(id, item);
            await _commandBus.ExecuteAsync(command);
            return CreatedAtRoute("GetAuctionItems", new {auctionId}, null);
        }

        [HttpGet(Name = "GetAuctionItems")]
        public async Task<IActionResult> GetItems([FromRoute] string auctionId)
        {
            var query = new GetAuctionItemsListQuery(auctionId);
            var viewModel = await _queryBus.ExecuteAsync<GetAuctionItemsListQuery, ListViewModel<AuctionItemListItemViewModel>>(query);
            return Ok(viewModel);
        }

        [HttpDelete("{itemName}")]
        public async Task<IActionResult> RemoveItem([FromRoute] string auctionId, [FromRoute] string itemName)
        {
            var id = IdentityBase.FromExistingId<AuctionId>(auctionId);
            var command = new RemoveAuctionItemCommand(id, itemName);
            await _commandBus.ExecuteAsync(command);
            return NoContent();
        }

        [HttpPut("{itemName}")]
        public async Task<IActionResult> UpdateItem(
            [FromRoute] string auctionId, 
            [FromRoute] string itemName,
            [FromBody] UpdateAuctionItemViewModel viewModel)
        {
            var id = IdentityBase.FromExistingId<AuctionId>(auctionId);
            var command = new UpdateAuctionItemCommand(
                id,
                itemName,
                viewModel.NewName,
                viewModel.NewDonor,
                viewModel.NewDescription,
                viewModel.NewQuantity);

            await _commandBus.ExecuteAsync(command);
            return NoContent();
        }
    }
}