Feature: Update Auction Item
  As an auction organizer
  I want to be able to update items in the auction
  So that I can track items for upcoming auctions

  Scenario: Update Auction Item
    Given an auction with one item
    When I update the auction item
    Then I should see the updated auction item