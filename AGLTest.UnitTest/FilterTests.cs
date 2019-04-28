using Xunit;
using System;
using System.Linq;
using System.Collections.Generic;
using AGLTest.Common.Config;
using AGLTest.Common.Services;
using AGLTest.Common.Models;

namespace AGLTest.UnitTest
{
    /// <summary>
    /// Test the pet search filter
    /// </summary>
    public class FilterTests
    {
        private const string _testFile = "TestData/StandardTest.json";

        public FilterTests()
        {
        }

        [Fact]
        public void FilterAll()
        {
            // compare counts of all pets using filter, and nom-filtered results

            var petService = GetService(_testFile);
            var filter = new PetFilterCriteria()
            {
                PetTypes = (IEnumerable<PetType>)Enum.GetValues(typeof(PetType))
            };

            var filterResult = petService.Search(filter);
            Assert.True(filterResult.Success);

            var allResult = petService.GetAll();
            Assert.True(allResult.Success);

            Assert.True(allResult.Data.SelectMany(x => x.Pets).Count() == filterResult.Data.SelectMany(x => x.Pets).Count());
        }

        [Theory]
        [InlineData(PetType.Cat)]
        [InlineData(PetType.Dog)]
        [InlineData(PetType.Fish)]
        public void FilterByPetType(PetType petType)
        {
            // find matching pet types

            var petService = GetService(_testFile);
            var filter = new PetFilterCriteria()
            {
                PetTypes = new List<PetType>() { petType }
            };

            var filterResult = petService.Search(filter);

            Assert.True(filterResult.Success);
            Assert.True(filterResult.Data.SelectMany(x => x.Pets).Count() > 0);
        }

        [Fact]
        public void FilterByPetName()
        {
            // compare a pet name with a filtered result

            var petService = GetService(_testFile);

            var allResult = petService.GetAll();
            Assert.True(allResult.Success);

            var firstPet = allResult.Data.SelectMany(x => x.Pets).First();

            var filter = new PetFilterCriteria()
            {
                PetNames = new List<string>() { firstPet.Name }
            };

            var filterResult = petService.Search(filter);
            Assert.True(filterResult.Success);

            // the filtered result should include the name, but should have different pet counts
            Assert.True(allResult.Data.SelectMany(x => x.Pets).Count() != filterResult.Data.SelectMany(x => x.Pets).Count());
            Assert.True(filterResult.Data.SelectMany(x => x.Pets).First().Name == firstPet.Name);
        }

        private IPetService GetService(string dataUrl)
        {
            var config = new DataFeedConfig(dataUrl);
            return new PetService(config);
        }
    }
}
