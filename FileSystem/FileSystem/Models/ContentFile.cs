namespace FileSystem.Models
{
    /// <summary>
    /// Represents a content file in a file system.
    /// </summary>
    public sealed class ContentFile : File
    {
        /// <summary>
        /// Creates a content file.
        /// </summary>
        /// <param name="path">The path to the content file.</param>
        /// <param name="content">The content of the file.</param>
        /// <param name="parent">The content file's parent.</param>
        public ContentFile(string path, string content, Directory parent) : base(path, parent)
        {
            Content = content ?? string.Empty;
        }

        /// <summary>
        /// The content of the file.
        /// </summary>
        public string Content { get; }
    }
}
