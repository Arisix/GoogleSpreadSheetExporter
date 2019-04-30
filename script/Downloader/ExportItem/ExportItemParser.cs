using Options;
using Patterns;
using System.Collections.Generic;
using System.IO;

namespace Item
{
    public class ExportItemParser : Singleton<ExportItemParser>
    {
        private List<ExportItem> _ExportItemList;

        public override void Initialize()
        {
            _ExportItemList = new List<ExportItem>();
            var dic = CSVReader.Read(File.ReadAllText(Config.ConfigPath));

            foreach(var kv in dic)
            {
                Dictionary<string, object> row = (Dictionary<string, object>)kv.Value;

                string name = (string)row["outputname"];
                string key = (string)row["key"];
                string gid = ((int)row["gid"]).ToString();

                ExportItem newItem = new ExportItem();
                newItem.Name = name;
                newItem.Key = key;
                newItem.Gid = gid;

                _ExportItemList.Add(newItem);
            }

        }

        public List<ExportItem> GetAllItems()
        {
            return _ExportItemList;
        }
    }
}
