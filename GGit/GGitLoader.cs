using Grasshopper;
using Grasshopper.GUI;
using Grasshopper.GUI.Canvas;
using Grasshopper.Kernel;
using System;
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

        private bool _added;

        public override GH_LoadingInstruction PriorityLoad()
        {
            CanvasCreated += AppendGGit;
            return GH_LoadingInstruction.Proceed;
        }

        private void AppendGGit(GH_Canvas canvas)
        {
            if (_added) return;
            foreach(Control control in canvas.Parent.Controls)
            {
                var toolstrip = control as ToolStrip;
                if (toolstrip != null)
                {
                    toolstrip.Items.Add("New Button", null, OnClick);
                    return;
                }
            }
            _added = true;
        }

        private void OnClick(object sender, EventArgs e)
        {
            MessageBox.Show("Haha", "lalsdf", MessageBoxButtons.OK);
        }
    }
}
