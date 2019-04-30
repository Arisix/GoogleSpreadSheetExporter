using System.Collections.Generic;

namespace Item
{
    class CSVReader
    {
        public static Dictionary<string, object> Read(string data)
        {
            var lines = ParseCsv(data);
            return ParseKeyValue(lines);
        }

        private static string[][] ParseCsv(string dataStream, bool skipComma = false)
        {
            string[][] _data;
            if (string.IsNullOrEmpty(dataStream))
            {
                _data = new string[0][];
                return _data;
            }

            int tabSize = 4;

            //define varible
            int x = 0;
            int y = 0;
            string value;
            char checkChar;
            bool isPrefix;
            bool isString;
            List<string> rowOfData;
            List<List<string>> datas = new List<List<string>>();

            //init values to parse
            value = "";
            isPrefix = false;
            isString = false;
            rowOfData = new List<string>();

            //checking in each character in the data
            //split each string between ',' and '\n'
            //and store the strings to a 2d list in correct order
            //if meet the " " then capture it without processing
            for (int i = 0, len = dataStream.Length; i < len; i++)
            {
                checkChar = dataStream[i];
                if (isPrefix)
                {
                    if (checkChar == '\'' || checkChar == '\"' || checkChar == '\\')
                    {
                        value += checkChar;
                    }
                    else if (checkChar == 'n' || checkChar == 'r')
                    {
                        value += '\n';
                    }
                    else if (checkChar == 't')
                    {
                        for (int tCount = tabSize; tCount > 0; tCount--)
                        {
                            value += ' ';
                        }
                    }
                    else if (checkChar == '0')
                    {
                        value += '\0';
                    }

                    isPrefix = false;
                }
                else if (checkChar == '\n' || (!skipComma && checkChar == ','))
                {
                    if (!isString)
                    {
                        rowOfData.Add(value);

                        if (checkChar == ',')
                        {
                            x++;
                        }
                        else
                        {
                            datas.Add(rowOfData);
                            rowOfData = new List<string>();
                            y++;
                            x = 0;
                        }
                        value = "";
                    }
                    else
                    {
                        value += checkChar;
                    }
                }
                else if (checkChar == '"')
                {
                    if (isString)
                    {
                        value += checkChar;
                        isString = false;
                    }
                    else
                    {
                        isString = true;
                        value += checkChar;
                    }
                }
                else if (checkChar == '\\')
                {
                    isPrefix = true;
                }
                else if (checkChar == '\r')
                {
                    // do nothing
                }
                else
                {
                    value += checkChar;
                }

                if (i >= len - 1)
                {
                    rowOfData.Add(value);
                    datas.Add(rowOfData);
                }

            }

            // Finally, remove last string if it is an empty string
            if (datas[datas.Count - 1].Count == 1 && string.IsNullOrEmpty(datas[datas.Count - 1][0]))
            {
                // REMOVE the last data
                datas.RemoveAt(datas.Count - 1);
            }


            _data = new string[datas.Count][];

            //modify the value which is start with " and end with "
            //replace the "" to "
            //store the value to the _data array
            for (int i = 0, iLen = datas.Count; i < iLen; i++)
            {
                _data[i] = new string[datas[i].Count];
                for (int j = 0, jLen = datas[i].Count; j < jLen; j++)
                {
                    value = datas[i][j];

                    if (value.StartsWith("\"") && value.EndsWith("\""))
                    {
                        value = value.Substring(1, value.Length - 2);
                    }
                    value = value.Replace("\"\"", "\"");
                    _data[i][j] = value;
                }
            }
            return _data;
        }

        private static Dictionary<string, object> ParseKeyValue(string[][] lines)
        {
            var list = new Dictionary<string, object>();
            if (lines.Length <= 1)
                return list;

            string[] header = null;
            for (var i = 0; i < lines.Length; i++)
            {

                var values = lines[i];
                if (values.Length == 0 || values[0] == "")
                    continue;
                if (header == null)
                {
                    header = values;
                    continue;
                }

                int n;
                float f;
                long l;

                var entry = new Dictionary<string, object>();
                for (var j = 0; j < header.Length && j < values.Length; j++)
                {
                    string value = values[j];
                    object finalvalue = value;


                    if (value.Contains(",") != true)
                    {
                        if (int.TryParse(value, out n))
                            finalvalue = n;
                        else if (long.TryParse(value, out l))
                            finalvalue = l;
                        else if (float.TryParse(value, out f))
                            finalvalue = f;
                    }
                    entry[header[j]] = finalvalue;
                }

                if (entry[header[0]].ToString().Length > 0)
                {
                    list.Add(entry[header[0]].ToString(), entry);
                }
            }
            return list;
        }
    }
}