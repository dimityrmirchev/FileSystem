namespace FileSystem.Models
{
    public class FileSystem
    {
        private readonly Directory _root;

        public FileSystem()
        {
            _root = new Directory("/", null);
        }
    }
}
