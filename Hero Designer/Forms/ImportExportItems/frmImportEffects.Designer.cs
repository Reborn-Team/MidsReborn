
namespace Hero_Designer
{
    public partial class frmImportEffects
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
            this.components = (System.ComponentModel.IContainer)new System.ComponentModel.Container();

            this.btnCheckAll = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.dlgBrowse = new System.Windows.Forms.OpenFileDialog();
            this.btnUncheckAll = new System.Windows.Forms.Button();
            this.btnImport = new System.Windows.Forms.Button();
            this.lstImport = new System.Windows.Forms.ListView();
            this.ColumnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.ColumnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.ColumnHeader4 = new System.Windows.Forms.ColumnHeader();
            this.ColumnHeader5 = new System.Windows.Forms.ColumnHeader();
            this.ColumnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.ColumnHeader6 = new System.Windows.Forms.ColumnHeader();
            this.Label8 = new System.Windows.Forms.Label();
            this.lblDate = new System.Windows.Forms.Label();
            this.udRevision = new System.Windows.Forms.NumericUpDown();
            this.Label6 = new System.Windows.Forms.Label();
            this.lblFile = new System.Windows.Forms.Label();
            this.btnFile = new System.Windows.Forms.Button();
            this.btnEraseAll = new System.Windows.Forms.Button();
            this.txtNoAU = new System.Windows.Forms.Label();
            this.HideUnchanged = new System.Windows.Forms.Button();
            this.udRevision.BeginInit();
            this.SuspendLayout();

            this.btnCheckAll.Location = new System.Drawing.Point(12, 545);
            this.btnCheckAll.Name = "btnCheckAll";

            this.btnCheckAll.Size = new System.Drawing.Size(75, 23);
            this.btnCheckAll.TabIndex = 60;
            this.btnCheckAll.Text = "Check All";
            this.btnCheckAll.UseVisualStyleBackColor = true;
            this.btnCheckAll.Click += new System.EventHandler(btnCheckAll_Click);

            this.btnClose.Location = new System.Drawing.Point(904, 516);
            this.btnClose.Name = "btnClose";

            this.btnClose.Size = new System.Drawing.Size(86, 23);
            this.btnClose.TabIndex = 59;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(btnClose_Click);
            this.dlgBrowse.DefaultExt = "csv";
            this.dlgBrowse.Filter = "CSV Spreadsheets (*.csv)|*.csv";

            this.btnUncheckAll.Location = new System.Drawing.Point(93, 545);
            this.btnUncheckAll.Name = "btnUncheckAll";

            this.btnUncheckAll.Size = new System.Drawing.Size(75, 23);
            this.btnUncheckAll.TabIndex = 61;
            this.btnUncheckAll.Text = "Uncheck All";
            this.btnUncheckAll.UseVisualStyleBackColor = true;
            this.btnUncheckAll.Click += new System.EventHandler(btnUncheckAll_Click);

            this.btnImport.Location = new System.Drawing.Point(904, 77);
            this.btnImport.Name = "btnImport";

            this.btnImport.Size = new System.Drawing.Size(86, 22);
            this.btnImport.TabIndex = 58;
            this.btnImport.Text = "Import";
            this.btnImport.UseVisualStyleBackColor = true;
            this.btnImport.Click += new System.EventHandler(btnImport_Click);
            this.lstImport.CheckBoxes = true;
            this.lstImport.Columns.AddRange(new System.Windows.Forms.ColumnHeader[6]
            {
        this.ColumnHeader1,
        this.ColumnHeader2,
        this.ColumnHeader4,
        this.ColumnHeader5,
        this.ColumnHeader3,
        this.ColumnHeader6
            });
            this.lstImport.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;

            this.lstImport.Location = new System.Drawing.Point(12, 77);
            this.lstImport.Name = "lstImport";

            this.lstImport.Size = new System.Drawing.Size(886, 462);
            this.lstImport.TabIndex = 57;
            this.lstImport.UseCompatibleStateImageBehavior = false;
            this.lstImport.View = System.Windows.Forms.View.Details;
            this.ColumnHeader1.Text = "Power";
            this.ColumnHeader1.Width = 293;
            this.ColumnHeader2.Text = "Effect";
            this.ColumnHeader2.Width = 87;
            this.ColumnHeader4.Text = "New";
            this.ColumnHeader4.Width = 53;
            this.ColumnHeader5.Text = "Modified";
            this.ColumnHeader3.Text = "PowerIndex Change";
            this.ColumnHeader3.Width = 93;
            this.ColumnHeader6.Text = "Change";
            this.ColumnHeader6.Width = 310;

            this.Label8.Location = new System.Drawing.Point(632, 53);
            this.Label8.Name = "Label8";

            this.Label8.Size = new System.Drawing.Size(65, 18);
            this.Label8.TabIndex = 56;
            this.Label8.Text = "Revision:";
            this.Label8.TextAlign = System.Drawing.ContentAlignment.TopRight;

            this.lblDate.Location = new System.Drawing.Point(9, 53);
            this.lblDate.Name = "lblDate";

            this.lblDate.Size = new System.Drawing.Size(175, 18);
            this.lblDate.TabIndex = 54;
            this.lblDate.Text = "Date:";

            this.udRevision.Location = new System.Drawing.Point(703, 51);
            this.udRevision.Maximum = new System.Decimal(new int[4]
            {
                (int) ushort.MaxValue,
                0,
                0,
                0
            });
            this.udRevision.Name = "udRevision";

            this.udRevision.Size = new System.Drawing.Size(116, 20);
            this.udRevision.TabIndex = 53;

            this.Label6.Location = new System.Drawing.Point(12, 9);
            this.Label6.Name = "Label6";

            this.Label6.Size = new System.Drawing.Size(150, 14);
            this.Label6.TabIndex = 52;
            this.Label6.Text = "Effect Definition File:";
            this.lblFile.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;

            this.lblFile.Location = new System.Drawing.Point(12, 25);
            this.lblFile.Name = "lblFile";

            this.lblFile.Size = new System.Drawing.Size(807, 23);
            this.lblFile.TabIndex = 51;
            this.lblFile.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            this.btnFile.Location = new System.Drawing.Point(825, 25);
            this.btnFile.Name = "btnFile";

            this.btnFile.Size = new System.Drawing.Size(165, 23);
            this.btnFile.TabIndex = 50;
            this.btnFile.Text = "Load / Re-Load";
            this.btnFile.UseVisualStyleBackColor = true;
            this.btnFile.Click += new System.EventHandler(btnFile_Click);

            this.btnEraseAll.Location = new System.Drawing.Point(904, 120);
            this.btnEraseAll.Name = "btnEraseAll";

            this.btnEraseAll.Size = new System.Drawing.Size(86, 69);
            this.btnEraseAll.TabIndex = 62;
            this.btnEraseAll.Text = "Erase All Effects";
            this.btnEraseAll.UseVisualStyleBackColor = true;
            this.btnEraseAll.Click += new System.EventHandler(btnEraseAll_Click);

            this.txtNoAU.Location = new System.Drawing.Point(904, 192);
            this.txtNoAU.Name = "txtNoAU";

            this.txtNoAU.Size = new System.Drawing.Size(86, 55);
            this.txtNoAU.TabIndex = 63;
            this.txtNoAU.Text = "0 Powers Locked";
            this.txtNoAU.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            this.HideUnchanged.Location = new System.Drawing.Point(174, 545);
            this.HideUnchanged.Name = "HideUnchanged";

            this.HideUnchanged.Size = new System.Drawing.Size(99, 23);
            this.HideUnchanged.TabIndex = 64;
            this.HideUnchanged.Text = "Hide Unchanged";
            this.HideUnchanged.UseVisualStyleBackColor = true;
            this.HideUnchanged.Click += new System.EventHandler(HideUnchanged_Click);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;

            this.ClientSize = new System.Drawing.Size(1002, 573);
            this.Controls.Add((System.Windows.Forms.Control)this.HideUnchanged);
            this.Controls.Add((System.Windows.Forms.Control)this.txtNoAU);
            this.Controls.Add((System.Windows.Forms.Control)this.btnEraseAll);
            this.Controls.Add((System.Windows.Forms.Control)this.btnCheckAll);
            this.Controls.Add((System.Windows.Forms.Control)this.btnClose);
            this.Controls.Add((System.Windows.Forms.Control)this.btnUncheckAll);
            this.Controls.Add((System.Windows.Forms.Control)this.btnImport);
            this.Controls.Add((System.Windows.Forms.Control)this.lstImport);
            this.Controls.Add((System.Windows.Forms.Control)this.Label8);
            this.Controls.Add((System.Windows.Forms.Control)this.lblDate);
            this.Controls.Add((System.Windows.Forms.Control)this.udRevision);
            this.Controls.Add((System.Windows.Forms.Control)this.Label6);
            this.Controls.Add((System.Windows.Forms.Control)this.lblFile);
            this.Controls.Add((System.Windows.Forms.Control)this.btnFile);
            this.Font = new System.Drawing.Font("Arial", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (byte)0);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.ShowInTaskbar = false;
            this.Text = "Import Power Effects";
            this.udRevision.EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        System.Windows.Forms.Button btnCheckAll;
        System.Windows.Forms.Button btnClose;
        System.Windows.Forms.Button btnEraseAll;
        System.Windows.Forms.Button btnFile;
        System.Windows.Forms.Button btnImport;
        System.Windows.Forms.Button btnUncheckAll;
        System.Windows.Forms.ColumnHeader ColumnHeader1;
        System.Windows.Forms.ColumnHeader ColumnHeader2;
        System.Windows.Forms.ColumnHeader ColumnHeader3;
        System.Windows.Forms.ColumnHeader ColumnHeader4;
        System.Windows.Forms.ColumnHeader ColumnHeader5;
        System.Windows.Forms.ColumnHeader ColumnHeader6;
        System.Windows.Forms.OpenFileDialog dlgBrowse;
        System.Windows.Forms.Button HideUnchanged;
        System.Windows.Forms.Label Label6;
        System.Windows.Forms.Label Label8;
        System.Windows.Forms.Label lblDate;
        System.Windows.Forms.Label lblFile;
        System.Windows.Forms.ListView lstImport;
        System.Windows.Forms.Label txtNoAU;
        System.Windows.Forms.NumericUpDown udRevision;
    }
}