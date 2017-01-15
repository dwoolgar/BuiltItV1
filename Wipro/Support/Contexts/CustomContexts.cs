using OpenQA.Selenium;
using TechTalk.SpecFlow;
using Wipro.Support.JProperties;
namespace Wipro.Support.Contexts
{
    static class CustomContexts
    {
        //--------------------------------------------------------------------------------------
        //--------------------------------------------------------------------------------------
        // FEATURE CONTEXT
        //--------------------------------------------------------------------------------------
        //--------------------------------------------------------------------------------------
        private const string TheDriverKey = "TheDriver";

        private const string MappingObjectKey = "MappingObject";

        //--------------------------------------------------------------------------------------
        // Current SuperDriver
        //--------------------------------------------------------------------------------------
        public static IWebDriver TheDriver
        {
            get { return (IWebDriver)FeatureContext.Current[TheDriverKey]; }
            set { FeatureContext.Current[TheDriverKey] = value; }
        }

        //--------------------------------------------------------------------------------------
        // Element Mapping Objects - General Mapping
        //--------------------------------------------------------------------------------------
        public static JavaPropertiesbits MappingObject
        {
            get { return (JavaPropertiesbits)FeatureContext.Current[MappingObjectKey]; }
            set { FeatureContext.Current[MappingObjectKey] = value; }
        }
    }
}
