using System;
using System.IO;
using Microsoft.Extensions.CommandLineUtils;
using Newtonsoft.Json;

namespace AGLTest.DataViewer.IO
{
    /// <summary>
    /// Write text to stdout, and stderr.
    /// </summary>
    public class OutputWriter
    {
        private readonly TextWriter _out;       // standard out
        private readonly TextWriter _err;       // standard error

        public OutputWriter(CommandLineApplication app)
        {
            _out = app.Out;
            _err = app.Error;
        }

        /// <summary>
        /// Write message to stdout
        /// </summary>
        /// <param name="message"></param>
        public void Write(string message = null)
        {
            _out.WriteLine(message);
        }

        /// <summary>
        /// Write object in JSON format to stdout
        /// </summary>
        /// <param name="o"></param>
        public void WriteJson(object o)
        {
            _out.WriteLine(JsonConvert.SerializeObject(o, Formatting.Indented));
        }

        /// <summary>
        /// Write error reason to stderr
        /// </summary>
        public void WriteFail(string message)
        {
            _err.WriteLine(message);
        }

        /// <summary>
        /// Write exception details to stderr
        /// </summary>
        public void WriteFail(Exception ex)
        {
            _err.WriteLine(ex.Message);
        }
    }
}
