using DevelopersToday_TestTask.Parsers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static DevelopersToday_TestTask.Program;

namespace DevelopersToday_TestTask.Processors
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    public struct OpenFileName
    {
        public int lStructSize;
        public IntPtr hwndOwner;
        public IntPtr hInstance;
        public string lpstrFilter;
        public string lpstrCustomFilter;
        public int nMaxCustFilter;
        public int nFilterIndex;
        public string lpstrFile;
        public int nMaxFile;
        public string lpstrFileTitle;
        public int nMaxFileTitle;
        public string lpstrInitialDir;
        public string lpstrTitle;
        public int Flags;
        public short nFileOffset;
        public short nFileExtension;
        public string lpstrDefExt;
        public IntPtr lCustData;
        public IntPtr lpfnHook;
        public string lpTemplateName;
        public IntPtr pvReserved;
        public int dwReserved;
        public int flagsEx;
    }

    internal class FileProcessor: IDisposable
    {
        
        [DllImport("comdlg32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern bool GetOpenFileName(ref OpenFileName ofn);
        private string filePath= string.Empty;
        CSVParser parser;
        StreamWriter streamWriter;
        public FileProcessor()
        {
            filePath = ShowDialog();
            if (filePath == string.Empty)
            {
                Console.WriteLine("Unsuccessful attempt to open a file");
                return;
            }
            parser = new CSVParser(filePath);
            string duplicatesPat= filePath.Substring(0,  filePath.LastIndexOf('.')) + "_duplicates.csv";
            streamWriter = new StreamWriter(duplicatesPat);
        }

        public bool TryLoadItems(ref List<DefaulModel> items, ref List<Key> uniqueKeys)
        {
            try
            {  
                List<string> duplicates = new List<string>();
                bool eof = parser.GetNextModels(ref items, ref duplicates, ref uniqueKeys);
                if(duplicates.Count > 0) 
                {
                    updateDuplicatesFile(duplicates);
                }
                return eof;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error was thrown while trying to process a file");

                return true;
            };
        }

        private void updateDuplicatesFile(List<string> duplicates) 
        {
            foreach (string d in duplicates) 
            {
                streamWriter.WriteLine(d);
                streamWriter.Flush();
            }
        }
        
        
        private static string ShowDialog()
        {
           
            var ofn = new OpenFileName();
            ofn.lStructSize = Marshal.SizeOf(ofn);
            ofn.lpstrFilter = "Excel Files (*.xlsx)\0*.csv\0All Files (*.*)\0*.*\0";
            ofn.lpstrFile = new string(new char[256]);
            ofn.nMaxFile = ofn.lpstrFile.Length;
            ofn.lpstrFileTitle = new string(new char[64]);
            ofn.nMaxFileTitle = ofn.lpstrFileTitle.Length;
            ofn.lpstrTitle = "Open File Dialog...";
            if (GetOpenFileName(ref ofn))
            {
                Console.WriteLine("File path: "+ofn.lpstrFile);
                return ofn.lpstrFile;
            }
            return string.Empty;

        }

        public void Dispose()
        {
            if(parser != null) parser.Dispose();
            if(streamWriter!=null) streamWriter.Dispose();
        }
    }
}
