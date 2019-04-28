using System.Collections.Generic;
using AGLTest.DataViewer.IO;
using AGLTest.Common.Models;
using AGLTest.DataViewer.View;

namespace AGLTest.DataViewer.CustomViews
{
    /// <summary>
    /// Default view
    /// Display all pet owner properties
    /// </summary>
    public class DefaultView : DataView, IDataView
    {
        public const string Name = "full";

        public DefaultView(OutputWriter output, string url)
            : base(output, url)
        {
        }

        public new void Display(IEnumerable<Person> data)
        {
            _output.WriteJson(data);
        }
    }
}
