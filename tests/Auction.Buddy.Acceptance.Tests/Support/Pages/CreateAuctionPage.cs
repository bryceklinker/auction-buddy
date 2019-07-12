using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Auction.Buddy.Acceptance.Tests.Support.Pages
{
    public class CreateAuctionPage : Page
    {
        private readonly List<JObject> _createdAuctions;
        public JObject LastAuctionDetails => _createdAuctions.LastOrDefault();

        public IEnumerable<JObject> CreatedAuctions => _createdAuctions;
        
        public CreateAuctionPage(WebDriverContext context) 
            : base(context)
        {
            _createdAuctions = new List<JObject>();
        }

        public void Navigate()
        {
            Navigate("/create-auction");
        }

        public async Task CreateAuction(string name, DateTime auctionDate)
        {
            var auctionDateString = auctionDate.ToString("MM/dd/yyyy");
            
            await Driver.WaitForElementByTestId("create-auction");
            Driver.FindElementByTestId("create-auction-name-input").SendKeys(name);
            Driver.FindElementByTestId("create-auction-date-input").SendKeys(auctionDateString);
            Driver.FindElementByTestId("create-auction-save-button").Click();
            
            _createdAuctions.Add(JObject.FromObject(new { name, auctionDate = auctionDateString}, JsonSerializer.Create(new JsonSerializerSettings { DateParseHandling = DateParseHandling.None })));
        }

        public async Task CreateAuctionAndWaitForDetails(string name, DateTime auctionDate)
        {
            await CreateAuction(name, auctionDate);
            await Driver.WaitForElementByTestId("auction-details");
        }

        public bool HasValidationErrors()
        {
            return Driver.FindElementsByTestId("validation-errors").Any();
        }
    }
}