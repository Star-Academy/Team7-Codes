using Xunit;
using Phase05.Model;
using Phase05.Model.Interface;
using Moq;
using System.Collections.Generic;
using System;
using System.Linq;

namespace Phase05.Test
{
    public class InvertedIndexTests : IDisposable
    {
        private InvertedIndex<string, string> InvertedIndex;
        private Dictionary<IToken<string>, HashSet<ITokenInfo<string>>> Dictionary;

        public InvertedIndexTests()
        {
            Dictionary = new Dictionary<IToken<string>, HashSet<ITokenInfo<string>>>();
            InvertedIndex = new InvertedIndex<string, string>(Dictionary);
        }

        public void Dispose()
        {

        }

        [Fact]
        public void AddNewToken()
        {
            var addQuery = new AddQuery<string, string>(new WordToken("barca"), new DocumentInfo("spain"));
            InvertedIndex.Add(addQuery);
            Assert.Equal(1, InvertedIndex.Dictionary.GetValueOrDefault(new WordToken("barca")).Count);
        }

        [Fact]
        public void AddTwoDocumentForTwoWord()
        {
            var addQuery1 = new AddQuery<string, string>(new WordToken("barca"), new DocumentInfo("spain"));
            var addQuery2 = new AddQuery<string, string>(new WordToken("barca"), new DocumentInfo("catolania"));
            InvertedIndex.Add(addQuery1);
            InvertedIndex.Add(addQuery2);
            Assert.Equal(2, InvertedIndex.Dictionary.GetValueOrDefault(new WordToken("barca")).Count);
        }

        [Fact]
        public void AddTwoSameAddQuery()
        {
            var addQuery1 = new AddQuery<string, string>(new WordToken("barca"), new DocumentInfo("spain"));
            InvertedIndex.Add(addQuery1);
            InvertedIndex.Add(addQuery1);
            Assert.Equal(1, InvertedIndex.Dictionary.GetValueOrDefault(new WordToken("barca")).Count);
        }

        [Fact]
        public void AddTwoWordWithSameDocument()
        {
            var addQuery1 = new AddQuery<string, string>(new WordToken("barca"), new DocumentInfo("spain"));
            var addQuery2 = new AddQuery<string, string>(new WordToken("atleti"), new DocumentInfo("spain"));
            InvertedIndex.Add(addQuery1);
            InvertedIndex.Add(addQuery2);
            Assert.Equal(1, InvertedIndex.Dictionary.GetValueOrDefault(new WordToken("barca")).Count);
            Assert.Equal(1, InvertedIndex.Dictionary.GetValueOrDefault(new WordToken("atleti")).Count);
            Assert.Throws<NullReferenceException>(() => InvertedIndex.Dictionary.GetValueOrDefault(new WordToken("bayern")).Count);
        }

        private void AddSomeDataToInvertedIndex()
        {
            var barca1 = new AddQuery<string, string>(new WordToken("barca"), new DocumentInfo("spain"));
            var barca2 = new AddQuery<string, string>(new WordToken("barca"), new DocumentInfo("catolania"));
            InvertedIndex.Add(barca1);
            InvertedIndex.Add(barca2);

            var atleti1 = new AddQuery<string, string>(new WordToken("atleti"), new DocumentInfo("spain"));
            InvertedIndex.Add(atleti1);

            var manCity1 = new AddQuery<string, string>(new WordToken("mancity"), new DocumentInfo("manchester"));
            var manCity2 = new AddQuery<string, string>(new WordToken("mancity"), new DocumentInfo("england"));
            var manCity3 = new AddQuery<string, string>(new WordToken("mancity"), new DocumentInfo("uk"));
            InvertedIndex.Add(manCity1);
            InvertedIndex.Add(manCity2);
            InvertedIndex.Add(manCity3);

            var manUnited1 = new AddQuery<string, string>(new WordToken("manunited"), new DocumentInfo("manchester"));
            InvertedIndex.Add(manUnited1);

            var arsenal1 = new AddQuery<string, string>(new WordToken("arsenal"), new DocumentInfo("london"));
            var arsenal2 = new AddQuery<string, string>(new WordToken("arsenal"), new DocumentInfo("england"));
            InvertedIndex.Add(arsenal1);
            InvertedIndex.Add(arsenal2);

            var celtic1 = new AddQuery<string, string>(new WordToken("celtic"), new DocumentInfo("glasgow"));
            var celtic2 = new AddQuery<string, string>(new WordToken("celtic"), new DocumentInfo("scotland"));
            var celtic3 = new AddQuery<string, string>(new WordToken("celtic"), new DocumentInfo("uk"));
            InvertedIndex.Add(celtic1);
            InvertedIndex.Add(celtic2);
            InvertedIndex.Add(celtic3);

        }

        private Mock<ISearchQuery<string>> MakeWantedMock(HashSet<IToken<string>> must, HashSet<IToken<string>> include, HashSet<IToken<string>> exclude)
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
            AddSomeDataToInvertedIndex();
            var must = new HashSet<IToken<string>>()
                {
                    new WordToken("celtic"),
                    new WordToken("mancity")
                };

            var searchQuery = MakeWantedMock(must, new HashSet<IToken<string>>(), new HashSet<IToken<string>>());
            Assert.Equal(1, InvertedIndex.Find(searchQuery.Object).Count);
        }

        [Fact]
        public void FindTwoMustWord2()
        {
            AddSomeDataToInvertedIndex();
            var must = new HashSet<IToken<string>>()
                {
                    new WordToken("celtic"),
                    new WordToken("barca")
                };

            var searchQuery = MakeWantedMock(must, new HashSet<IToken<string>>(), new HashSet<IToken<string>>());
            Assert.Equal(0, InvertedIndex.Find(searchQuery.Object).Count);
        }

        [Fact]
        public void FindthreeMustWord()
        {
            AddSomeDataToInvertedIndex();
            var must = new HashSet<IToken<string>>()
                {
                    new WordToken("celtic"),
                    new WordToken("mancity"),
                    new WordToken("arsenal")
                };

            var searchQuery = MakeWantedMock(must, new HashSet<IToken<string>>(), new HashSet<IToken<string>>());
            Assert.Equal(0, InvertedIndex.Find(searchQuery.Object).Count);
        }

        [Fact]
        public void FindOneMustOneExcludeWord()
        {
            AddSomeDataToInvertedIndex();

            var must = new HashSet<IToken<string>>()
                {
                    new WordToken("celtic")
                };

            var exclude = new HashSet<IToken<string>>()
                {
                    new WordToken("mancity")
                };

            var searchQuery = MakeWantedMock(must, new HashSet<IToken<string>>(), exclude);
            Assert.Equal(2, InvertedIndex.Find(searchQuery.Object).Count);
        }

        [Fact]
        public void FindSingleNotFoundWord()
        {
            AddSomeDataToInvertedIndex();
            var must = new HashSet<IToken<string>>()
                {
                    new WordToken("chelsea")
                };

            var searchQuery = MakeWantedMock(must, new HashSet<IToken<string>>(), new HashSet<IToken<string>>());
            Assert.Equal(0, InvertedIndex.Find(searchQuery.Object).Count);
        }

        [Fact]
        public void FindOneMustWithOneIncludeNotFound()
        {
            AddSomeDataToInvertedIndex();
            var must = new HashSet<IToken<string>>()
                {
                    new WordToken("celtic")
                };

            var include = new HashSet<IToken<string>>()
                {
                    new WordToken("chelsea")
                };

            var searchQuery = MakeWantedMock(must, include, new HashSet<IToken<string>>());
            Assert.Equal(3, InvertedIndex.Find(searchQuery.Object).Count);
        }

        [Fact]
        public void FindTwoIncludeWithThreeExclude()
        {
            AddSomeDataToInvertedIndex();
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
            Assert.Equal(2, InvertedIndex.Find(searchQuery.Object).Count);
        }

        [Fact]
        public void FindAllKindes()
        {
            AddSomeDataToInvertedIndex();
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
            Assert.Equal(2, InvertedIndex.Find(searchQuery.Object).Count);
        }



    }
}