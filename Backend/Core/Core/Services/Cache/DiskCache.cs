using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Caching;


[assembly: InternalsVisibleTo("Users.Test")]
namespace Core.Services.Cache
{
    internal class DiskCache : ICache
    {

        
        public T Get<T>(string key)
        {
            T response;

            try
            {
                string filePath = GetPath(key);

                if (File.Exists(filePath))
                {
                    var fileContent = GetContent(filePath);

                    response = JsonConvert.DeserializeObject<T>(fileContent);
                }
                else
                {
                    response = default(T);
                }
            }
            catch
            {
                //Crossing fingers... --> Should track
                response = default(T);
            }

            return response;
        }

        public List<string> ListFileNames()
        {
            List<string> response = new List<string>();

            try
            {
                string dir = GetBaseDirectory();
                
                DirectoryInfo d = new DirectoryInfo(dir);
                string[] Files = Directory.GetFiles(dir, "*.json");
                
                foreach (string file in Files)
                {
                    string fileName = Path.GetFileNameWithoutExtension(file);
                    response.Add(fileName);
                }
            }
            catch
            {
                //Crossing fingers... --> Should track
            }

            return response;
        }


        
        public void Add(string key, object value)
        {
            string filePath = GetPath(key);

            SaveToDisk(key, value, filePath);
        }


        public void Remove(string key)
        {
            string filePath = GetPath(key);

            try
            {
                File.Delete(filePath);
            }
            catch
            {
                //Crossing fingers... --> Should track
            }
        }


        private string GetPath(string key)
        {
            StringBuilder filePath = new StringBuilder();

            filePath.Append(GetBaseDirectory());
            filePath.Append(key);
            filePath.Append(".json");
            
            return filePath.ToString();
        }


        private string GetBaseDirectory()
        {
            StringBuilder filePath = new StringBuilder();
            
            filePath.Append(@"C:\Users\Public\GuillemSchibstedLover\");

            return filePath.ToString();
        }


        private string GetContent(string filePath)
        {
            string response;

            try
            {
                string fileContent = string.Empty;

                if (File.Exists(filePath))
                {
                    using (FileStream stream = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                    using (StreamReader reader = new StreamReader(stream))
                    while (!reader.EndOfStream)
                    {
                        fileContent = reader.ReadToEnd();
                    }
                }

                response = fileContent;
            }
            catch
            {
                //Crossing fingers... --> Should track
                response = string.Empty;
            }

            return response;
        }


        private void SaveToDisk(string key, object value, string filePath)
        {
            FileInfo file = new FileInfo(filePath);

            if (!file.Directory.Exists)
            {
                file.Directory.Create();
            }

            try
            {
                string json = JsonConvert.SerializeObject(value);

                using (StreamWriter outfile = new StreamWriter(new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.ReadWrite), Encoding.UTF8))
                {
                    outfile.Write(json);
                }
            }
            catch
            {
                //Crossing fingers... --> Should track
            }
        }
    }
}
