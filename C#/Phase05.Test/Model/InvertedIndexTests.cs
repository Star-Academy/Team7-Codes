using Xunit;
using Phase05.Model;
using Phase05.Model.Interface;
using Moq;
using System.Collections.Generic;
using System;

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
            throw new NotImplementedException();
        }

        [Fact]
        public void AddNewToken()
        {
            var addQuery = new AddQuery<string, string>(new WordToken("barca"), new DocumentInfo("spain"));
            InvertedIndex.Add(addQuery);
            Assert.Equal(1, Dictionary.GetValueOrDefault(new WordToken("barca")).Count);
        }

        [Fact]
        public void AddTwoDocumentForTwoWord()
        {
            var addQuery1 = new AddQuery<string, string>(new WordToken("barca"), new DocumentInfo("spain"));
            var addQuery2 = new AddQuery<string, string>(new WordToken("barca"), new DocumentInfo("catolania"));
            InvertedIndex.Add(addQuery1);
            InvertedIndex.Add(addQuery2);
            Assert.Equal(2, Dictionary.GetValueOrDefault(new WordToken("barca")).Count);
        }

        [Fact]
        public void AddTwoWordWithSameDocument()
        {
            var addQuery1 = new AddQuery<string, string>(new WordToken("barca"), new DocumentInfo("spain"));
            var addQuery2 = new AddQuery<string, string>(new WordToken("atleti"), new DocumentInfo("spain"));
            InvertedIndex.Add(addQuery1);
            InvertedIndex.Add(addQuery2);
            Assert.Equal(1, Dictionary.GetValueOrDefault(new WordToken("barca")).Count);
            Assert.Equal(1, Dictionary.GetValueOrDefault(new WordToken("atleti")).Count);
            Assert.Equal(0, Dictionary.GetValueOrDefault(new WordToken("bayern")).Count);
        }


        [Fact]
        public void TestName()
        {
        //Given
        
        //When
        
        //Then
        }


    }
}