using GGit.Utils;
using System.Collections.Generic;
using System.Windows.Forms;

namespace GGit.Forms
{
    partial class GGitViewer
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.gH_Canvas1 = new Grasshopper.GUI.Canvas.GH_Canvas();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gH_Canvas1
            // 
            this.gH_Canvas1.ActiveInteraction = null;
            this.gH_Canvas1.ActiveObject = null;
            this.gH_Canvas1.ActiveWidget = null;
            this.gH_Canvas1.AllowDrop = true;
            this.gH_Canvas1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gH_Canvas1.Document = null;
            this.gH_Canvas1.Location = new System.Drawing.Point(0, 0);
            this.gH_Canvas1.Margin = new System.Windows.Forms.Padding(0);
            this.gH_Canvas1.ModifiersEnabled = false;
            this.gH_Canvas1.Name = "gH_Canvas1";
            this.gH_Canvas1.Size = new System.Drawing.Size(519, 361);
            this.gH_Canvas1.TabIndex = 0;
            this.gH_Canvas1.Text = "gH_Canvas1";
            this.gH_Canvas1.ValidGraphics = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.richTextBox1);
            this.splitContainer1.Panel1.Controls.Add(this.listBox1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.gH_Canvas1);
            this.splitContainer1.Size = new System.Drawing.Size(784, 361);
            this.splitContainer1.SplitterDistance = 261;
            this.splitContainer1.TabIndex = 0;
            // 
            // listBox1
            // 
            this.listBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listBox1.DisplayMember = "Hash";
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 12;
            this.listBox1.Location = new System.Drawing.Point(12, 12);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(236, 180);
            this.listBox1.TabIndex = 0;
            this.listBox1.ValueMember = "Message";
            // 
            // richTextBox1
            // 
            this.richTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox1.Location = new System.Drawing.Point(12, 198);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.Size = new System.Drawing.Size(236, 106);
            this.richTextBox1.TabIndex = 3;
            this.richTextBox1.Text = "";
            // 
            // GGitViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 361);
            this.Controls.Add(this.splitContainer1);
            this.Name = "GGitViewer";
            this.ShowIcon = false;
            this.Text = "GGitViewer";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        internal Grasshopper.GUI.Canvas.GH_Canvas gH_Canvas1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        internal System.Windows.Forms.ListBox listBox1;
        private RichTextBox richTextBox1;
    }
}