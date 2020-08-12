using Phase05.Model.Interface;
using Phase05.Model;

namespace Phase05.Model
{
    public class Normalizer
    {
        public IToken Normalize(IToken token)
        {
            string originalText = "";
            var resultToken = new WordToken(originalText.ToLower());
            return resultToken;
        }
    }
}