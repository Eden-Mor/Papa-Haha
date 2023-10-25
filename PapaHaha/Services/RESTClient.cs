using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Font = Microsoft.Maui.Font;

namespace PapaHaha.Services
{
    public class RESTClient
    {
        private readonly HttpClient httpClient;

        public RESTClient()
        {
            httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("User-Agent", "Papa Haha (https://github.com/Xennso/Papa-Haha)");
        }

        public async Task<HttpResponseMessage> GetAsync(string url, string acceptEncoding = "application/json")
        {
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(acceptEncoding));

            HttpResponseMessage response = await httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                return response;
            }
            else
            {

                return response;
            }
        }
    }
}
