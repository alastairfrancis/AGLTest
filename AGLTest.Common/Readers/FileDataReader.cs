using System;
using System.IO;

namespace AGLTest.Common.Readers
{
    /// <summary>
    /// Read data from file
    /// </summary>
    public class FileDataReader : IDataReader
    {
        public Uri Path { get; }

        public FileDataReader(Uri path)
        {
            Path = path;
        }

        public string Read()
        {
            return File.ReadAllText(Path.AbsolutePath);
        }
    }
}
