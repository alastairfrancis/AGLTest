using System.Collections.Generic;
using AGLTest.Common.Models;

namespace AGLTest.Common.Services
{
    /// <summary>
    /// Pet owner service interface
    /// </summary>
    public interface IPetService
    {
        /// <summary>
        /// Get all pet owners
        /// </summary>
        Result<IEnumerable<Person>> GetAll();

        /// <summary>
        /// Get pet owners filtered by search criteria
        /// </summary>
        Result<IEnumerable<Person>> Search(PetFilterCriteria searchCriteria);
    }
}
