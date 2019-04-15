using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace searchfight
{

    public class BingSearchEngine : ISearchEngine
    {
        private static string bingKey = "47ae91fc36ca4648a22cd03fe71b8cad";
        private static string BING_API_URL = "https://api.cognitive.microsoft.com/bing/v7.0/search?q=";

        private string language;

        public BingSearchEngine(string language){
            this.language = language;
        }

        public ResponseModel GetSearchResult() {
            BingResponseModel result = GetSearchResult(GetUrl(language)).Result;

            return new ResponseModel{
                Engine="BING",
                Name = language,
                Total = result.webPages.totalEstimatedMatches
            };

        } 

        private long GetTotalResults(BingResponseModel result) => result.webPages.totalEstimatedMatches;

        private string GetUrl(string query) => BING_API_URL + query;

        private HttpWebRequest GetRequest(string url) =>
             GetRequest((HttpWebRequest)WebRequest.Create(url));

        private HttpWebRequest GetRequest(HttpWebRequest request)
        {
            request.Headers["Ocp-Apim-Subscription-Key"] = bingKey;
            return request;
        }

        private async Task<WebResponse> GetResponse(string url) =>
            await GetRequest(url).GetResponseAsync();

        private async Task<Stream> GetResponseStream(string url) =>
            (await GetResponse(url)).GetResponseStream();

        private async Task<T> GetSearchResult<T>(string url) where T : class
        {
            using (Stream responseStream = await GetResponseStream(url))
                return GetObject<T>(responseStream);
        }

        private async Task<BingResponseModel> GetSearchResult(string url) =>
            await GetSearchResult<BingResponseModel>(url);

        private T GetObject<T>(Stream responseStream) where T : class =>
            JsonConvert.DeserializeObject<T>(new StreamReader(responseStream, Encoding.UTF8).ReadToEnd());


    }
}