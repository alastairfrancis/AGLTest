using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace AGLTest.Common.Models
{
    /// <summary>
    /// Representation of a person
    /// </summary>
    public class Person
    {
        public string Name { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public GenderType Gender { get; set; }
        public uint Age { get; set; }
        public IEnumerable<Pet> Pets { get; set; }

        public Person()
        {
            Pets = new List<Pet>();
        }
    }
}
