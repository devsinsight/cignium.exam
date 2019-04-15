using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace searchfight
{

    public class GoogleSearchEngine : ISearchEngine
    {
        private static string googleKey = "AIzaSyAoa6FxUYEg8YkNzPw4ri0DpV9EnRbxVHA";
        private static string GOOGLE_API_URL = String.Format("https://www.googleapis.com/customsearch/v1?key={0}&cx=017576662512468239146:omuauf_lfve&q=", googleKey);

        private string language;

        public GoogleSearchEngine(string language){
            this.language = language;
        }

        public ResponseModel GetSearchResult() {
            GoogleResponseModel result = GetSearchResult(GetUrl(language)).Result;

            return new ResponseModel{
                Engine = "GOOGLE",
                Name = language,
                Total = result.queries.request[0].totalResults
            };

        } 

        private long GetTotalResults(GoogleResponseModel result) =>
            result.queries.request.First().totalResults;

        private string GetUrl(string query) => GOOGLE_API_URL + query;

        private HttpWebRequest GetRequest(string url) => (HttpWebRequest)WebRequest.Create(url);

        private async Task<WebResponse> GetResponse(string url) =>
            await GetRequest(url).GetResponseAsync();

        private async Task<Stream> GetResponseStream(string url) =>
            (await GetResponse(url)).GetResponseStream();

        private async Task<GoogleResponseModel> GetSearchResult(string url) =>
            await GetSearchResult<GoogleResponseModel>(url);

        private async Task<T> GetSearchResult<T>(string url) where T : class
        {
            using (Stream responseStream = await GetResponseStream(url))
                return GetObject<T>(responseStream);
        }

        private T GetObject<T>(Stream responseStream) where T : class =>
            JsonConvert.DeserializeObject<T>(new StreamReader(responseStream, Encoding.UTF8).ReadToEnd());
    }
}