using System.Collections.Generic;
using System.Linq;

namespace AGLTest.Common.Models
{
    /// <summary>
    /// Filter criteria for search
    /// </summary>
    public class PetFilterCriteria
    {
        public IEnumerable<string> PetNames { get; set; }
        public IEnumerable<PetType> PetTypes { get; set; }

        /// <summary>
        /// Return false if no filter criteria defined
        /// </summary>
        public bool HasCriteria()
        {
            return ((PetNames != null) && PetNames.Any()) ||
                   ((PetTypes != null) && PetTypes.Any());
        }
    }
}
