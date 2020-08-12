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
            var normalizer = new Normalizer();
            var expected = new WordToken("  this_is_a_    text and this is a number 4323");
            var token = new WordToken("  tHIS_iS_A_    teXT ANd tHiS IS a nuMBER 4323");
            var actual = normalizer.Normalize(token);
            Assert.Equal(expected, actual);
        }
    }
}