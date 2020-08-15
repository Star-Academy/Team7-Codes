using Phase05.Model.Interface;

namespace Phase05.Model
{
    public class Normalizer
    {
        public IToken<string> Normalize(IToken<string> token)
        {
            var originalText = token.Content;
            var normalizedText = originalText.ToLower();
            return new WordToken(normalizedText);
        }
    }
}