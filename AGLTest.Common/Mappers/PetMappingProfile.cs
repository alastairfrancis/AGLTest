using AutoMapper;
using System.Collections.Generic;
using AGLTest.Common.Models;
using AGLTest.Common.Models;

namespace AGLTest.Common.Mappers
{
    /// <summary>
    /// Autoregister mapping profiles if using dependency injection
    /// If not using dependency injection, the mappers will have to be added manually during initialisation
    /// </summary>
    public class PetMappingProfile : Profile
    {
        public PetMappingProfile()
        {
            RegisterMappings();
        }

        private void RegisterMappings()
        {
            CreateMap<IEnumerable<Person>, GenderPetView> ().ConvertUsing<GenderPetViewConverter>();
        }
    }
}