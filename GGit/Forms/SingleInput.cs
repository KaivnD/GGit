using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GGit.Forms
{
    public partial class SingleInput : Form
    {
        public delegate void ConfirmHandler(object sender, string input);
        public event ConfirmHandler ConfirmEvent;
        public SingleInput()
        {
            InitializeComponent();
        }

        private void confirmBtn_Click(object sender, EventArgs e)
        {
            if (inputContent.Text != "")
            {
                ConfirmEvent(this, inputContent.Text);
            }
            else MessageBox.Show("Input can't be empty.");
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
