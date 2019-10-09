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
                    ToolStripMenuItem push = new ToolStripMenuItem("Push", Properties.Resources.git, OnDocumentCommit);
                    //init.Click += OnClick;
                    commit.ShortcutKeys = Keys.Control | Keys.K;
                    customItem.DropDown.Items.Add(init);
                    customItem.DropDown.Items.Add(commit);
                    customItem.DropDown.Items.Add(push);

                    customItem.DropDown.Items.Add(new ToolStripSeparator());
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
            if (ActiveCanvas.Document == null) return;
            bool isModified = ActiveCanvas.Document.IsModified;
            if (isModified)
            {
                MessageBox.Show(string.Format("Please save this document first!"));
            } else
            {
                string docPath = ActiveCanvas.Document.FilePath;
                CommitForm commitForm = new CommitForm(docPath);
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
