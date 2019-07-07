// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by SpecFlow (http://www.specflow.org/).
//      SpecFlow Version:3.0.0.0
//      SpecFlow Generator Version:3.0.0.0
// 
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------
#region Designer generated code
#pragma warning disable
namespace Auction.Buddy.Acceptance.Tests.Features
{
    using TechTalk.SpecFlow;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "3.0.0.0")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public partial class AuctionsFeature : Xunit.IClassFixture<AuctionsFeature.FixtureData>, System.IDisposable
    {
        
        private static TechTalk.SpecFlow.ITestRunner testRunner;
        
        private Xunit.Abstractions.ITestOutputHelper _testOutputHelper;
        
#line 1 "Auctions.feature"
#line hidden
        
        public AuctionsFeature(AuctionsFeature.FixtureData fixtureData, Xunit.Abstractions.ITestOutputHelper testOutputHelper)
        {
            this._testOutputHelper = testOutputHelper;
            this.TestInitialize();
        }
        
        public static void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "Auctions", null, ProgrammingLanguage.CSharp, ((string[])(null)));
            testRunner.OnFeatureStart(featureInfo);
        }
        
        public static void FeatureTearDown()
        {
            testRunner.OnFeatureEnd();
            testRunner = null;
        }
        
        public virtual void TestInitialize()
        {
        }
        
        public virtual void ScenarioTearDown()
        {
            testRunner.OnScenarioEnd();
        }
        
        public virtual void ScenarioInitialize(TechTalk.SpecFlow.ScenarioInfo scenarioInfo)
        {
            testRunner.OnScenarioInitialize(scenarioInfo);
            testRunner.ScenarioContext.ScenarioContainer.RegisterInstanceAs<Xunit.Abstractions.ITestOutputHelper>(_testOutputHelper);
        }
        
        public virtual void ScenarioStart()
        {
            testRunner.OnScenarioStart();
        }
        
        public virtual void ScenarioCleanup()
        {
            testRunner.CollectScenarioErrors();
        }
        
        public virtual void FeatureBackground()
        {
#line 2
  #line 3
    testRunner.Given("I have logged in", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
        }
        
        void System.IDisposable.Dispose()
        {
            this.ScenarioTearDown();
        }
        
        [Xunit.FactAttribute(DisplayName="Create Auction Successful")]
        [Xunit.TraitAttribute("FeatureTitle", "Auctions")]
        [Xunit.TraitAttribute("Description", "Create Auction Successful")]
        public virtual void CreateAuctionSuccessful()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Create Auction Successful", null, ((string[])(null)));
#line 5
  this.ScenarioInitialize(scenarioInfo);
            this.ScenarioStart();
#line 2
  this.FeatureBackground();
#line 6
    testRunner.When("I create an auction", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 7
    testRunner.Then("I should see the new auction", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Xunit.FactAttribute(DisplayName="Create Auction Failure")]
        [Xunit.TraitAttribute("FeatureTitle", "Auctions")]
        [Xunit.TraitAttribute("Description", "Create Auction Failure")]
        public virtual void CreateAuctionFailure()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Create Auction Failure", null, ((string[])(null)));
#line 9
  this.ScenarioInitialize(scenarioInfo);
            this.ScenarioStart();
#line 2
  this.FeatureBackground();
#line 10
    testRunner.When("I create an invalid auction", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 11
    testRunner.Then("I should see validation errors", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "3.0.0.0")]
        [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
        public class FixtureData : System.IDisposable
        {
            
            public FixtureData()
            {
                AuctionsFeature.FeatureSetup();
            }
            
            void System.IDisposable.Dispose()
            {
                AuctionsFeature.FeatureTearDown();
            }
        }
    }
}
#pragma warning restore
#endregion
