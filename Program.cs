using System;
using System.Net;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Linq;
using System.Collections.Generic;

namespace searchfight
{
    class Program
    {
        private static string googleKey = "AIzaSyAoa6FxUYEg8YkNzPw4ri0DpV9EnRbxVHA";
        private static string bingKey = "17a271b424764ae5985950f1bf1f2cb2";
        private static string GOOGLE_API_URL = String.Format("https://www.googleapis.com/customsearch/v1?key={0}&cx=017576662512468239146:omuauf_lfve&q=", googleKey);
        private static string BING_API_URL = "https://api.cognitive.microsoft.com/bing/v5.0/search?q=";
        static void Main(string[] args)
        {
            ValidateArguments(args);

            var programmingLanguages = GetProgrammingLanguageList(args);

            programmingLanguages.ForEach(item =>
                 Console.WriteLine("{0} Google Search: {1} MSN Search: {2}", item.Name, item.GoogleTotalResult, item.BingTotalResult)
            );

            Console.WriteLine("Google Winner: {0}", programmingLanguages.GetGoogleWinner);
            Console.WriteLine("MSN Winner: {0}", programmingLanguages.GetBingWinner);
            Console.WriteLine("TOTAL Winner: {0}", programmingLanguages.GetTotalWinner);
        }

        private static ProgrammingLanguageList GetProgrammingLanguageList(string[] args){
            return new ProgrammingLanguageList(args.Select(item =>
            {
                GoogleResponseModel googleSearchResult = GetGoogleSearchResult(GetGoogleUrl(item)).Result;
                BingResponseModel bingSearchResult = GetBingSearchResult(GetBingUrl(item)).Result;
                return new ProgrammingLanguage(item, GetGoogleTotalResults(googleSearchResult), GetBingTotalResults(bingSearchResult));
            }).ToList());
        }

        private static void ValidateArguments(string[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("Please enter the name of two programming languages");
                Environment.Exit(0);
            }
        }

        private static bool IsFirstPMWinner(int googleFirstPMResult, int googleSecondPMResult) =>
            googleFirstPMResult > googleSecondPMResult;


        private static int GetGoogleTotalResults(GoogleResponseModel result) =>
            result.queries.request.First().totalResults;

        private static int GetBingTotalResults(BingResponseModel result) =>
            result.webPages.totalEstimatedMatches;

        private static string GetGoogleUrl(string query) =>
            GOOGLE_API_URL + query;
        private static string GetBingUrl(string query) =>
            BING_API_URL + query;

        private static HttpWebRequest GetRequest(string url, bool isBingSearch) => 
            isBingSearch ? GetBingRequest((HttpWebRequest)WebRequest.Create(url)) : (HttpWebRequest)WebRequest.Create(url);


        private static HttpWebRequest GetBingRequest(HttpWebRequest request)
        {
            request.Headers["Ocp-Apim-Subscription-Key"] = bingKey;
            return request;
        }

        private static async Task<WebResponse> GetResponse(string url, bool isBingSearch) =>
            await GetRequest(url, isBingSearch).GetResponseAsync();

        private static async Task<Stream> GetResponseStream(string url, bool isBingSearch) =>
            (await GetResponse(url, isBingSearch)).GetResponseStream();

        private static async Task<T> GetSearchResult<T>(string url, bool isBingSearch = false) where T : class
        {
            using (Stream responseStream = await GetResponseStream(url, isBingSearch))
                return GetObject<T>(responseStream);
        }

        private static async Task<GoogleResponseModel> GetGoogleSearchResult(string url) =>
            await GetSearchResult<GoogleResponseModel>(url);
        private static async Task<BingResponseModel> GetBingSearchResult(string url) =>
            await GetSearchResult<BingResponseModel>(url, true);

        private static T GetObject<T>(Stream responseStream) where T : class =>
            JsonConvert.DeserializeObject<T>(new StreamReader(responseStream, Encoding.UTF8).ReadToEnd());


    }

}
