using Microsoft.SemanticKernel;
using SimpleFeedReader;
using System.ComponentModel;

namespace AgentSamples
{
    internal class MyNewsPlugin
    {
        [KernelFunction("get_news")]
        [Description("Gets the latest news from today for a category.")]
        [return: Description("A list of today's news items.")]
        public async Task<IEnumerable<FeedItem>> GetNewsAsync(Kernel kernel, string category)
        {
            var reader = new FeedReader();
            var items = await reader.RetrieveFeedAsync($"http://www.nytimes.com/services/xml/rss/nyt/{category}.xml");

            return items.Take(5).ToList();
        }
    }
}
