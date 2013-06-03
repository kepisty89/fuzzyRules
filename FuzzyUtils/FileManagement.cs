using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuzzyUtils
{
    public class FileManagement
    {        
        public static void WriteToFile(string filename, string data)
        {
            string fileDirectory = Path.GetDirectoryName(filename);

            if(!Directory.Exists(fileDirectory))
            {
                Directory.CreateDirectory(fileDirectory);
            }

            if (!File.Exists(filename))
            {
                File.CreateText(filename).Write(data);
            }
            else
            {
                File.WriteAllText(filename, data);
            }
        }
    }
}
