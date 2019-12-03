Feature: Add Item to Auction
  As an auction organizer
  I want to be able to add items to the auction
  So that I can track items for upcoming auctions
  
  Scenario: Add Item to Auction
    Given an existing auction
    When I add an item to the auction
    Then I should see one item in the auction