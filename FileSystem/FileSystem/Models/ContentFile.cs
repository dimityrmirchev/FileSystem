namespace FileSystem.Models
{
    public sealed class ContentFile : File
    {
        public ContentFile(string path, string content, Directory parent) : base(path, parent)
        {
            Content = content ?? string.Empty;
        }

        public string Content { get; }
    }
}
