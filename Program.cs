using System;
using System.Linq;

namespace searchfight
{
    class Program
    {

        static void Main(string[] args)
        {
            ValidateArguments(args);

            var programmingLanguages = GetProgrammingLanguageList(args);

            programmingLanguages.ForEach(item =>
                 Console.WriteLine("{0} Google Search: {1} Bing Search: {2}", item.Name, item.GoogleTotalResult, item.BingTotalResult)
            );

            Console.WriteLine("Google Winner: {0}", programmingLanguages.GetGoogleWinner);
            Console.WriteLine("Bing Winner: {0}", programmingLanguages.GetBingWinner);
            Console.WriteLine("TOTAL Winner: {0}", programmingLanguages.GetTotalWinner);
        }

        private static ProgrammingLanguageList GetProgrammingLanguageList(string[] args) =>
            new ProgrammingLanguageList( args.Select(SearchEngineHandler.Handle).ToList() );
        

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
