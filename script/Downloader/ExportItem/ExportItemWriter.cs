using System.IO;

namespace Item
{
    public class ExportItemWriter
    {
        private StreamWriter _Writer = null;

        public ExportItemWriter(string path)
        {
            string DirectoryPath = Path.GetDirectoryName(path);
            if (Directory.Exists(DirectoryPath) == false)
            {
                Directory.CreateDirectory(DirectoryPath);
            }

            _Writer = new StreamWriter(path);
        }

        public void Write(string content)
        {
            _Writer.Write(content);
            _Writer.Flush();
        }

        public void Dispose()
        {
            _Writer.Dispose();
        }
    }
}
