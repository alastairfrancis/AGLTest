using System.ComponentModel;

namespace AGLTest.Common.Models
{
    /// <summary>
    /// Pet owner gender
    /// </summary>
    public enum GenderType
    {
        [DisplayName("Female")]
        Female = 1,

        [DisplayName("Male")]
        Male = 2
    }
}
