using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientLibrary.Services;

public static class FileService
{

    public static string WriteToFile(string path, string content)
    {
        try
        {
            using (FileStream fs = File.Create(path))
            {
                byte[] info = new UTF8Encoding(true).GetBytes(content);
                fs.Write(info, 0, info.Length);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return "";
        }
        return path;
    }
}