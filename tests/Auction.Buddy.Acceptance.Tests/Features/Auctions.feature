Feature: Auctions
  Background: 
    Given I have logged in
  
  
  Scenario: Create Auction
    When I create an auction
    Then I should see the new auction