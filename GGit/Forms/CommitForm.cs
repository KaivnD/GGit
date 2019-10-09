using BrightIdeasSoftware;
using GGit.Utils;
using Grasshopper.Kernel;
using LibGit2Sharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GGit.Forms
{
    public partial class CommitForm : Form
    {
        private string _workdir { set; get; }
        private string _filePath { set; get; }
        private List<GStatus> statuses = new List<GStatus>();

        public CommitForm(string filePath)
        {
            _filePath = filePath;
            InitializeComponent();
            StatusInit();
        }

        private void StatusInit()
        {
            try
            {
                _workdir = getWorkDir(_filePath);
                RetrieveStatus();
                statusList.SetObjects(statuses);
                statusList.ButtonClick += OnStatusBtnClick;
            }
            catch (Exception err)
            {
                MessageBox.Show(string.Format(err.Message));
            }
        }

        private void RetrieveStatus()
        {
            statuses.Clear();
            using (var repo = new Repository(_workdir))
            {
                foreach (var item in repo.RetrieveStatus(new StatusOptions()))
                {
                    statuses.Add(new GStatus(_workdir, item.FilePath, Convert.ToInt32(item.State), Staged(item.FilePath)));
                }
            }
        }

        /// <summary>
        /// TO DO make it more resonable
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private bool Staged(string path)
        {
            using (var repo = new Repository(_workdir))
            {
                foreach (IndexEntry ie in repo.Index.ToArray())
                {
                    if (Equals(path, ie.Path))
                    {
                        try
                        {
                            GitObject hash = repo.Head.Tip[ie.Path].Target;
                            return hash.Id != ie.Id;
                        }
                        catch
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        private void OnStatusBtnClick (object sender, CellClickEventArgs e)
        {
            switch (e.SubItem.Text)
            {
                case "+":
                    {
                        statuses[e.RowIndex].StageFile();
                        statusList.SetObjects(statuses);
                        break;
                    }
                case "-":
                    {
                        statuses[e.RowIndex].UnStageFile();
                        statusList.SetObjects(statuses);
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }

        /// <summary>
        /// Find a dir contains a dir named .git
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private string getWorkDir(string path)
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

        private void commitBtn_Click(object sender, EventArgs e)
        {
            if (commitMsgInput.Text != "")
            {
                try
                {
                    using (var repo = new Repository(_workdir))
                    {
                        GH_SettingsServer sserver = new GH_SettingsServer("ggit");
                        string username = sserver.GetValue("author", "nobody");
                        string email = sserver.GetValue("email", "nobody@nobody.com");
                        Signature author = new Signature(username, email, DateTime.Now);
                        Signature committer = author;
                        repo.Commit(commitMsgInput.Text, author, committer);
                    }
                    RetrieveStatus();
                    statusList.SetObjects(statuses);
                    commitMsgInput.Text = "";
                    MessageBox.Show("Commit success !");
                }
                catch (Exception err)
                {
                    MessageBox.Show(string.Format("Commit Failed: {0}", err.Message));
                }
            } else MessageBox.Show("Please input commit message !");
        }

        private void stageAllBtn_Click(object sender, EventArgs e)
        {
            statuses.ForEach(x => x.StageFile());
            RetrieveStatus();
            statusList.SetObjects(statuses);
        }

        private void unstageAllBtn_Click(object sender, EventArgs e)
        {
            statuses.ForEach(x => x.UnStageFile());
            RetrieveStatus();
            statusList.SetObjects(statuses);
        }
    }
}
