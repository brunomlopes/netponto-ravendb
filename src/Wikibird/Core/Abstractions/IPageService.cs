using Wikibird.Models;

namespace Wikibird.Core.Abstractions
{
    public interface IPageService
    {
        Page GetPage(string name);
        void SavePage(string name, string title, string content);
    }
}