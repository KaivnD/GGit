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
    public partial class SignatureConfig : Form
    {
        public SignatureConfig()
        {
            InitializeComponent();
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            if(authorInput.Text == "")
            {
                MessageBox.Show("Username can't be empty.");
                return;
            }
            if (emailInput.Text == "")
            {
                MessageBox.Show("E-mail can't be empty.");
                return;
            }
            string reg = @"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*";
            Regex r = new Regex(reg);
            if (!r.IsMatch(emailInput.Text))
            {
                MessageBox.Show("E-mail is not a email.");
                return;
            }

            GH_SettingsServer sserver = new GH_SettingsServer("ggit");
            sserver.SetValue("author", authorInput.Text);
            sserver.SetValue("email", emailInput.Text);
            sserver.WritePersistentSettings();
            Close();
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
