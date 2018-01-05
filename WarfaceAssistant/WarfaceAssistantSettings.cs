using System;
using System.Text;
using System.IO;

namespace WarfaceAssistant
{
    static class WarfaceAssistantSettings
    {
        public static string path_log { get; set; }
        public static bool FindLogFile()
        {
            string file_row = null;
            byte[] temp = new byte[1024];
            int start = -1,
                end = -1;
            StringBuilder sb = new StringBuilder();
            sb.Append(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile));
            sb.Append("\\Saved Games\\My Games\\Warface\\Profiles\\default\\GFXSettings.Warface.xml");
            file_row = sb.ToString();
            if (File.Exists(file_row) == false) return false;
            FileStream fs = new FileStream(file_row, FileMode.Open, FileAccess.Read);
            fs.Read(temp, 0, temp.Length);
            file_row = System.Text.Encoding.Unicode.GetString(temp);

            if (file_row.Contains("INSTALLPATH"))
                for (int i = 0; i < file_row.Length ; i++)
                {
                    if (file_row.Substring(i, 11).StartsWith("INSTALLPATH"))
                    {
                        i += 19;
                        start = i++;
                        for (int g = i; g < i + 256; g++)
                            if (file_row[g] == '"')
                            {
                                end = g;
                                break;
                            }
                        break;
                    }
                }
            else return false;
            file_row = file_row.Substring(start, end - start);
            file_row = file_row.Remove(file_row.Length - 13);
            file_row = file_row.Insert(file_row.Length, "Game.log");
            path_log = file_row;
            return true;
        }
    }
}
