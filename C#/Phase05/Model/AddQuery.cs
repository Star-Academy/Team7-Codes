using System.Collections.Generic;
using Phase05.Model.Interface;

namespace Phase05.Model
{
    public class AddQuery<T, E> : IAddQuery<T, E>
    {
        public IToken<T> Token { get; set; }
        public ITokenInfo<E> TokenInfo { get; set; }

        public AddQuery(IToken<T> token, ITokenInfo<E> tokenInfo)
        {
            Token = token;
            TokenInfo = tokenInfo;
        }

        public override bool Equals(object obj)
        {
            return obj is AddQuery<T, E> query &&
                   EqualityComparer<IToken<T>>.Default.Equals(Token, query.Token) &&
                   EqualityComparer<ITokenInfo<E>>.Default.Equals(TokenInfo, query.TokenInfo);
        }

        public override int GetHashCode()
        {
            int hashCode = 1994911116;
            hashCode = hashCode * -1521134295 + EqualityComparer<IToken<T>>.Default.GetHashCode(Token);
            hashCode = hashCode * -1521134295 + EqualityComparer<ITokenInfo<E>>.Default.GetHashCode(TokenInfo);
            return hashCode;
        }
    }
}