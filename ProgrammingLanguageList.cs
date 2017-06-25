using System.Collections.Generic;
using System.Linq;

namespace searchfight
{
    public class ProgrammingLanguageList : List<ProgrammingLanguage>
    {
        public string GetGoogleWinner => this.OrderByDescending( item => item.GoogleTotalResult).First().Name;
        public string GetBingWinner => this.OrderByDescending( item => item.BingTotalResult).First().Name;
        public string GetTotalWinner => this.OrderByDescending( item => item.GetTotalResult).First().Name;

        public ProgrammingLanguageList(List<ProgrammingLanguage> programmingLanguageList){
            this.AddRange(programmingLanguageList);
        }
    }
}