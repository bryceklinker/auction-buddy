Feature: Remove Item from Auction
  As an auction organizer
  I want to be able to remove items from an auction
  So that I can track items for upcoming auctions

  Scenario: Remove Item from Auction
    Given an auction with one item
    When I remove an item from the auction
    Then I should see no items in the auction