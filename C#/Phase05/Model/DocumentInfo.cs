using Phase05.Model.Interface;

namespace Phase05.Model
{
    public class DocumentInfo : ITokenInfo<string>
    {
        public string Content{get; set;}

        public DocumentInfo(string content)
        {
            Content = content;
        }
    }
}