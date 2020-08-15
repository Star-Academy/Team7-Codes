using System.Collections.Generic;
using Phase05.Model.Interface;

namespace Phase05.Model
{
    public class DocumentInfo : ITokenInfo<string>
    {
        public string Content { get; set; }

        public DocumentInfo(string content)
        {
            Content = content;
        }

        public override bool Equals(object obj)
        {
            return obj is DocumentInfo info &&
                   Content == info.Content;
        }

        public override int GetHashCode()
        {
            return 1997410482 + EqualityComparer<string>.Default.GetHashCode(Content);
        }
    }
}