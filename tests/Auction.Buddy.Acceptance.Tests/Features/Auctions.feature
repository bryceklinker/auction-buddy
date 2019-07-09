Feature: Auctions
  Background: 
    Given I have logged in
    
  Scenario: Create Auction Successful
    When I create an auction
    Then I should see the new auction
    
  Scenario: Create Auction Failure
    When I create an invalid auction
    Then I should see validation errors