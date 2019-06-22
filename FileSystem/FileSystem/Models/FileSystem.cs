namespace FileSystem.Models
{
    public class FileSystem
    {
        private readonly Directory _root;
        private readonly Directory _currentDirectory;

        public FileSystem()
        {
            _root = new Directory("/", null);
            _currentDirectory = _root;
        }
    }
}
