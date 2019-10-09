using Grasshopper.Kernel;
using LibGit2Sharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GGit.Utils
{
    public static class Tools
    {
        public static Signature getSignature()
        {
            GH_SettingsServer sserver = new GH_SettingsServer("ggit");
            string username = sserver.GetValue("author", "");
            string email = sserver.GetValue("email", "");
            if (username == "" || email == "")
            {
                return null;
            }
            Signature author = new Signature(username, email, DateTime.Now);
            return author;
        }
        /// <summary>
        /// Find a dir contains a dir named .git
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string getWorkDir(string path)
        {
            if (File.Exists(path) || Directory.Exists(path))
            {
                if (!string.Equals(Path.GetDirectoryName(path), null))
                {
                    string fileDir = Path.GetDirectoryName(path);
                    DirectoryInfo dir = new DirectoryInfo(fileDir);
                    DirectoryInfo[] dirInfos = dir.GetDirectories();
                    string workDir = string.Empty;
                    foreach (DirectoryInfo info in dirInfos)
                    {
                        if (info.Name == ".git")
                        {
                            workDir = fileDir;
                            break;
                        }
                    }
                    if (string.Equals(workDir, string.Empty)) return getWorkDir(fileDir);
                    else return workDir;
                }
                else throw new Exception(string.Format("This path is not in git repository", path));
            }
            else throw new Exception(string.Format("Path '{0}' doesn't exists !", path));
        }
    }
}
