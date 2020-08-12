namespace Phase05.Model.Interface
{
    public interface IAddQuery<T, E>
    {
        IToken<T> Token { get; set; }

        ITokenInfo<E> TokenInfo { get; set; }

    }
}