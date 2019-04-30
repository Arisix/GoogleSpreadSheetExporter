using Options;

namespace Item
{
    public class ExportItem
    {
        public string Name;
        public string Key;
        public string Gid;
        public string FilePath { get { return string.Format("{0}{1}.csv", Config.OutputPath, Name); } }
    }
}
