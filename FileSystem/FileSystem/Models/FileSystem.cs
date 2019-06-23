using System;
using System.Linq;

namespace FileSystem.Models
{
    public sealed class FileSystem
    {
        private readonly Directory _root;
        private Directory _currentDirectory;

        private FileSystem()
        {
            _root = new Directory("/", null);
            _currentDirectory = _root;
        }

        public static FileSystem Instance { get; } = new FileSystem();

        public string CurrentDirectoryPath => _currentDirectory.Path;

        public void AddDirectory(string path)
        {
            AddDirectory(path, !path.StartsWith('/'));
        }

        public void ChangeDirectory(string path)
        {
            ChangeDirectory(path, !path.StartsWith('/'));
        }

        private void AddDirectory(string path, bool relative)
        {
            var lastIndex = path.LastIndexOf('/');
            if (lastIndex != -1)
            {
                var pathToAddTo = path.Substring(0, lastIndex + 1);
                var newDirectoryName = path.Substring(lastIndex + 1, path.Length - lastIndex + 1);
                if (TryGetDirectory(pathToAddTo, relative, out Directory directoryToAddTo)
                    && !TryGetDirectory(path, relative, out Directory _))
                {
                    directoryToAddTo.AddChild(new Directory(pathToAddTo + $"/{newDirectoryName}", directoryToAddTo));
                }
                else
                {
                    throw new InvalidOperationException($"Cannot add directory {path}");
                }
            }
            else
            {
                if (!TryGetDirectory(path, relative, out Directory _))
                {
                    _currentDirectory.AddChild(new Directory(_currentDirectory.Path + $"/{path}", _currentDirectory));
                }
                else
                {
                    throw new InvalidOperationException($"Cannot add directory {path}");
                }
            }
        }

        private void ChangeDirectory(string path, bool relative)
        {
            if (TryGetDirectory(path, relative, out Directory directory))
            {
                _currentDirectory = directory;
            }
            else
            {
                throw new InvalidOperationException($"Cannot navigate to {path}");
            }
        }

        private bool TryGetDirectory(string path, bool relative, out Directory directory)
        {
            var directoryNames = path.Split('/').Where(s => !string.IsNullOrWhiteSpace(s)).ToArray();
            var currentDirectory = relative ? _currentDirectory : _root;
            foreach (var name in directoryNames)
            {
                if (string.Equals(name, ".."))
                {
                    currentDirectory = currentDirectory?.Parent;
                }
                else
                {
                    currentDirectory = currentDirectory?
                        .GetChildren()
                        .FirstOrDefault(c => c is Directory && string.Equals(name, c.Name)) as Directory;
                }

                if (currentDirectory == null)
                {
                    directory = null;
                    return false;
                }
            }

            directory = currentDirectory;
            return true;
        }
    }
}
