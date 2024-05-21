using Microsoft.VisualBasic.FileIO;

using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevelopersToday_TestTask.Parsers
{
    public static class DefaultRowParser
    {
        static public DefaulModel ParseRow(string[] cells, Dictionary<string, int> fieldIndexes)
        {

            DefaulModel model = new DefaulModel();
            var modelfields = typeof(DefaulModel).GetFields();
            foreach (var field in modelfields)
            {
                if (fieldIndexes.TryGetValue(field.Name.Trim('_'), out int index))
                {
                    if (index > cells.Length)
                    {
                        continue;
                    }

                    if (field.FieldType == typeof(DateTime))
                    {
                        if (DateTime.TryParse(cells[index], CultureInfo.InvariantCulture, out DateTime result))
                            field.SetValue(model, result);
                        else field.SetValue(model, DateTime.MinValue);
                    }
                    else if (field.FieldType == typeof(byte))
                    {
                        if (byte.TryParse(cells[index], out byte result))
                            field.SetValue(model, result);
                        else field.SetValue(model, (byte)0);
                    }
                    else if (field.FieldType == typeof(int))
                    {
                        if (int.TryParse(cells[index], out int result))
                            field.SetValue(model, result);
                        else field.SetValue(model, 0);
                    }
                    else if (field.FieldType == typeof(float))
                    {
                        if (float.TryParse(cells[index], CultureInfo.InvariantCulture, out float result))
                            field.SetValue(model, result);
                        else field.SetValue(model, 0);
                    }
                    else if (field.FieldType == typeof(string))
                    {
                        switch (cells[index].ToLower().Trim())
                        {
                            case "n":
                                field.SetValue(model, "No");
                                break;
                            case "y":
                                field.SetValue(model, "Yes");
                                break;
                            default:
                                field.SetValue(model, cells[index].Trim());
                                break;
                        };
                    }


                }
            }
            return model;
        }


    }


    public class CSVParser : IDisposable
    {
        TextFieldParser parser;
        Dictionary<string,int> fieldIndexes = new Dictionary<string,int>();
        public CSVParser(string filePath)
        {
            parser = new TextFieldParser(filePath);

            parser.TextFieldType = FieldType.Delimited;
            parser.SetDelimiters(",");
            int index = 0;
            foreach (var item in parser.ReadFields())
            {
                fieldIndexes.Add(item, index++);
            }

        }

        public bool GetNextModels(ref List<DefaulModel> items, ref List<string> duplicates, ref List<Key> uniqueKeys)
        {
            if(items.Count!=0) items.Clear();
            int index = 0;
            while (!parser.EndOfData&&index<10000)
            {
                index++;
                
                string[] cells = parser.ReadFields();
                var item = DefaultRowParser.ParseRow(cells, fieldIndexes);

                var key = new Key(item.tpep_pickup_datetime, item.tpep_dropoff_datetime, item.passenger_count);
                bool uniqueItemKey=true;
                foreach (var k in uniqueKeys)
                {
                    if(k.Equals(key))
                    {
                        uniqueItemKey = false;
                        break;

                    }
                }
                if(uniqueItemKey)
                {
                    items.Add(item);
                    uniqueKeys.Add(key);
                }
                else
                {
                    duplicates.Add(String.Join(',', cells));
                }
            }

            return parser.EndOfData;
        }
        public void Dispose()
        {
            if (parser!= null)
                parser.Dispose();
        }
    }
}
