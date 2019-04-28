using Xunit;
using AGLTest.Common.Config;
using AGLTest.Common.Services;

namespace AGLTest.UnitTest
{
    public class LoadDataTests
    {
        /// <summary>
        /// Test loading valid, and invalid data
        /// </summary>
        public LoadDataTests()
        {
        }

        [Theory]
        [InlineData("http://agl-developer-test.azurewebsites.net/people.json")]
        [InlineData("TestData/StandardTest.json")]
        public void ValidData(string url)
        {
            var result = GetService(url).GetAll();
            Assert.True(result.Success);
        }

        [Theory]
        [InlineData("TestData/InvalidJson.json")]
        [InlineData("TestData/InvalidProperties.json")]
        [InlineData("TestData/InvalidEnums.json")]
        public void InvalidData(string url)
        {
            var result = GetService(url).GetAll();
            Assert.False(result.Success);
        }

        private IPetService GetService(string dataUrl)
        {
            var config = new DataFeedConfig(dataUrl);
            return new PetService(config);
        }
    }
}
