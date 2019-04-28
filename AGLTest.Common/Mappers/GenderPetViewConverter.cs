using AutoMapper;
using System.Linq;
using System.Collections.Generic;
using AGLTest.Common.Models;
using AGLTest.Common.Models;

namespace AGLTest.Common.Mappers
{
    /// <summary>
    /// Maps person collection to custom view of owner by gender
    /// </summary>
    public class GenderPetViewConverter : ITypeConverter<IEnumerable<Person>, GenderPetView>
    {
        public GenderPetView Convert(IEnumerable<Person> source, GenderPetView destination, ResolutionContext context)
        {
            var result = new GenderPetView();

            if (source != null)
            {
                result.Pets = source.GroupBy(p => p.Gender)
                                    .ToDictionary(
                                        group => group.Key,
                                        group => group.SelectMany(x => x.Pets).OrderBy(pet => pet.Name).Select(pet => pet.Name).Distinct().ToList()
                                    );
            }

            return result;
        }
    }
}
