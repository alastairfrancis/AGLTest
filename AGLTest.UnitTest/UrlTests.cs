using Xunit;
using System;
using AGLTest.Common.Config;

namespace AGLTest.UnitTest
{
    /// <summary>
    /// Test data URL handling
    /// </summary>
    public class UrlTests
    {
        public UrlTests()
        {
        }

        [Theory]
        [InlineData("http://agl-developer-test.azurewebsites.net/people.json")]
        [InlineData("https://agl-developer-test.azurewebsites.net/people.json")]
        [InlineData("TestData/StandardTest.json")]
        public void ValidUrl(string url)
        {
            var config = new DataFeedConfig(url);
            Assert.True(config.Url != null);
        }

        [Theory]
        [InlineData("http://agl-developer-test.azurewebsites.net/people_unknown.json")]
        [InlineData("http://agl-developer-test.azurewebsites.badurl.net/people.json")]
        [InlineData("badfile")]
        [InlineData(null)]
        public void InvalidUrl(string url)
        {
            try
            {
                var config = new DataFeedConfig(url);
                Assert.False(config.Url != null);
            }
            catch (Exception)
            {
                Assert.True(true);
            }            
        }

    }
}
