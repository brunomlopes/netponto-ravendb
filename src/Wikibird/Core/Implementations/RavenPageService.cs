using System;
using System.Collections.Generic;
using Wikibird.Core.Abstractions;
using Wikibird.Models;

namespace Wikibird.Core.Implementations
{
    public class RavenPageService : IPageService
    {
        public Page GetPage(string name)
        {
            throw new NotImplementedException();
        }

        public void SavePage(string name, string title, string content)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PageTitle> GetPageNames()
        {
            throw new NotImplementedException();
        }
    }
}