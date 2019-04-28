using System.Collections.Generic;
using System.Linq;
using AGLTest.Common.Extensions;
using AGLTest.Common.Models;

namespace AGLTest.DataViewer.Config
{
    /// <summary>
    /// Configuration settings for viewer
    /// </summary>
    public class ViewerConfig
    {
        /// <summary>
        /// Filter criteria
        /// </summary>
        public List<PetType> petTypes { get; set; }

        public ViewerConfig()
        {
            petTypes = new List<PetType>();
        }
 
        /// <summary>
        /// Add pet type to filter criteria
        /// </summary>
        public void Add(string pet)
        {
            petTypes.Add(pet.ToEnum<PetType>());
        }

        /// <summary>
        /// Add range of pet types to filter criteria
        /// </summary>
        public void AddRange(IEnumerable<string> pets)
        {
            petTypes.AddRange(pets.Select(p => p.ToEnum<PetType>()));
        }
    }
}
