using Newtonsoft.Json.Linq;

namespace AdbrainTest.Services
{
    public interface IRedditService
    {
        JObject GetListings(string subReddit);
        void SaveListings(JObject listings);
    }
}
