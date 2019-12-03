Feature: Update Auction
  As an auction organizer
  I want to be able to update auctions
  So that I can adjust the date of the auction
  
  Scenario: Update Existing Auction
    Given an existing auction
    When I update the auction
    Then the auction has been updated