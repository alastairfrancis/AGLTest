using System;

namespace AGLTest.Common.Readers
{
    public interface IDataReader
    {
        Uri Path { get; }

        string Read();
    }
}
