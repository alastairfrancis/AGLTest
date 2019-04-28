using System.ComponentModel;

namespace AGLTest.Common.Models
{
    /// <summary>
    /// Types of pets
    /// </summary>
    public enum PetType
    {
        [DisplayName("Cat")]
        Cat = 1,

        [DisplayName("Dog")]
        Dog = 2,

        [DisplayName("Fish")]
        Fish = 3
    }
}
