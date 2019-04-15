using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace searchfight
{
    class Program
    {

        static void Main(string[] args)
        {
            ValidateArguments(args);

            var searchEngineList = new List<ISearchEngine>();

            args.ToList().ForEach( language => {
                searchEngineList.Add(new GoogleSearchEngine(language));
                searchEngineList.Add(new BingSearchEngine(language));
            });

            var results = searchEngineList
                                    .Select(x=> x.GetSearchResult())
                                    .ToList();

            var groupLanguages = results
                                    .GroupBy(item => item.Name);

            var getGlobalWinner = groupLanguages
                                    .Select(x => new { Name=x.Key, Total = x.Sum( y => y.Total) })
                                    .OrderByDescending( z => z.Total)
                                    .First();
            var getGoogleWinner = results
                                    .Where(x => x.Engine == "GOOGLE")
                                    .OrderByDescending(x => x.Total)
                                    .First();
            var getBingWinner = results
                                    .Where(x => x.Engine == "BING")
                                    .OrderByDescending(x => x.Total)
                                    .First();

            var tupleResults = groupLanguages
                                .Select(group => new Tuple<string, long, long>(group.Key, 
                                        group.Where(x => x.Engine == "GOOGLE").First().Total, 
                                        group.Where(x => x.Engine == "BING").First().Total))
                                .ToList();

            tupleResults.ForEach( t =>
                 Console.WriteLine("{0} Google Search: {1} Bing Search: {2}", t.Item1, t.Item2, t.Item3)
            );

            Console.WriteLine("Google Winner: {0}", getGoogleWinner.Name);
            Console.WriteLine("Bing Winner: {0}", getBingWinner.Name);
            Console.WriteLine("TOTAL Winner: {0}", getGlobalWinner.Name);

        }


        private static void ValidateArguments(string[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("Please enter the name of two programming languages");
                Environment.Exit(0);
            }
        }

      

    }

}
