using Xunit;
using Phase05.Model;
using System.Collections.Generic;
using System;

namespace Phase05.Test
{
    public class SearchQueryTests : IDisposable
    {

        private SearchQuery<string> SearchQuery;
        private HashSet<WordToken> MustInclude;
        private HashSet<WordToken> Include;
        private HashSet<WordToken> Exclude;
        public SearchQueryTests()
        {
            SearchQuery = new SearchQuery<string>();
            MustInclude = new HashSet<WordToken>();
            Include = new HashSet<WordToken>();
            Exclude = new HashSet<WordToken>();

        }

        public void Dispose()
        {

        }

        [Fact]
        public void OneMustWord()
        {
            var wordToken = new WordToken("ali");
            MustInclude.Add(wordToken);
            SearchQuery.ParseString("ali");
            Assert.Equal(MustInclude, SearchQuery.MustIncludeTokens);
        }


    }
}