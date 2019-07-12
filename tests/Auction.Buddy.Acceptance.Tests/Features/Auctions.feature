Feature: Auctions
  Background: 
    Given I have logged in
    
  Scenario: Create Auction Successful
    When I create an auction
    Then I should see the new auction
    
  Scenario: Create Auction Failure
    When I create an invalid auction
    Then I should see validation errors
    
  Scenario: View Auctions List
    Given auctions already exist
    When I view auctions
    Then I should see all auctions
    
  Scenario: View Auction Detail
    Given auctions already exist
    When I select an auction
    Then I should see the auction's details