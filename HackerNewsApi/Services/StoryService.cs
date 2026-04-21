
using HackerNewsApi.Clients;
using HackerNewsApi.Models;
using Microsoft.Extensions.Caching.Memory;

namespace HackerNewsApi.Services
{
    public class StoryService : IStoryService
    {
        private readonly IHackerNewsClient _client;

        private readonly IMemoryCache _cache;

        public StoryService( IHackerNewsClient client, IMemoryCache cache)
        {
            _client = client;
            _cache = cache;
        }

        public async Task<List<StoryDto>> GetBestStoriesAsync(int n)
        {
            //get cache data first
            if(_cache.TryGetValue("best_stories", out List<StoryDto> cachedStories))
            {
                return cachedStories.Take(n).ToList();  
            }


            //get all ids
            var ids = await _client.GetBestStoryIdsAsync();

            //take first n ids
            var selectedIds = ids.Take(50);

            //create tasks instead of awaiting immediately
            var tasks = selectedIds.Select(id => _client.GetStoryByIdAsync(id));

            //execute all req in parallel
            var items = await Task.WhenAll(tasks);

            var stories =  items.Where(item => item != null)
                .Select(item => new StoryDto
                {
                    Title = item.Title ?? "No title",
                    Uri = item.Url ?? string.Empty,
                    PostedBy = item.By,
                    Time = DateTimeOffset.FromUnixTimeSeconds(item.Time).UtcDateTime,
                    Score = item.Score,
                    CommentCount = item.Descendants

                })
                .OrderByDescending(s=> s.Score) .ToList();

                //store in cache for atleast 5 mins
                _cache.Set("best_stories", stories, TimeSpan.FromMinutes(5));

                return stories.Take(n).ToList();



        }

    }
}
