using System.Collections.Generic;
using Wikibird.Models;

namespace Wikibird.Core.Abstractions
{
    public interface IPageService
    {
        Page GetPage(string name);
        void SavePage(string name, string title, string content, string[] tags);
        IEnumerable<PageTitle> GetPageNames();
        ListCategoryResult ListCategory(string category);
    }

    public class ListCategoryResult
    {
        public IEnumerable<Page> Pages { get; set; }
        public int TotalCount { get; set; }
    }
}