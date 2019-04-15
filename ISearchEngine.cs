using System.Threading.Tasks;
namespace searchfight
{
    public interface ISearchEngine
    {
        ResponseModel GetSearchResult();
    }
}