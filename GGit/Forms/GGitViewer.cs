using Grasshopper.GUI.Canvas;
using Grasshopper.GUI;
using System.Windows.Forms;
using Drawing = System.Drawing;
using Grasshopper.Kernel;
using Grasshopper.GUI.Canvas.Interaction;
using GGit.Utils;
using System.Collections.Generic;

namespace GGit.Forms
{
    public partial class GGitViewer : Form
    {
        internal GH_Document document = null;
        public string _filepath;
        public BindingSource listSource = new BindingSource();

        public GGitViewer(string filepath)
        {
            _filepath = filepath;
            GH_DocumentIO doc = new GH_DocumentIO();
            doc.Open(filepath);
            document = doc.Document;
            InitializeComponent();
            ListInit();
        }

        private void ListInit()
        {
            List<GGitHistroy> list = new List<GGitHistroy>();
            listSource.DataSource = list;

            list.Add(new GGitHistroy("fdsajgsk-fdswa13fdsa-dsagdsafds", "反对沙皇大飒飒东风"));
            list.Add(new GGitHistroy("fdsajgsk-fdswa13fdsa-dsagdsafds", "岁的法国郭德纲大发噶"));
            list.Add(new GGitHistroy("fdsajgsk-fdswa13fdsa-dsagdsafds", "反对沙皇大飒飒东风"));
            list.Add(new GGitHistroy("fdsajgsk-fdswa13fdsa-dsagdsafds", "岁的法国郭德纲大发噶"));
            list.Add(new GGitHistroy("fdsajgsk-fdswa13fdsa-dsagdsafds", "反对沙皇大飒飒东风"));
            list.Add(new GGitHistroy("fdsajgsk-fdswa13fdsa-dsagdsafds", "岁的法国郭德纲大发噶"));
            list.Add(new GGitHistroy("fdsajgsk-fdswa13fdsa-dsagdsafds", "反对沙皇大飒飒东风"));
            list.Add(new GGitHistroy("fdsajgsk-fdswa13fdsa-dsagdsafds", "反对沙皇大飒飒东风"));
            list.Add(new GGitHistroy("fdsajgsk-fdswa13fdsa-dsagdsafds", "反对沙皇大飒飒东风"));
            list.Add(new GGitHistroy("fdsajgsk-fdswa13fdsa-dsagdsafds", "反对沙皇大飒飒东风"));
            list.Add(new GGitHistroy("fdsajgsk-fdswa13fdsa-dsagdsafds", "反对沙皇大飒飒东风"));
            list.Add(new GGitHistroy("fdsajgsk-fdswa13fdsa-dsagdsafds", "反对沙皇大飒飒东风"));
            list.Add(new GGitHistroy("fdsajgsk-fdswa13fdsa-dsagdsafds", "反对沙皇大飒飒东风"));

            list.Add(new GGitHistroy("fdsajgsk-fdswa13fdsa-dsagdsafds", "岁的法国郭德纲大发噶"));
            list.Add(new GGitHistroy("fdsajgsk-fdswa13fdsa-dsagdsafds", "岁的法国郭德纲大发噶"));
            list.Add(new GGitHistroy("fdsajgsk-fdswa13fdsa-dsagdsafds", "岁的法国郭德纲大发噶"));
            list.Add(new GGitHistroy("fdsajgsk-fdswa13fdsa-dsagdsafds", "岁的法国郭德纲大发噶"));
            list.Add(new GGitHistroy("fdsajgsk-fdswa13fdsa-dsagdsafds", "岁的法国郭德纲大发噶"));
            list.Add(new GGitHistroy("fdsajgsk-fdswa13fdsa-dsagdsafds", "岁的法国郭德纲大发噶"));
            list.Add(new GGitHistroy("fdsajgsk-fdswa13fdsa-dsagdsafds", "岁的法国郭德纲大发噶"));
            listBox1.DataSource = listSource;
        }

        public void FadeIn()
        {
            if (document != null) gH_Canvas1.Document = document;
            if (!base.Visible)
            {
                Show();
                gH_Canvas1.DestroyMRUPanels();
                Select();
            }
        }
    }
}
