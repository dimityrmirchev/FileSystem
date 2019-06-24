﻿namespace FileSystem.Models
{
    public class ContentFile : File
    {
        public ContentFile(string path, string content, Directory parent) : base(path, parent)
        {
            Content = content;
        }

        public string Content { get; }
    }
}
