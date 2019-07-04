Feature: Login
  
  Scenario: Successful Login
    Given I have valid credentials
    When I login
    Then I should see auctions
  
  Scenario: Failed Login
    Given I have invalid credentials
    When I login
    Then I should see unauthorized