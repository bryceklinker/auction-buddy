Feature: View Current Auction

Scenario: View Current Auction
  Given I have auctions
  When I log into the application
  Then I should see the current auction
