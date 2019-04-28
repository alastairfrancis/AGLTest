using System.Collections.Generic;

namespace AGLTest.Common.Models
{
    /// <summary>
    /// Custom view of pet names via owner gender
    /// </summary>
    public class GenderPetView
    {
        public IDictionary<GenderType, List<string>> Pets { get; set; }
    }
}
