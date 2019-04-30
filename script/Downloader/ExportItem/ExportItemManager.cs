using Patterns;
using System.Collections.Generic;

namespace Item
{
    public class ExportItemManager : Singleton<ExportItemManager>
    {
        private ExportItemParser _Parser = null;
        private Dictionary<string, ExportItemWriter> _WriterPool = new Dictionary<string, ExportItemWriter>();

        public override void Initialize()
        {
            _Parser = ExportItemParser.Instance;
        }

        public List<ExportItem> GetAllItems()
        {
            return _Parser.GetAllItems();
        }

        public void Write(string path, string content)
        {
            if(_WriterPool.TryGetValue(path, out var writer) == false)
            {
                writer = new ExportItemWriter(path);
                _WriterPool[path] = writer;
            }

            writer.Write(content);
        }

        public void Dispose()
        {
            foreach(var kv in _WriterPool)
            {
                kv.Value.Dispose();
            }
        }
    }
}
