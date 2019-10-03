using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Euynac.Utility
{
    class Time
    {
        /// <summary>
        /// 获取时间戳
        /// </summary>
        /// <returns></returns>
        public static long Get13TimeStamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 8, 0, 0, 0);
            return Convert.ToInt64(ts.TotalMilliseconds);
        }
        public static int Get10TimeStamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 8, 0, 0);
            return Convert.ToInt32(ts.TotalSeconds);

        }
    }

    #region 可聚合一个镶嵌的资源，然后运行时自动生成在程序
    public static class PersonalResource
    {
        public static string GetTempURL(byte[] resource, string fileName)
        {
            byte[] resoureBytes = resource;
            string fileUrl = Application.StartupPath + "\\" + fileName;
            if (!IsExist(fileUrl))
            {
                System.IO.FileStream resourceFileStream = new System.IO.FileStream(fileUrl, System.IO.FileMode.Create);
                resourceFileStream.Write(resoureBytes, 0, resoureBytes.Length);
                resourceFileStream.Close();
            }
            return fileUrl;

        }
        private static bool IsExist(string fileUrl)
        {
            return System.IO.File.Exists(fileUrl);
        }
    }
    #endregion

    #region 文件操作类
    class EuyFile
    {
        public static string getNameByPath(string path, bool withExtension = true)
        {
            if(withExtension)
            {
                return Path.GetFileName(path);
            }
            else
            {
                return Path.GetFileNameWithoutExtension(path);
            } 
        }
        public static bool IsPath(string path)
        {
            Regex regex = new Regex(@"^([a-z]|[A-Z]):(\\([a-z]|[A-Z]| |.|[0-9])+)*$");
            if (regex.IsMatch(path))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static string GetDirectoryNameByPath(string path)//当前目录名
        {
            return Directory.GetParent(path).Name;
        }
        public static string GetDirectoryByPath(string path)
        {
            return Path.GetDirectoryName(path);
        }
        public static void CreateDirectory(string path)
        {
            Directory.CreateDirectory(path);
        }

        public static void WriteFile(string path, StringBuilder content)
        {
            FileStream fileStream = new FileStream(path, FileMode.Create);
            StreamWriter writer = new StreamWriter(fileStream);
            writer.Write(content);
            writer.Flush();
            writer.Close();
            fileStream.Close();
        }
        public static StringBuilder ReadFile(string path) //读取一般大小文件（未测试多大）
        {
            try
            {
                FileStream fileStream = new FileStream(path, FileMode.Open);
                StreamReader reader = new StreamReader(fileStream);
                StringBuilder result = new StringBuilder(reader.ReadToEnd());
                reader.Close();
                return result;
            }
            catch (FileNotFoundException)
            {
                return null;
            }
        }
    }
    #endregion
}
