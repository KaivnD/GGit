namespace GGit.Forms
{
    partial class CommitForm
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
            this.statusList = new BrightIdeasSoftware.ObjectListView();
            this.olvColumn4 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn1 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn2 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn3 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            ((System.ComponentModel.ISupportInitialize)(this.statusList)).BeginInit();
            this.SuspendLayout();
            // 
            // statusList
            // 
            this.statusList.AllColumns.Add(this.olvColumn4);
            this.statusList.AllColumns.Add(this.olvColumn1);
            this.statusList.AllColumns.Add(this.olvColumn2);
            this.statusList.AllColumns.Add(this.olvColumn3);
            this.statusList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.statusList.CellEditUseWholeCell = false;
            this.statusList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumn4,
            this.olvColumn1,
            this.olvColumn2,
            this.olvColumn3});
            this.statusList.Cursor = System.Windows.Forms.Cursors.Default;
            this.statusList.Font = new System.Drawing.Font("微软雅黑 Light", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.statusList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.statusList.HideSelection = false;
            this.statusList.Location = new System.Drawing.Point(0, 0);
            this.statusList.Margin = new System.Windows.Forms.Padding(0);
            this.statusList.Name = "statusList";
            this.statusList.Size = new System.Drawing.Size(283, 200);
            this.statusList.TabIndex = 1;
            this.statusList.UseCompatibleStateImageBehavior = false;
            this.statusList.View = System.Windows.Forms.View.Details;
            // 
            // olvColumn4
            // 
            this.olvColumn4.AspectName = "Status";
            this.olvColumn4.IsVisible = false;
            this.olvColumn4.Width = 25;
            // 
            // olvColumn1
            // 
            this.olvColumn1.AspectName = "Path";
            this.olvColumn1.Groupable = false;
            this.olvColumn1.IsEditable = false;
            this.olvColumn1.Searchable = false;
            this.olvColumn1.ShowTextInHeader = false;
            this.olvColumn1.Sortable = false;
            this.olvColumn1.Text = "File Path";
            this.olvColumn1.Width = 150;
            // 
            // olvColumn2
            // 
            this.olvColumn2.AspectName = "Stage";
            this.olvColumn2.ButtonPadding = new System.Drawing.Size(0, 0);
            this.olvColumn2.ButtonSize = new System.Drawing.Size(25, 25);
            this.olvColumn2.ButtonSizing = BrightIdeasSoftware.OLVColumn.ButtonSizingMode.CellBounds;
            this.olvColumn2.Groupable = false;
            this.olvColumn2.IsButton = true;
            this.olvColumn2.IsEditable = false;
            this.olvColumn2.ShowTextInHeader = false;
            this.olvColumn2.Sortable = false;
            this.olvColumn2.Text = "√";
            this.olvColumn2.Width = 25;
            // 
            // olvColumn3
            // 
            this.olvColumn3.AspectName = "Discard";
            this.olvColumn3.ButtonPadding = new System.Drawing.Size(0, 0);
            this.olvColumn3.ButtonSize = new System.Drawing.Size(25, 25);
            this.olvColumn3.ButtonSizing = BrightIdeasSoftware.OLVColumn.ButtonSizingMode.CellBounds;
            this.olvColumn3.Groupable = false;
            this.olvColumn3.IsButton = true;
            this.olvColumn3.IsEditable = false;
            this.olvColumn3.Searchable = false;
            this.olvColumn3.ShowTextInHeader = false;
            this.olvColumn3.Sortable = false;
            this.olvColumn3.Text = "×";
            this.olvColumn3.UseFiltering = false;
            this.olvColumn3.Width = 25;
            // 
            // CommitForm
            // 
            this.AccessibleRole = System.Windows.Forms.AccessibleRole.Pane;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 461);
            this.Controls.Add(this.statusList);
            this.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "CommitForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Commit";
            ((System.ComponentModel.ISupportInitialize)(this.statusList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private BrightIdeasSoftware.ObjectListView statusList;
        private BrightIdeasSoftware.OLVColumn olvColumn1;
        private BrightIdeasSoftware.OLVColumn olvColumn2;
        private BrightIdeasSoftware.OLVColumn olvColumn3;
        private BrightIdeasSoftware.OLVColumn olvColumn4;
    }
}