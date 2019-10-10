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
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using LibGit2Sharp.Handlers;

namespace GGit
{
    public class GGitPlugin
    {
        private bool _btnAdded;

        private bool _menuAdded;

        public GGitPlugin()
        {

        }

        public void AppendGGitBtn(GH_Canvas canvas)
        {
            if (_btnAdded) return;
            foreach (Control control in DocumentEditor.Controls)
            {
                StatusStrip ss = control as StatusStrip;
                if (ss != null)
                {
                    ToolStripMenuItem gitBtn = new ToolStripMenuItem(null, Properties.Resources.icons8_git_16_green);
                    ToolStripStatusLabel status = new ToolStripStatusLabel("git status");
                    gitBtn.DropDown.Items.Add(status);
                    ss.Items.Add(gitBtn);
                    return;
                }
            }
            _btnAdded = true;
        }

        public void AppendGGitMenu(GH_Canvas canvas)
        {
            if (_menuAdded) return;
            foreach (Control ctrl in DocumentEditor.Controls)
            {
                var menuStrip = ctrl as MenuStrip;
                if (menuStrip != null)
                {
                    ToolStripMenuItem customItem = new ToolStripMenuItem("GGit");
                    ToolStripMenuItem init = new ToolStripMenuItem("Init", null, OnInitRepository);
                    ToolStripMenuItem clone = new ToolStripMenuItem("Clone", null, OnClone);
                    ToolStripMenuItem commit = new ToolStripMenuItem("Commit", null, OnDocumentCommit);
                    ToolStripMenuItem push = new ToolStripMenuItem("Push", null, OnPush);
                    ToolStripMenuItem pull = new ToolStripMenuItem("Pull", null, OnPull);

                    // This can allow user set their own shortcut
                    GH_DocumentEditor.AggregateShortcutMenuItems += (sender, e) =>
                    {
                        e.AppendItem(commit);
                        e.AppendItem(push);
                    };

                    commit.ShortcutKeys = Keys.Control | Keys.K;
                    push.ShortcutKeys = Keys.Control | Keys.Shift | Keys.K;

                    customItem.DropDown.Items.Add(init);
                    customItem.DropDown.Items.Add(clone);
                    customItem.DropDown.Items.Add(commit);
                    customItem.DropDown.Items.Add(push);
                    customItem.DropDown.Items.Add(pull);

                    customItem.DropDown.Items.Add(new ToolStripSeparator());
                    ToolStripMenuItem conf = new ToolStripMenuItem("Author", null, OnSettingAuthor);
                    customItem.DropDown.Items.Add(conf);
                    //ToolStripItem toolStripItem4 = customItem.DropDown.Items.Add("NPKG", null, delegate
                    //{
                    //    Process.Start("https://ncf.cz-studio.cn");
                    //});
                    menuStrip.Items.Add(customItem);
                    return;
                }
            }
            _menuAdded = true;
        }

        private async void OnPush(object sender, EventArgs e)
        {
            LoadingForm loadingForm = new LoadingForm();
            loadingForm.Show(ActiveCanvas.FindForm());

            await Task.Run(() => DocumentPush());
            loadingForm.Close();
        }

        private void OnClone(object sender, EventArgs e)
        {
            DoubleInput cloneForm = new DoubleInput();
            cloneForm.Text = "Clone remote repository";

            cloneForm.label1.Text = "Remote Url";
            cloneForm.label2.Text = "Local Path";

            cloneForm.input2.Cursor = Cursors.Hand;
            cloneForm.input2.Text = "Click me !";
            cloneForm.input2.ReadOnly = true;
            cloneForm.input2.Click += (ed, eg) =>
            {
                if (cloneForm.input1.Text == "")
                {
                    MessageBox.Show("No remote url found");
                    return;
                }
                FolderBrowserDialog folder = new FolderBrowserDialog();
                DialogResult res = folder.ShowDialog();
                if (res == DialogResult.OK || res == DialogResult.Yes)
                {
                    Uri uri = new Uri(cloneForm.input1.Text);
                    string name = uri.Segments[uri.Segments.Length - 1].TrimEnd('/').Split('.')[0];
                    cloneForm.input2.Text = Path.Combine(@folder.SelectedPath, name);
                }
            };

            cloneForm.Show();
            
            cloneForm.ConfirmEvent += async (sd, url, path) =>
            {
                LoadingForm loadingForm = new LoadingForm();
                loadingForm.Show(ActiveCanvas.FindForm());
                await Task.Run(() => CloneRepository(url, path));

                loadingForm.Close();
                cloneForm.Close();
                Uri uri = new Uri(url);
                string name = uri.Segments[uri.Segments.Length - 1].TrimEnd('/').Split('.')[0];
                MessageBox.Show(string.Format("{0} is Cloned to {1}", name, path));
            };
        }

        private void CloneRepository(string url, string path)
        {
            try
            {
                Repository.Clone(url, path);
            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void OnPull(object sender, EventArgs e)
        {
            LoadingForm loadingForm = new LoadingForm();
            loadingForm.Show(ActiveCanvas.FindForm());

            await Task.Run(() => PullRepository());
            loadingForm.Close();
        }

        private void PullRepository ()
        {
            if (ActiveCanvas.Document == null) return;
            string docPath = ActiveCanvas.Document.FilePath;

            try
            {
                using (var repo = new Repository(getWorkDir(docPath)))
                {
                    GH_SettingsServer sserver = new GH_SettingsServer("ggit");
                    string token = sserver.GetValue("AccessToken", "");
                    // Credential information to fetch
                    PullOptions options = new PullOptions();
                    options.FetchOptions = new FetchOptions();
                    options.FetchOptions.CredentialsProvider = (_url, _user, _cred) =>
                        new UsernamePasswordCredentials
                        {
                            // TO DO Set AccessToken From
                            Username = token,
                            Password = string.Empty
                        };

                    // User information to create a merge commit
                    var signature = getSignature();

                    // Pull
                    Commands.Pull(repo, signature, options);
                }
            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DocumentPush()
        {
            // TO DO nothing to push tip
            if (ActiveCanvas.Document == null) return;
            try
            {
                string docPath = ActiveCanvas.Document.FilePath;
                using (var repo = new Repository(getWorkDir(docPath)))
                {
                    GH_SettingsServer sserver = new GH_SettingsServer("ggit");
                    if (repo.Network.Remotes["origin"] == null)
                    {
                        MessageBox.Show("Remote origin is not exist, create a repository remotely, something like github, and paste the url into next window form.");
                        SingleInput inputer = new SingleInput();
                        inputer.Text = "Remote Url";
                        inputer.singleLabel.Text = "Remote Url";
                        inputer.ConfirmEvent += (sender, content) => {
                            repo.Network.Remotes.Add("origin", content);
                            inputer.Close();
                        };
                        inputer.ShowDialog();
                        return;
                    }

                    Remote remote = repo.Network.Remotes["origin"];
                    string token = sserver.GetValue("AccessToken", "");

                    if (token == "")
                    {
                        DialogResult choose = MessageBox.Show("Do you want to set a Personal Access Token to avoid input username and password for each push ?", "Access Token didn't exist", MessageBoxButtons.YesNoCancel);
                        if (choose == DialogResult.OK)
                        {
                            MessageBox.Show("Remote origin is not exist, create a repository remotely, something like github, and paste the url into next window form.");
                            SingleInput inputer = new SingleInput();
                            inputer.Text = "Access Token";
                            inputer.singleLabel.Text = "Token";
                            inputer.ConfirmEvent += (sender, accessToken) => {
                                sserver.SetValue("AccessToken", accessToken);
                                inputer.Close();
                            };
                            inputer.ShowDialog();

                        }
                        else if (choose == DialogResult.No)
                        {
                            DoubleInput loginForm = new DoubleInput();
                            loginForm.Text = "Git login";
                            loginForm.label1.Text = "Username";
                            loginForm.input2.PasswordChar = '*';
                            loginForm.label2.Text = "Passowrd";
                            loginForm.ConfirmEvent += (sd, username, password) => {
                                if (username == "")
                                {
                                    MessageBox.Show("Username can't be empty.");
                                    return;
                                }
                                if (password == "")
                                {
                                    MessageBox.Show("E-mail can't be empty.");
                                    return;
                                }

                                PushOptions options1 = new PushOptions();
                                options1.CredentialsProvider = (_url, _user, _cred) =>
                                    new UsernamePasswordCredentials
                                    {
                                        // TO DO Set AccessToken From
                                        Username = username,
                                        Password = password
                                    };

                                repo.Network.Push(remote, @"refs/heads/master", options1);

                                loginForm.Close();
                            };
                            loginForm.ShowDialog();
                        }
                        return;
                    }

                    PushOptions options = new PushOptions();
                    options.CredentialsProvider = (_url, _user, _cred) =>
                        new UsernamePasswordCredentials
                        {
                            // TO DO Set AccessToken From
                            Username = token,
                            Password = string.Empty
                        };

                    repo.Network.Push(remote, @"refs/heads/master", options);
                    DocumentEditor.SetStatusBarEvent(new GH_RuntimeMessage("Push done !"));
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
            if (getSignature() == null)
            {
                MessageBox.Show(string.Format("Please tell git who you are!"));
                DoubleInput conform = new DoubleInput();
                conform.Show(ActiveCanvas.FindForm());
                return;
            }
            if (ActiveCanvas.Document == null) return;
            bool isModified = ActiveCanvas.Document.IsModified;
            if (isModified)
            {
                MessageBox.Show(string.Format("Please save this document first!"));
            }
            else
            {
                string docPath = ActiveCanvas.Document.FilePath;
                CommitForm commitForm = new CommitForm(docPath);
                try
                {
                    using (var repo = new Repository(getWorkDir(docPath)))
                    {
                        if (!repo.RetrieveStatus().IsDirty)
                        {
                            MessageBox.Show("Nothing To Commit !");
                        }
                        else commitForm.Show(ActiveCanvas.FindForm());
                    }
                }
                catch
                {
                }
            }
        }

        private void OnSettingAuthor(object sender, EventArgs e)
        {
            DoubleInput conform = new DoubleInput();
            conform.Text = "Git author setting";
            conform.label1.Text = "Username";
            conform.label2.Text = "E-mail";
            conform.ConfirmEvent += (sd, author, email) => {
                if (author == "")
                {
                    MessageBox.Show("Username can't be empty.");
                    return;
                }
                if (email == "")
                {
                    MessageBox.Show("E-mail can't be empty.");
                    return;
                }
                string reg = @"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*";
                Regex r = new Regex(reg);
                if (!r.IsMatch(email))
                {
                    MessageBox.Show("E-mail is not a email.");
                    return;
                }

                GH_SettingsServer sserver = new GH_SettingsServer("ggit");
                sserver.SetValue("author", author);
                sserver.SetValue("email", email);
                sserver.WritePersistentSettings();

                conform.Close();
            };
            conform.Show(ActiveCanvas.FindForm());
        }
    }
}
