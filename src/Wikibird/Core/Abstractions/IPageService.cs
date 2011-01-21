using System.Collections.Generic;
using Wikibird.Models;

namespace Wikibird.Core.Abstractions
{
    public interface IPageService
    {
        Page GetPage(string name);
        void SavePage(string name, string title, string content, string[] tags);
        IEnumerable<PageTitle> GetPageNames();
        ListResult ListCategory(string category);
        ListResult ListTag(string tag);
    }

    public class ListResult
    {
        public IEnumerable<Page> Pages { get; set; }
        public int TotalCount { get; set; }
    }
    
}