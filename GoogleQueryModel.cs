using System.Collections.Generic;

namespace searchfight
{
    public class GoogleResponseModel
    {
        public GoogleQueryModel queries { get; set; }    

    }
    public class GoogleQueryModel
    {
        public List<GoogleRequestModel> request { get; set; }
    }

    public class GoogleRequestModel {
        public string title { get; set; }         
        public int totalResults { get; set; }
    }
}