namespace searchfight
{
    public class ProgrammingLanguage
    {
        public string Name { get; set; }
        public int GoogleTotalResult { get; set; }

        public int BingTotalResult { get; set; }

        public int GetTotalResult => GoogleTotalResult + BingTotalResult;

        public ProgrammingLanguage(string name, int googleTotalResult, int bingTotalResult){
            Name = name;
            GoogleTotalResult = googleTotalResult;
            BingTotalResult = bingTotalResult;
        }
    }
}