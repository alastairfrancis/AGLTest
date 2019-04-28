using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using AGLTest.Common.Models;

namespace AGLTest.Common.JsonParser
{
    /// <summary>
    /// Custom JSON converter override deserialisation to filter results
    /// </summary>
    public class FilteredJsonConverter : JsonConverter
    {
        private readonly PetFilterCriteria _criteria;

        public FilteredJsonConverter(PetFilterCriteria filterCriteria)
        {
            _criteria = filterCriteria;
        }

        public override bool CanRead
        {
            get { return true; }
        }

        public override bool CanWrite
        {
            get { return false; }
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var ret = new List<Person>();

            if (reader.TokenType == JsonToken.StartArray)
            {
                reader.Read();
                while (reader.TokenType != JsonToken.EndArray)
                {
                    var person = (Person)serializer.Deserialize(reader, typeof(Person));

                    // if we have pet filters, apply them to the deserialised person
                    if (_criteria != null)
                    {
                        if (person.Pets == null)
                        {
                            // no pets, no filters to apply
                            person = null;
                        }
                        else
                        {
                            // filter pets by name
                            if ((_criteria.PetNames != null) && _criteria.PetNames.Any())
                            {
                                person.Pets = person.Pets.Where(pet => _criteria.PetNames.Any(name => pet.Name == name));
                            }

                            // filter pets of type
                            if ((_criteria.PetTypes != null) && _criteria.PetTypes.Any())
                            {
                                person.Pets = person.Pets.Where(pet => _criteria.PetTypes.Any(petType => pet.Type == petType));
                            }

                            // if no matching pets, don't use this person
                            if (!person.Pets.Any())
                            {
                                person = null;
                            }
                        }
                    }

                    if (person != null)
                    {
                        // if person has no pets, return an empty list, as opposed to JSON parser null default value
                        // this is safer for client code as it doesn't have to check for null, and empty lists
                        if (person.Pets == null)
                        {
                            person.Pets = new List<Pet>();
                        }

                        ret.Add(person);
                    }

                    reader.Read();
                }
            }

            return ret;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(List<Person>);
        }
    }
}
