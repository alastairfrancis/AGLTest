using AutoMapper;
using System.Collections.Generic;
using AGLTest.DataViewer.IO;
using AGLTest.Common.Models;
using AGLTest.DataViewer.View;

namespace AGLTest.DataViewer.CustomViews
{
    /// <summary>
    /// Display pets by owner gender
    /// </summary>
    public class GenderView : DataView, IDataView
    {
        public const string Name = "gender";

        public GenderView(OutputWriter output, string url)
            : base(output, url)
        {
        }

        public override void Display(IEnumerable<Person> data)
        {
            var viewData = Mapper.Map<IEnumerable<Person>, GenderPetView>(data);
            _output.WriteJson(viewData.Pets);
        }
    }
}
