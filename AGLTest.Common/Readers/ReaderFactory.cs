using System;

namespace AGLTest.Common.Readers
{
    /// <summary>
    /// Create data reader based on the uri scheme
    /// </summary>
    public static class ReaderFactory
    {
        public static IDataReader CreateReader(Uri url)
        {
            switch (url.Scheme.ToLower())
            {
                case "file":
                    return new FileDataReader(url);

                case "http":
                case "https":
                    return new HttpDataReader(url);
            }

            throw new Exception($"Unsupported data feed scheme, url '{url}'");
        }
    }
}
