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
            AssertAllTrue(SearchQuery);
        }

        [Fact]
        public void OneIncludeWord()
        {
            var wordToken = new WordToken("ali");
            Include.Add(wordToken);
            SearchQuery.ParseString("+ali");
            AssertAllTrue(SearchQuery);
        }

        [Fact]
        public void OneExcludeeWord()
        {
            var wordToken = new WordToken("ali");
            Exclude.Add(wordToken);
            SearchQuery.ParseString("-ali");
            AssertAllTrue(SearchQuery);
        }


        [Fact]
        public void OneFromAll()
        {
            var wordToken1 = new WordToken("ali");
            var wordToken2 = new WordToken("hassan");
            var wordToken3 = new WordToken("parsa");
            Exclude.Add(wordToken1);
            Include.Add(wordToken2);
            MustInclude.Add(wordToken3);
            SearchQuery.ParseString("-ali +hassan parsa");
            AssertAllTrue(SearchQuery);
        }

        
        [Fact]
        public void ThreeMustWord()
        {
            var wordToken1 = new WordToken("ali");
            var wordToken2 = new WordToken("hassan");
            var wordToken3 = new WordToken("parsa");
            MustInclude.Add(wordToken1);
            MustInclude.Add(wordToken2);
            MustInclude.Add(wordToken3);
            SearchQuery.ParseString("ali hassan parsa");
            AssertAllTrue(SearchQuery);
        }


        private void AssertAllTrue(SearchQuery<string> searchQuery)
        {
            Assert.Equal(SearchQuery.MustIncludeTokens.Count, MustInclude.Count);
            Assert.Equal(SearchQuery.IncludeTokens.Count, Include.Count);
            Assert.Equal(SearchQuery.ExcludeTokens.Count, Exclude.Count);
        }


    }
}