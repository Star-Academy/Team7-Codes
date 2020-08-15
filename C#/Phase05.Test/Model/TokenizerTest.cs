using Xunit;
using Moq;
using Phase05.Model.Interface;
using Phase05.Model;

namespace Phase05.Test.Model
{
    public abstract class TokenizerTest
    {
        [Fact]
        public void TokenizeDirectoryTest()
        {
            var mockIIndex = new Mock<IIndex<string, string>>();
            Tokenizer tokenizer = new Tokenizer(mockIIndex.Object);
            // IAddQuery<string, string> expected = new AddQuery(new WordToken("a"), new DocumentInfo<>);
            // tokenizer.TokenizeDirectory("");
            // mockIIndex.Setup(iIndex => iIndex.Add(It
            //     .Is<IAddQuery<string, string>>(addQuery.equals())));
        }
    }
}