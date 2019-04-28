using Xunit;
using AutoMapper;
using System.Linq;
using System.Collections.Generic;
using AGLTest.Common.Config;
using AGLTest.Common.Services;
using AGLTest.Common.Models;
using AGLTest.Common.Mappers;
using AGLTest.Common.Extensions;

namespace AGLTest.UnitTest
{
    public class MapDataTests
    {
        private const string _testFile = "TestData/StandardTest.json";

        /// <summary>
        /// Test mapping data into other views
        /// </summary>
        public MapDataTests()
        {
            Mapper.Initialize(config =>
            {
                config.CreateMap<IEnumerable<Person>, GenderPetView>().ConvertUsing<GenderPetViewConverter>();
            });
        }

        [Theory]
        [InlineData("Cat")]
        [InlineData("Dog")]
        [InlineData("Fish")]
        public void GenderView(string pet)
        {            
            // check the mapping of data into gender view

            var petService = GetService(_testFile);
            var filter = new PetFilterCriteria()
            {
                PetTypes = new List<PetType>() { pet.ToEnum<PetType>() }
            };

            var filterResult = petService.Search(filter);
            Assert.True(filterResult.Success);

            var viewData = Mapper.Map<IEnumerable<Person>, GenderPetView>(filterResult.Data);

            // view maps pet owners into gender categories, ordered by pet name 
            foreach (var key in viewData.Pets.Keys)
            {
                var petNames = viewData.Pets[key];
                var filteredPetNames = filterResult.Data.Where(person => person.Gender == key).SelectMany(person => person.Pets).Select(p => p.Name);

                // check if sequences of ordered names are equal
                var orderedNames = filteredPetNames.Distinct().OrderBy(x => x);
                Assert.True(petNames.SequenceEqual(orderedNames, EqualityComparer<string>.Default));
            }
        }

        private IPetService GetService(string dataUrl)
        {
            var config = new DataFeedConfig(dataUrl);
            return new PetService(config);
        }
    }
}
