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

namespace GGit
{
    public class GGitLoader : GH_AssemblyPriority
    {
        public GGitLoader()
        {
        }

        public override GH_LoadingInstruction PriorityLoad()
        {
            GGitPlugin plugin = new GGitPlugin();
            CanvasCreated += plugin.AppendGGitBtn;
            CanvasCreated += plugin.AppendGGitMenu;
            return GH_LoadingInstruction.Proceed;
        }
    }
}
