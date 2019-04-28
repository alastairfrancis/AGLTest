using System;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using AGLTest.Common.Models;
using AGLTest.Common.Services;

namespace AGLTest.Web.Controllers
{
    /// <summary>
    /// Controller API used in Pet views
    /// Normally I would use a separate API server, and get the data via that
    /// </summary>
    [Produces("application/json")]
    [Route("api/pet")]
    public class PetController : Controller
    {
        private readonly IPetService _petService;

        public PetController(IPetService petService)
        {
            _petService = petService;
        }

        [HttpGet]       
        public IEnumerable<Person> GetAllOwners()
        {
            var result = _petService.GetAll();
            if (result.Success)
            {
                return result.Data;
            }

            throw new Exception(result.Error);
        }
        
        [HttpGet]
        [Route("ownerSearch")]
        public IEnumerable<Person> SearchForOwners([FromQuery] string petName, [FromQuery] PetType? petType)
        {
            var searchCriteria = new PetFilterCriteria();

            if (!string.IsNullOrWhiteSpace(petName))
            {
                searchCriteria.PetNames = new List<string>() { petName };
            }
                
            if (petType.HasValue)
            {
                searchCriteria.PetTypes = new List<PetType>() { petType.Value };
            }

            var result = _petService.Search(searchCriteria);
            if (result.Success)
            {
                return result.Data;
            }

            throw new Exception(result.Error);
        }

        [HttpGet]
        [Route("ownerGenderSearch")]
        public IDictionary<GenderType, List<string>> SearchForOwnerGenders([FromQuery] PetType? petType)
        {
            
            var searchCriteria = new PetFilterCriteria();

            if (petType.HasValue)
            {
                searchCriteria.PetTypes = new List<PetType>() { petType.Value };
            }

            var result = _petService.Search(searchCriteria);
            if (result.Success)
            {                
                return Mapper.Map<IEnumerable<Person>, GenderPetView>(result.Data).Pets;
            }

            throw new Exception(result.Error);
        }
    }
}
