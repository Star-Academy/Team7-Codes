using Phase05.Model.Interface;
using Phase05.Model;
using Xunit;

namespace Phase05.Test.Model
{
    public class NormalizerTest
    {
        [Fact]
        public void NormalizeLowerCaseTest()
        {
            var sampleToken = new WordToken("tHIs sHOulD bE NorMalIzEd TExT");
            var normalizer = new Normalizer();
            var actual = normalizer.Normalize(sampleToken);
            var expected = new WordToken("this should be normalized text");
            Assert.Equal(expected, actual);
        }
    }
}