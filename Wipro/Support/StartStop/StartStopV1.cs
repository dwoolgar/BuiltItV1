using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Threading;
using TechTalk.SpecFlow;
using Wipro.Support.Contexts;
using Wipro.Support.PageMapping.PageElementMaping;

namespace Wipro.Support.StartStop
{
    [Binding]
    public class StartStop
    {
        [BeforeFeature()]
        public static void BeforeFeature()
        {
            CustomContexts.TheDriver = new ChromeDriver(@"C:\VSProjects\Wipro\packages\Drivers\");

            CustomContexts.MappingObject = PageElementMapping.GetElementMappingObject(@"C:\VSProjects\Wipro\Wipro\Support\Mapping\weather.mapping");
        }

        [BeforeScenario()]
        public static void BeforeScenario()
        {

            ((IWebDriver)CustomContexts.TheDriver).Navigate().GoToUrl("https://weather-acceptance.herokuapp.com/");
            ((IWebDriver)CustomContexts.TheDriver).Manage().Window.Maximize();

            Thread.Sleep(2000);
        }

        [AfterScenario()]
        public static void TearDown()
        { 
            ((IWebDriver)CustomContexts.TheDriver).Navigate().GoToUrl("about:blank");
            
            //If using MSTest
            // Put this in to stop consecutive tests from failing for it in feature.cs file - TestInitialize method
            if (FeatureContext.Current.ContainsKey("MsTestContext"))
            {
                FeatureContext.Current.Remove("MsTestContext");
            }
        }

        [AfterFeature()]
        public static void CleanupFeature()
        {
            ((IWebDriver)CustomContexts.TheDriver).Quit();
        }
    }
}
