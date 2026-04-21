using HackerNewsApi.Models;

namespace HackerNewsApi.Services
{
    public interface IStoryService
    {
        Task<List<StoryDto>> GetBestStoriesAsync(int n);
    }
}
