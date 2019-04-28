using System.Linq;
using System.Collections.Generic;
using AGLTest.DataViewer.IO;
using AGLTest.Common.Config;
using AGLTest.Common.Services;
using AGLTest.Common.Models;
using AGLTest.DataViewer.Config;
using AGLTest.DataViewer.Types;

namespace AGLTest.DataViewer.View
{
    /// <summary>
    /// Base class implementation for custom view classes
    /// Custom views can be created by inheriting this base, and implementing the IDataView interface
    /// </summary>
    public class DataView
    {
        protected readonly IPetService _petService;
        protected readonly OutputWriter _output;

        public DataView(OutputWriter output, string url)
        {
            _output = output;

            var config = new DataFeedConfig(url);
            _petService = new PetService(config);
        }

        /// <summary>
        /// Fetch data from server, and display it
        /// </summary>
        /// <returns>Exit code</returns>
        public int Execute(ViewerConfig config)
        {
            var result = LoadData(config.petTypes);

            if (result.Success)
            {
                Display(result.Data);
                return ExitCodes.SUCCESS;
            }

            _output.WriteFail(result.Error);
            return ExitCodes.ERROR;
        }

        /// <summary>
        /// Load filtered results from service
        /// </summary>
        public virtual Result<IEnumerable<Person>> LoadData(IEnumerable<PetType> petTypes)
        {
            if (petTypes.Any())
            {
                var criteria = new PetFilterCriteria() { PetTypes = petTypes };
                return _petService.Search(criteria);
            }
            else
            {
                return _petService.GetAll();
            }
        }

        /// <summary>
        /// Display the results
        /// </summary>
        public virtual void Display(IEnumerable<Person> data)
        {
            _output.WriteJson(data);
        }
    }
}
