using System;
using System.Collections.Generic;
using System.Linq;

namespace FileSystem.Models
{
    public sealed class FileSystem
    {
        private readonly Directory _root;
        private Directory _currentDirectory;

        public FileSystem()
        {
            _root = new Directory("/", null);
            _currentDirectory = _root;
        }

        public string CurrentDirectoryPath => _currentDirectory.Path;

        public void AddDirectory(string path)
        {
            AddDirectory(path, !path.StartsWith('/'));
        }

        public void ChangeDirectory(string path)
        {
            ChangeDirectory(path, !path.StartsWith('/'));
        }

        public IEnumerable<File> ListDirectory(string path)
        {
            if (TryGetDirectory(path, !path.StartsWith('/'), out Directory directory))
            {
                return directory.GetChildren();
            }

            throw new InvalidOperationException($"Couldn't list directory {path}");
        }

        public void RemoveContentFiles(string[] paths)
        {
            var filesToRemove = new List<ContentFile>();
            foreach (var path in paths)
            {
                if (TryGetContentFile(path, !path.StartsWith('/'), out ContentFile contentFile))
                {
                    filesToRemove.Add(contentFile);
                }
                else
                {
                    throw new InvalidOperationException($"Couldn't find file {path}");
                }
            }

            foreach (var contentFile in filesToRemove)
            {
                RemoveFile(contentFile);
            }
        }

        public bool TryGetDirectory(string path, out Directory directory)
        {
            return TryGetDirectory(path, !path.StartsWith('/'), out directory);
        }

        private void AddDirectory(string path, bool isRelative)
        {
            var lastIndex = path.LastIndexOf('/');
            if (lastIndex != -1)
            {
                var pathToAddTo = path.Substring(0, lastIndex + 1);
                var newDirectoryName = path.Substring(lastIndex + 1, path.Length - lastIndex - 1);
                if (TryGetDirectory(pathToAddTo, isRelative, out Directory directoryToAddTo)
                    && !TryGetDirectory(path, isRelative, out Directory _))
                {
                    var newDirectoryPath = directoryToAddTo.Path.EndsWith("/")
                        ? directoryToAddTo.Path + $"{newDirectoryName}"
                        : directoryToAddTo.Path + $"/{newDirectoryName}";

                    directoryToAddTo.AddChild(new Directory(newDirectoryPath, directoryToAddTo));
                }
                else
                {
                    throw new InvalidOperationException($"Cannot create directory {path}");
                }
            }
            else
            {
                if (!TryGetDirectory(path, isRelative, out Directory _))
                {
                    var newDirectoryPath = _currentDirectory.Path.EndsWith("/")
                        ? _currentDirectory.Path + $"{path}"
                        : _currentDirectory.Path + $"/{path}";
                    _currentDirectory.AddChild(new Directory(newDirectoryPath, _currentDirectory));
                }
                else
                {
                    throw new InvalidOperationException($"Cannot create directory {path}");
                }
            }
        }

        private void ChangeDirectory(string path, bool isRelative)
        {
            if (TryGetDirectory(path, isRelative, out Directory directory))
            {
                _currentDirectory = directory;
            }
            else
            {
                throw new InvalidOperationException($"Cannot navigate to {path}");
            }
        }

        private void RemoveFile(File file)
        {
            if (file is ContentFile contentFile)
            {
                contentFile.Parent.RemoveChild(contentFile);
            }
            else if (file is Directory directory)
            {
                if (!directory.GetChildren().Any())
                {
                    directory.Parent.RemoveChild(directory);
                }
                else
                {
                    throw new InvalidOperationException($"{file.Path} is not an empty directory and cannot be deleted");
                }
            }
        }

        private bool TryGetDirectory(string path, bool isRelative, out Directory directory)
        {
            var directoryNames = path.Split('/').Where(s => !string.IsNullOrWhiteSpace(s)).ToArray();
            var currentDirectory = isRelative ? _currentDirectory : _root;
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

        private bool TryGetContentFile(string path, bool isRelative, out ContentFile contentFile)
        {
            var directoryNames = path.Split('/').Where(s => !string.IsNullOrWhiteSpace(s)).ToArray();
            var currentDirectory = isRelative ? _currentDirectory : _root;
            for (var i = 0; i < directoryNames.Length - 1; i++)
            {
                if (string.Equals(directoryNames[i], ".."))
                {
                    currentDirectory = currentDirectory?.Parent;
                }
                else
                {
                    currentDirectory = currentDirectory?
                        .GetChildren()
                        .FirstOrDefault(c => c is Directory && string.Equals(directoryNames[i], c.Name)) as Directory;
                }

                if (currentDirectory == null)
                {
                    contentFile = null;
                    return false;
                }
            }

            var fileToReturn = currentDirectory?
                .GetChildren()
                .FirstOrDefault(c =>
                    c is ContentFile &&
                    string.Equals(directoryNames[directoryNames.Length - 1], c.Name)) as ContentFile;

            if (fileToReturn == null)
            {
                contentFile = null;
                return false;
            }

            contentFile = fileToReturn;
            return true;
        }
    }
}
