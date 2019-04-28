using System;
using System.Linq;
using System.Collections.Generic;
using AGLTest.Common.Extensions;
using AGLTest.DataViewer.IO;

namespace AGLTest.DataViewer.View
{
    /// <summary>
    /// Factory to create instances implementing the view interface
    /// </summary>
    /// <typeparam name="T">View interface</typeparam>
    public class DataViewFactory<T>
    {
        /// <summary>
        /// Collection of view names mapped with concrete class types
        /// </summary>
        private readonly Dictionary<string, Type> _viewClasses;

        /// <summary>
        /// Property with name of view
        /// </summary>
        private static readonly string ViewClassPropertyName = "Name";

        /// <summary>
        /// Interface type implemented by concrete views
        /// </summary>
        private static readonly Type ViewClassInterface = typeof(IDataView);

        public IEnumerable<string> ViewNames
        {
            get
            {
                return _viewClasses.Keys.ToList();
            }
        }

        public DataViewFactory()
        {
            _viewClasses = new Dictionary<string, Type>();
            InitClassDefinitions();
        }

        public T Create(string viewName, OutputWriter output, string url)
        {
            return (T)Activator.CreateInstance(_viewClasses[viewName], output, url);
        }

        private void InitClassDefinitions()
        {
            // get all concrete classes implementing the interface
            var classes = typeof(T).GetImplementedClasses();
            foreach (Type clazz in classes)
            {
                // look for the public const string that contains the view name
                var fieldInfos = clazz.GetConstants();

                var constantInfo = fieldInfos.Where(fi => fi.Name == ViewClassPropertyName);
                if (constantInfo.Count() == 1)
                {
                    var viewName = constantInfo.First().GetRawConstantValue() as string;
                    _viewClasses[viewName] = clazz;
                }
            }
        }

    }
}
