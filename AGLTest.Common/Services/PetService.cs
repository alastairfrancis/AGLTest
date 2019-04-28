using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using AGLTest.Common.Config;
using AGLTest.Common.Readers;
using AGLTest.Common.Models;
using AGLTest.Common.JsonParser;

namespace AGLTest.Common.Services
{
    /// <summary>
    /// Pet owner service
    /// </summary>
    public class PetService : IPetService
    {
        private readonly Uri _feedUrl;

        public PetService(DataFeedConfig config)
        {
            _feedUrl = config.Url;
        }

        /// <summary>
        /// Get all pet owners
        /// </summary>
        public Result<IEnumerable<Person>> GetAll()
        {
            return ReadData();
        }

        /// <summary>
        /// Get pet owners filtered by search criteria
        /// </summary>
        public Result<IEnumerable<Person>> Search(PetFilterCriteria searchCriteria)
        {
            return ReadData(searchCriteria.HasCriteria() ? searchCriteria : null);
        }

        private Result<IEnumerable<Person>> ReadData(PetFilterCriteria filter = null)
        {           
            try
            {
                var dataReader = ReaderFactory.CreateReader(_feedUrl);
                var json = dataReader.Read();
                var converter = new FilteredJsonConverter(filter);

                var settings = new JsonSerializerSettings
                {
                    Formatting = Formatting.Indented,                                   // slightly inefficient, but easier to read
                    ContractResolver = new CamelCasePropertyNamesContractResolver(),    // useful to prevent issues with enum matching
                    MissingMemberHandling = MissingMemberHandling.Error,                // throw error on unmapped properties
                    Converters = new List<JsonConverter>() { converter }
                };

                var data = JsonConvert.DeserializeObject<List<Person>>(json, settings);
                return Result<IEnumerable<Person>>.OnSuccess(data);

            }
            catch (Exception ex)
            {
                return Result<IEnumerable<Person>>.OnFail(ex.Message);
            }
        }
    }
}
