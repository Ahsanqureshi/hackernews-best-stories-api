using HackerNewsApi.Models;

namespace HackerNewsApi.Clients
{
    public class HackerNewsClient : IHackerNewsClient
    {
        public readonly HttpClient _httpClient;

        public HackerNewsClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<int>> GetBestStoryIdsAsync()
        {
            // call endpoint to get list of best story ids
            var ids = await _httpClient.GetFromJsonAsync<List<int>>("https://hacker-news.firebaseio.com/v0/beststories.json");

            return ids ?? new List<int>();
        }
        public async Task<HackerNewsItems> GetStoryByIdAsync(int id)
        {
            //fetch detials of a single story
            return await _httpClient.GetFromJsonAsync<HackerNewsItems>($"https://hacker-news.firebaseio.com/v0/item/{id}.json");

        }
    }
}
