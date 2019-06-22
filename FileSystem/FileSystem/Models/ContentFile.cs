namespace FileSystem.Models
{
    public class ContentFile : File
    {
        public ContentFile(string path, string content) : base(path)
        {
            Content = content;
        }

        public string Content { get; }
    }
}
