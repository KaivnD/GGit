using Grasshopper.Kernel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace GGit.Forms
{
    public partial class DoubleInput : Form
    {
        public delegate void ConfirmHandler(object sender, string input1, string input2);
        public event ConfirmHandler ConfirmEvent;
        public DoubleInput()
        {
            InitializeComponent();
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            ConfirmEvent(this, input1.Text, input2.Text);
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
