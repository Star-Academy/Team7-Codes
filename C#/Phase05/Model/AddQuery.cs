using Phase05.Model.Interface;

namespace Phase05.Model
{
    public class AddQuery<T, E> : IAddQuery<T,E>
    {
        public IToken<T> Token { get; set; }
        public ITokenInfo<E> TokenInfo { get; set; }

        public AddQuery(IToken<T> token, ITokenInfo<E> tokenInfo)
        {
            Token = token;
            TokenInfo = tokenInfo;
        }
    }
}