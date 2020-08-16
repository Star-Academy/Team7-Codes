using Xunit;
using Phase05.Model;
using System.Collections.Generic;
using System;

namespace Phase05.Test
{
    public class SearchQueryTests
    {

        private SearchQuery<string> searchQuery;
        private HashSet<WordToken> mustInclude;
        private HashSet<WordToken> include;
        private HashSet<WordToken> exclude;
        public SearchQueryTests()
        {
            searchQuery = new SearchQuery<string>();
            mustInclude = new HashSet<WordToken>();
            include = new HashSet<WordToken>();
            exclude = new HashSet<WordToken>();

        }

        [Fact]
        public void OneMustWord()
        {
            var wordToken = new WordToken("ali");
            mustInclude.Add(wordToken);
            searchQuery.ParseString("ali");
            AssertAllTrue(searchQuery);
        }

        [Fact]
        public void OneIncludeWord()
        {
            var wordToken = new WordToken("ali");
            include.Add(wordToken);
            searchQuery.ParseString("+ali");
            AssertAllTrue(searchQuery);
        }

        [Fact]
        public void OneExcludeeWord()
        {
            var wordToken = new WordToken("ali");
            exclude.Add(wordToken);
            searchQuery.ParseString("-ali");
            AssertAllTrue(searchQuery);
        }


        [Fact]
        public void OneFromAll()
        {
            var wordToken1 = new WordToken("ali");
            var wordToken2 = new WordToken("hassan");
            var wordToken3 = new WordToken("parsa");
            exclude.Add(wordToken1);
            include.Add(wordToken2);
            mustInclude.Add(wordToken3);
            searchQuery.ParseString("-ali +hassan parsa");
            AssertAllTrue(searchQuery);
        }


        [Fact]
        public void ThreeMustWord()
        {
            var wordToken1 = new WordToken("ali");
            var wordToken2 = new WordToken("hassan");
            var wordToken3 = new WordToken("parsa");
            mustInclude.Add(wordToken1);
            mustInclude.Add(wordToken2);
            mustInclude.Add(wordToken3);
            searchQuery.ParseString("ali hassan parsa");
            AssertAllTrue(searchQuery);
        }


        private void AssertAllTrue(SearchQuery<string> searchQuery)
        {
            Assert.Equal(this.searchQuery.MustIncludeTokens.Count, mustInclude.Count);
            Assert.Equal(this.searchQuery.IncludeTokens.Count, include.Count);
            Assert.Equal(this.searchQuery.ExcludeTokens.Count, exclude.Count);
        }


    }
}