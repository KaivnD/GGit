using GGit.Forms;
using Grasshopper;
using Grasshopper.GUI;
using Grasshopper.GUI.Canvas;
using Grasshopper.Kernel;
using LibGit2Sharp;
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using static Grasshopper.Instances;
using static GGit.Utils.Tools;

namespace GGit
{
    public class GGitLoader : GH_AssemblyPriority
    {
        public GGitLoader()
        {
        }

        private bool _btnAdded;

        private bool _menuAdded;

        public override GH_LoadingInstruction PriorityLoad()
        {
            CanvasCreated += AppendGGitBtn;
            CanvasCreated += AppendGGitMenu;
            return GH_LoadingInstruction.Proceed;
        }

        private void AppendGGitBtn(GH_Canvas canvas)
        {
            if (_btnAdded) return;
            foreach(Control control in canvas.Parent.Controls)
            {
                var toolstrip = control as ToolStrip;
                if (toolstrip != null)
                {
                    toolstrip.Items.Add(null, Properties.Resources.git, OnClick);
                    return;
                }
            }
            _btnAdded = true;
        }

        private void AppendGGitMenu(GH_Canvas canvas)
        {
            if (_menuAdded) return;
            foreach (Control ctrl in DocumentEditor.Controls)
            {
                var menuStrip = ctrl as MenuStrip;
                if (menuStrip != null)
                {
                    ToolStripMenuItem customItem = new ToolStripMenuItem("GGit");
                    ToolStripMenuItem init = new ToolStripMenuItem("Init", Properties.Resources.git, OnInitRepository);
                    ToolStripMenuItem commit = new ToolStripMenuItem("Commit", Properties.Resources.git, OnDocumentCommit);
                    ToolStripMenuItem push = new ToolStripMenuItem("Push", Properties.Resources.git, OnDocumentPush);
                    ToolStripMenuItem clone = new ToolStripMenuItem("Clone", Properties.Resources.git, OnDocumentCommit);

                    commit.ShortcutKeys = Keys.Control | Keys.K;
                    push.ShortcutKeys = Keys.Control | Keys.Shift | Keys.K;
                    customItem.DropDown.Items.Add(init);
                    customItem.DropDown.Items.Add(commit);
                    customItem.DropDown.Items.Add(push);

                    customItem.DropDown.Items.Add(new ToolStripSeparator());
                    ToolStripMenuItem conf = new ToolStripMenuItem("Setting", Properties.Resources.git, (sender, e) => {
                        SignatureConfig conform = new SignatureConfig();
                        conform.Show();
                    });
                    customItem.DropDown.Items.Add(conf);
                    ToolStripItem toolStripItem4 = customItem.DropDown.Items.Add("NPKG", null, delegate
                    {
                        Process.Start("https://ncf.cz-studio.cn");
                    });
                    menuStrip.Items.Add(customItem);
                    return;
                }
            }
            _menuAdded = true;
        }

        private void OnDocumentPush(object sender, EventArgs e)
        {
            if (ActiveCanvas.Document == null) return;
            try
            {
                string docPath = ActiveCanvas.Document.FilePath;
                using (var repo = new Repository(getWorkDir(docPath)))
                {
                    if (repo.Network.Remotes["origin"] == null)
                    {
                        // TO DO Set remote url form
                        repo.Network.Remotes.Add("origin", "https://github.com/KaivnD/Gt.git");
                        return;
                    }

                    Remote remote = repo.Network.Remotes["origin"];

                    var options = new PushOptions();
                    options.CredentialsProvider = (_url, _user, _cred) =>
                        new UsernamePasswordCredentials
                        {
                            // TO DO Set AccessToken From
                            Username = "the-access-token", // YOUR TOKEN HERE
                            Password = string.Empty
                        };

                    // TO DO If get AcessToken Faild or accesstoken didn't work pop a form input
                    // TO DO remove to tools
                    GH_SettingsServer sserver = new GH_SettingsServer("ggit");
                    string username = sserver.GetValue("author", "nobody");
                    string email = sserver.GetValue("email", "nobody@nobody.com");
                    Signature author = new Signature(username, email, DateTime.Now);
                    repo.Network.Push(remote, @"refs/heads/master", options);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void OnInitRepository(object sender, EventArgs e)
        {
            FolderBrowserDialog folder = new FolderBrowserDialog();
            folder.Description = "Select a folder to be Initialize";
            DialogResult res = folder.ShowDialog();
            if (res == DialogResult.OK || res == DialogResult.Yes)
            {
                string repository = Repository.Init(@folder.SelectedPath);
                if (!string.Equals(repository, string.Empty))
                {
                    MessageBox.Show(string.Format("初始化{0}成功", repository));
                }
            }
        }

        private void OnDocumentCommit(object sender, EventArgs e)
        {
            GH_SettingsServer sserver = new GH_SettingsServer("ggit");
            string author = sserver.GetValue("author", "");
            string email = sserver.GetValue("email", "");
            if (author == "" || email == "")
            {
                MessageBox.Show(string.Format("Please tell git who you are!"));
                SignatureConfig conform = new SignatureConfig();
                conform.Show();
                return;
            }
            if (ActiveCanvas.Document == null) return;
            bool isModified = ActiveCanvas.Document.IsModified;
            if (isModified)
            {
                MessageBox.Show(string.Format("Please save this document first!"));
            } else
            {
                string docPath = ActiveCanvas.Document.FilePath;
                CommitForm commitForm = new CommitForm(docPath);

                GH_WindowsFormUtil.CenterFormOnCursor(commitForm, true);
                commitForm.Show();
            }
        }

        private void OnClick(object sender, EventArgs e)
        {
            string filepath = @"C:\Users\KaivnD\Desktop\ss.gh";
            GGitViewer vv = new GGitViewer(filepath);
            vv.FadeIn();
        }
    }
}
