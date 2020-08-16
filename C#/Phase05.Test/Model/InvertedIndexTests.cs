using Xunit;
using Phase05.Model;
using Phase05.Model.Interface;
using Moq;
using System.Collections.Generic;
using System;
using Phase05.CustomException;

namespace Phase05.Test
{
    public class InvertedIndexTests
    {


        [Fact]
        public void AddNewToken()
        {
            var index = new Dictionary<IToken<string>, HashSet<ITokenInfo<string>>>();
            var invertedIndex = new InvertedIndex<string, string>(index);
            var addQuery = new AddQuery<string, string>(new WordToken("barca"), new DocumentInfo("spain"));
            invertedIndex.Add(addQuery);
            Assert.Equal(1, invertedIndex.Index.GetValueOrDefault(new WordToken("barca")).Count);
        }

        [Fact]
        public void AddTwoDocumentForTwoWord()
        {
            var index = new Dictionary<IToken<string>, HashSet<ITokenInfo<string>>>();
            var invertedIndex = new InvertedIndex<string, string>(index);
            var addQuery1 = new AddQuery<string, string>(new WordToken("barca"), new DocumentInfo("spain"));
            var addQuery2 = new AddQuery<string, string>(new WordToken("barca"), new DocumentInfo("catolania"));
            invertedIndex.Add(addQuery1);
            invertedIndex.Add(addQuery2);
            Assert.Equal(2, invertedIndex.Index.GetValueOrDefault(new WordToken("barca")).Count);
        }

        [Fact]
        public void AddTwoSameAddQuery()
        {
            var index = new Dictionary<IToken<string>, HashSet<ITokenInfo<string>>>();
            var invertedIndex = new InvertedIndex<string, string>(index);
            var addQuery1 = new AddQuery<string, string>(new WordToken("barca"), new DocumentInfo("spain"));
            invertedIndex.Add(addQuery1);
            invertedIndex.Add(addQuery1);
            Assert.Equal(1, invertedIndex.Index.GetValueOrDefault(new WordToken("barca")).Count);
        }

        [Fact]
        public void AddTwoWordWithSameDocument()
        {
            var index = new Dictionary<IToken<string>, HashSet<ITokenInfo<string>>>();
            var invertedIndex = new InvertedIndex<string, string>(index);
            var addQuery1 = new AddQuery<string, string>(new WordToken("barca"), new DocumentInfo("spain"));
            var addQuery2 = new AddQuery<string, string>(new WordToken("atleti"), new DocumentInfo("spain"));
            invertedIndex.Add(addQuery1);
            invertedIndex.Add(addQuery2);
            Assert.Equal(1, invertedIndex.Index.GetValueOrDefault(new WordToken("barca")).Count);
            Assert.Equal(1, invertedIndex.Index.GetValueOrDefault(new WordToken("atleti")).Count);
            Assert.Throws<NullReferenceException>(() => invertedIndex.Index.GetValueOrDefault(new WordToken("bayern")).Count);
        }

        private void AddSomeDataToInvertedIndex(InvertedIndex<string, string> invertedIndex)
        {
            var barca1 = new AddQuery<string, string>(new WordToken("barca"), new DocumentInfo("spain"));
            var barca2 = new AddQuery<string, string>(new WordToken("barca"), new DocumentInfo("catolania"));
            invertedIndex.Add(barca1);
            invertedIndex.Add(barca2);

            var atleti1 = new AddQuery<string, string>(new WordToken("atleti"), new DocumentInfo("spain"));
            invertedIndex.Add(atleti1);

            var manCity1 = new AddQuery<string, string>(new WordToken("mancity"), new DocumentInfo("manchester"));
            var manCity2 = new AddQuery<string, string>(new WordToken("mancity"), new DocumentInfo("england"));
            var manCity3 = new AddQuery<string, string>(new WordToken("mancity"), new DocumentInfo("uk"));
            invertedIndex.Add(manCity1);
            invertedIndex.Add(manCity2);
            invertedIndex.Add(manCity3);

            var manUnited1 = new AddQuery<string, string>(new WordToken("manunited"), new DocumentInfo("manchester"));
            invertedIndex.Add(manUnited1);

            var arsenal1 = new AddQuery<string, string>(new WordToken("arsenal"), new DocumentInfo("london"));
            var arsenal2 = new AddQuery<string, string>(new WordToken("arsenal"), new DocumentInfo("england"));
            invertedIndex.Add(arsenal1);
            invertedIndex.Add(arsenal2);

            var celtic1 = new AddQuery<string, string>(new WordToken("celtic"), new DocumentInfo("glasgow"));
            var celtic2 = new AddQuery<string, string>(new WordToken("celtic"), new DocumentInfo("scotland"));
            var celtic3 = new AddQuery<string, string>(new WordToken("celtic"), new DocumentInfo("uk"));
            invertedIndex.Add(celtic1);
            invertedIndex.Add(celtic2);
            invertedIndex.Add(celtic3);

        }

        private Mock<ISearchQuery<string>> MakeWantedMock(ISet<IToken<string>> must, ISet<IToken<string>> include, ISet<IToken<string>> exclude)
        {
            var searchQuery = new Mock<ISearchQuery<string>>();
            searchQuery.SetupProperty(x => x.MustIncludeTokens, new HashSet<IToken<string>>(must));
            searchQuery.SetupProperty(x => x.IncludeTokens, new HashSet<IToken<string>>(include));
            searchQuery.SetupProperty(x => x.ExcludeTokens, new HashSet<IToken<string>>(exclude));
            return searchQuery;
        }

        [Fact]
        public void FindTwoMustWord1()
        {
            var index = new Dictionary<IToken<string>, HashSet<ITokenInfo<string>>>();
            var invertedIndex = new InvertedIndex<string, string>(index);
            AddSomeDataToInvertedIndex(invertedIndex);
            var must = new HashSet<IToken<string>>()
                {
                    new WordToken("celtic"),
                    new WordToken("mancity")
                };

            var searchQuery = MakeWantedMock(must, new HashSet<IToken<string>>(), new HashSet<IToken<string>>());
            Assert.Equal(1, invertedIndex.Find(searchQuery.Object).Count);
        }

        [Fact]
        public void FindTwoMustWord2()
        {
            var index = new Dictionary<IToken<string>, HashSet<ITokenInfo<string>>>();
            var invertedIndex = new InvertedIndex<string, string>(index);
            AddSomeDataToInvertedIndex(invertedIndex);
            var must = new HashSet<IToken<string>>()
                {
                    new WordToken("celtic"),
                    new WordToken("barca")
                };

            var searchQuery = MakeWantedMock(must, new HashSet<IToken<string>>(), new HashSet<IToken<string>>());
            Assert.Throws<NoResultFoundException>(() => invertedIndex.Find(searchQuery.Object));
        }

        [Fact]
        public void FindthreeMustWord()
        {
            var index = new Dictionary<IToken<string>, HashSet<ITokenInfo<string>>>();
            var invertedIndex = new InvertedIndex<string, string>(index);
            AddSomeDataToInvertedIndex(invertedIndex);
            var must = new HashSet<IToken<string>>()
                {
                    new WordToken("celtic"),
                    new WordToken("mancity"),
                    new WordToken("arsenal")
                };

            var searchQuery = MakeWantedMock(must, new HashSet<IToken<string>>(), new HashSet<IToken<string>>());
            Assert.Throws<NoResultFoundException>(() => invertedIndex.Find(searchQuery.Object));
        }

        [Fact]
        public void FindOneMustOneExcludeWord()
        {
            var index = new Dictionary<IToken<string>, HashSet<ITokenInfo<string>>>();
            var invertedIndex = new InvertedIndex<string, string>(index);
            AddSomeDataToInvertedIndex(invertedIndex);

            var must = new HashSet<IToken<string>>()
                {
                    new WordToken("celtic")
                };

            var exclude = new HashSet<IToken<string>>()
                {
                    new WordToken("mancity")
                };

            var searchQuery = MakeWantedMock(must, new HashSet<IToken<string>>(), exclude);
            Assert.Equal(2, invertedIndex.Find(searchQuery.Object).Count);
        }

        [Fact]
        public void FindSingleNotFoundWord()
        {
            var index = new Dictionary<IToken<string>, HashSet<ITokenInfo<string>>>();
            var invertedIndex = new InvertedIndex<string, string>(index);
            AddSomeDataToInvertedIndex(invertedIndex);
            var must = new HashSet<IToken<string>>()
                {
                    new WordToken("chelsea")
                };

            var searchQuery = MakeWantedMock(must, new HashSet<IToken<string>>(), new HashSet<IToken<string>>());
            Assert.Throws<NoResultFoundException>(() => invertedIndex.Find(searchQuery.Object));
        }

        [Fact]
        public void FindOneMustWithOneIncludeNotFound()
        {
            var index = new Dictionary<IToken<string>, HashSet<ITokenInfo<string>>>();
            var invertedIndex = new InvertedIndex<string, string>(index);
            AddSomeDataToInvertedIndex(invertedIndex);
            var must = new HashSet<IToken<string>>()
                {
                    new WordToken("celtic")
                };

            var include = new HashSet<IToken<string>>()
                {
                    new WordToken("chelsea")
                };

            var searchQuery = MakeWantedMock(must, include, new HashSet<IToken<string>>());
            Assert.Equal(3, invertedIndex.Find(searchQuery.Object).Count);
        }

        [Fact]
        public void FindTwoIncludeWithThreeExclude()
        {
            var index = new Dictionary<IToken<string>, HashSet<ITokenInfo<string>>>();
            var invertedIndex = new InvertedIndex<string, string>(index);
            AddSomeDataToInvertedIndex(invertedIndex);
            var include = new HashSet<IToken<string>>()
                {
                    new WordToken("mancity"),
                    new WordToken("barca")
                };

            var exclude = new HashSet<IToken<string>>()
                {
                    new WordToken("manunited"),
                    new WordToken("arsenal"),
                    new WordToken("atleti")
                };

            var searchQuery = MakeWantedMock(new HashSet<IToken<string>>(), include, exclude);
            Assert.Equal(2, invertedIndex.Find(searchQuery.Object).Count);
        }

        [Fact]
        public void FindAllKinds()
        {
            var index = new Dictionary<IToken<string>, HashSet<ITokenInfo<string>>>();
            var invertedIndex = new InvertedIndex<string, string>(index);
            AddSomeDataToInvertedIndex(invertedIndex);
            var must = new HashSet<IToken<string>>()
                {
                    new WordToken("liverpool"),
                };

            var include = new HashSet<IToken<string>>()
                {
                    new WordToken("mancity"),
                    new WordToken("barca")
                };

            var exclude = new HashSet<IToken<string>>()
                {
                    new WordToken("manunited"),
                    new WordToken("arsenal"),
                    new WordToken("atleti")
                };

            var searchQuery = MakeWantedMock(must, include, exclude);
            Assert.Equal(2, invertedIndex.Find(searchQuery.Object).Count);
        }



    }
}