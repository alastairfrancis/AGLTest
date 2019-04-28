using System;
using System.Net.Http;

namespace AGLTest.Common.Readers
{
    /// <summary>
    /// Read data from http(s) source
    /// </summary>
    public class HttpDataReader : IDataReader
    {
        public Uri Path { get; }

        public HttpDataReader(Uri url)
        {
            Path = url;
        }

        public string Read()
        {
            var client = new HttpClient();
            var response = client.GetAsync(Path).Result;
            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadAsStringAsync().Result;
            }

            throw new Exception($"Could read data from http client. Reason: {response.ReasonPhrase}");
        }
    }
}
