using System;
using System.IO;

namespace AGLTest.Common.Config
{
    /// <summary>
    /// Configuration options for data feed
    /// </summary>
    public class DataFeedConfig
    {
        public Uri Url { get; set; }

        public DataFeedConfig()
        {
        }

        public DataFeedConfig(string url)
        {
            SetUrl(url);
        }

        public DataFeedConfig(Uri url)
        {
            Url = url;
        }

        public Uri SetUrl(string url)
        {
            if (Uri.IsWellFormedUriString(url, UriKind.RelativeOrAbsolute))
            {
                if (File.Exists(url))
                {
                    // if relative path defined, resolve to full path for the uri schema
                    var fullPath = Path.GetFullPath(url);
                    Url = new Uri($"file://{fullPath}");
                }
                else
                {
                    Url = new Uri(url);
                }
            }

            return Url;
        }
    }
}
