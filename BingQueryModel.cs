namespace searchfight
{
    public class BingResponseModel
    {
        public BingWebPage webPages { get; set; }
    }

    public class BingWebPage{
        public long totalEstimatedMatches { get; set; }
    }
}