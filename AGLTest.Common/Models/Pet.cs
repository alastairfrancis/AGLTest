using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace AGLTest.Common.Models
{
    /// <summary>
    /// Representation of a pet
    /// </summary>
    public class Pet
    {
        public string Name { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public PetType Type { get; set; }
    }
}
