using System.Collections.Generic;

namespace FileSystem.Models
{
    public class Directory : File
    {
        private readonly Directory _parent;
        private readonly List<File> _children;

        public Directory(string path, Directory parent) : base(path)
        {
            _parent = parent;
            _children = new List<File>();
        }

        public void AddChild(File file)
        {
            _children.Add(file);
        }

        public void RemoveChild(File file)
        {
            _children.Remove(file);
        }

        public IEnumerable<Directory> GetSubDirectories()
        {
            foreach (var child in _children)
            {
                if (child is Directory directory)
                {
                    yield return directory;
                }
            }
        }
    }
}
