using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace statsAnalysis._03_guiObjects
{
    partial class mainWindow
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
            //System.Windows.Forms.DataGridView statsViewTable;
            this.packageName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.className = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.uniqueKeywords = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.uniqueUdis = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.uniqueConstants = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.uniqueSpecialChars = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.totalKeywords = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.totalUdis = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.totalConstants = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.totalSpecialChars = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.totalChars = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.totalWhiteSpace = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.totalCommentChars = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.percentWhitespace = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.percentComments = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.filepath = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mainMenuStrip = new System.Windows.Forms.MenuStrip();
            this.mainMenuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.mainMenuFileImport = new System.Windows.Forms.ToolStripMenuItem();
            this.mainMenuFileExit = new System.Windows.Forms.ToolStripMenuItem();
            this.labelPackage = new System.Windows.Forms.Label();
            this.packageDropDown = new System.Windows.Forms.ComboBox();
            this.labelClass = new System.Windows.Forms.Label();
            this.classDropDown = new System.Windows.Forms.ComboBox();
            this.searchButton = new System.Windows.Forms.Button();
            this.clearButton = new System.Windows.Forms.Button();
            this.fileContentsTextBox = new System.Windows.Forms.RichTextBox();
            this.statsViewTable = new System.Windows.Forms.DataGridView();
            this.mainMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.statsViewTable)).BeginInit();
            this.SuspendLayout();
            // 
            // packageName
            // 
            this.packageName.HeaderText = "Package Name";
            this.packageName.Name = "packageName";
            this.packageName.ReadOnly = true;
            // 
            // className
            // 
            this.className.HeaderText = "Class Name";
            this.className.Name = "className";
            this.className.ReadOnly = true;
            // 
            // uniqueKeywords
            // 
            this.uniqueKeywords.HeaderText = "Unique Keywords";
            this.uniqueKeywords.Name = "uniqueKeywords";
            this.uniqueKeywords.ReadOnly = true;
            this.uniqueKeywords.Width = 70;
            // 
            // uniqueUdis
            // 
            this.uniqueUdis.HeaderText = "Unique UDIs";
            this.uniqueUdis.Name = "uniqueUdis";
            this.uniqueUdis.ReadOnly = true;
            this.uniqueUdis.Width = 70;
            // 
            // uniqueConstants
            // 
            this.uniqueConstants.HeaderText = "Unique Constants";
            this.uniqueConstants.Name = "uniqueConstants";
            this.uniqueConstants.ReadOnly = true;
            this.uniqueConstants.Width = 70;
            // 
            // uniqueSpecialChars
            // 
            this.uniqueSpecialChars.HeaderText = "Unique Special Chars";
            this.uniqueSpecialChars.Name = "uniqueSpecialChars";
            this.uniqueSpecialChars.ReadOnly = true;
            this.uniqueSpecialChars.Width = 70;
            // 
            // totalKeywords
            // 
            this.totalKeywords.HeaderText = "Total Keywords";
            this.totalKeywords.Name = "totalKeywords";
            this.totalKeywords.ReadOnly = true;
            this.totalKeywords.Width = 70;
            // 
            // totalUdis
            // 
            this.totalUdis.HeaderText = "Total UDIs";
            this.totalUdis.Name = "totalUdis";
            this.totalUdis.ReadOnly = true;
            this.totalUdis.Width = 70;
            // 
            // totalConstants
            // 
            this.totalConstants.HeaderText = "Total Constants";
            this.totalConstants.Name = "totalConstants";
            this.totalConstants.ReadOnly = true;
            this.totalConstants.Width = 70;
            // 
            // totalSpecialChars
            // 
            this.totalSpecialChars.HeaderText = "Total Special Chars";
            this.totalSpecialChars.Name = "totalSpecialChars";
            this.totalSpecialChars.ReadOnly = true;
            this.totalSpecialChars.Width = 70;
            // 
            // totalChars
            // 
            this.totalChars.HeaderText = "Total Characters";
            this.totalChars.Name = "totalChars";
            this.totalChars.ReadOnly = true;
            this.totalChars.Width = 70;
            // 
            // totalWhiteSpace
            // 
            this.totalWhiteSpace.HeaderText = "Total Whitespace Chars";
            this.totalWhiteSpace.Name = "totalWhiteSpace";
            this.totalWhiteSpace.ReadOnly = true;
            this.totalWhiteSpace.Width = 70;
            // 
            // totalCommentChars
            // 
            this.totalCommentChars.HeaderText = "Total Comments";
            this.totalCommentChars.Name = "totalCommentChars";
            this.totalCommentChars.ReadOnly = true;
            // 
            // percentWhitespace
            // 
            this.percentWhitespace.HeaderText = "Percent Whitespace";
            this.percentWhitespace.Name = "percentWhitespace";
            this.percentWhitespace.ReadOnly = true;
            this.percentWhitespace.Width = 70;
            // 
            // percentComments
            // 
            this.percentComments.HeaderText = "Percent Comments";
            this.percentComments.Name = "percentComments";
            this.percentComments.ReadOnly = true;
            this.percentComments.Width = 70;
            // 
            // filepath
            // 
            this.filepath.HeaderText = "File Path";
            this.filepath.Name = "filepath";
            this.filepath.ReadOnly = true;
            this.filepath.Visible = false;
            // 
            // mainMenuStrip
            // 
            this.mainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mainMenuFile});
            this.mainMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.mainMenuStrip.Name = "mainMenuStrip";
            this.mainMenuStrip.Size = new System.Drawing.Size(1209, 24);
            this.mainMenuStrip.TabIndex = 0;
            // 
            // mainMenuFile
            // 
            this.mainMenuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mainMenuFileImport,
            this.mainMenuFileExit});
            this.mainMenuFile.Name = "mainMenuFile";
            this.mainMenuFile.Size = new System.Drawing.Size(37, 20);
            this.mainMenuFile.Text = "File";
            // 
            // mainMenuFileImport
            // 
            this.mainMenuFileImport.Name = "mainMenuFileImport";
            this.mainMenuFileImport.Size = new System.Drawing.Size(163, 22);
            this.mainMenuFileImport.Text = "Import New Files";
            this.mainMenuFileImport.Click += new System.EventHandler(this.mainMenuFileImport_Click);
            // 
            // mainMenuFileExit
            // 
            this.mainMenuFileExit.Name = "mainMenuFileExit";
            this.mainMenuFileExit.Size = new System.Drawing.Size(163, 22);
            this.mainMenuFileExit.Text = "Exit";
            this.mainMenuFileExit.Click += new System.EventHandler(this.mainMenuFileExit_Click);
            // 
            // labelPackage
            // 
            this.labelPackage.AutoSize = true;
            this.labelPackage.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.labelPackage.Location = new System.Drawing.Point(13, 42);
            this.labelPackage.Name = "labelPackage";
            this.labelPackage.Size = new System.Drawing.Size(70, 18);
            this.labelPackage.TabIndex = 1;
            this.labelPackage.Text = "Package:";
            // 
            // packageDropDown
            // 
            this.packageDropDown.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.packageDropDown.FormattingEnabled = true;
            this.packageDropDown.Location = new System.Drawing.Point(85, 39);
            this.packageDropDown.Name = "packageDropDown";
            this.packageDropDown.Size = new System.Drawing.Size(194, 26);
            this.packageDropDown.TabIndex = 2;
            // 
            // labelClass
            // 
            this.labelClass.AutoSize = true;
            this.labelClass.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.labelClass.Location = new System.Drawing.Point(314, 42);
            this.labelClass.Name = "labelClass";
            this.labelClass.Size = new System.Drawing.Size(50, 18);
            this.labelClass.TabIndex = 3;
            this.labelClass.Text = "Class:";
            // 
            // classDropDown
            // 
            this.classDropDown.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.classDropDown.FormattingEnabled = true;
            this.classDropDown.Location = new System.Drawing.Point(370, 39);
            this.classDropDown.Name = "classDropDown";
            this.classDropDown.Size = new System.Drawing.Size(197, 26);
            this.classDropDown.TabIndex = 4;
            // 
            // searchButton
            // 
            this.searchButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.searchButton.Location = new System.Drawing.Point(587, 39);
            this.searchButton.Name = "searchButton";
            this.searchButton.Size = new System.Drawing.Size(80, 26);
            this.searchButton.TabIndex = 5;
            this.searchButton.Text = "Search";
            this.searchButton.UseVisualStyleBackColor = true;
            this.searchButton.Click += new System.EventHandler(this.searchButton_Click);
            // 
            // clearButton
            // 
            this.clearButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.clearButton.Location = new System.Drawing.Point(681, 39);
            this.clearButton.Name = "clearButton";
            this.clearButton.Size = new System.Drawing.Size(73, 26);
            this.clearButton.TabIndex = 6;
            this.clearButton.Text = "Clear";
            this.clearButton.UseVisualStyleBackColor = true;
            this.clearButton.Click += new System.EventHandler(this.clearButton_Click);
            // 
            // fileContentsTextBox
            // 
            this.fileContentsTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.fileContentsTextBox.Location = new System.Drawing.Point(13, 481);
            this.fileContentsTextBox.Name = "fileContentsTextBox";
            this.fileContentsTextBox.Size = new System.Drawing.Size(1184, 303);
            this.fileContentsTextBox.TabIndex = 7;
            this.fileContentsTextBox.Text = "";
            // 
            // statsViewTable
            // 
            statsViewTable.AllowUserToAddRows = false;
            statsViewTable.AllowUserToDeleteRows = false;
            statsViewTable.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            statsViewTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            statsViewTable.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.packageName,
            this.className,
            this.uniqueKeywords,
            this.uniqueUdis,
            this.uniqueConstants,
            this.uniqueSpecialChars,
            this.totalKeywords,
            this.totalUdis,
            this.totalConstants,
            this.totalSpecialChars,
            this.totalChars,
            this.totalWhiteSpace,
            this.totalCommentChars,
            this.percentWhitespace,
            this.percentComments,
            this.filepath});
            statsViewTable.Location = new System.Drawing.Point(13, 126);
            statsViewTable.MultiSelect = false;
            statsViewTable.Name = "statsViewTable";
            statsViewTable.ReadOnly = true;
            statsViewTable.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            statsViewTable.Size = new System.Drawing.Size(1184, 339);
            statsViewTable.TabIndex = 9;
            // 
            // mainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1209, 796);
            this.Controls.Add(statsViewTable);
            this.Controls.Add(this.fileContentsTextBox);
            this.Controls.Add(this.clearButton);
            this.Controls.Add(this.searchButton);
            this.Controls.Add(this.classDropDown);
            this.Controls.Add(this.labelClass);
            this.Controls.Add(this.packageDropDown);
            this.Controls.Add(this.labelPackage);
            this.Controls.Add(this.mainMenuStrip);
            this.MainMenuStrip = this.mainMenuStrip;
            this.Name = "mainWindow";
            this.Text = "Form1";
            this.mainMenuStrip.ResumeLayout(false);
            this.mainMenuStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(statsViewTable)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip mainMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem mainMenuFile;
        private System.Windows.Forms.ToolStripMenuItem mainMenuFileImport;
        private System.Windows.Forms.ToolStripMenuItem mainMenuFileExit;
        private System.Windows.Forms.Label labelPackage;
        private System.Windows.Forms.ComboBox packageDropDown;
        private System.Windows.Forms.Label labelClass;
        private System.Windows.Forms.ComboBox classDropDown;
        private System.Windows.Forms.Button searchButton;
        private System.Windows.Forms.Button clearButton;
        private System.Windows.Forms.RichTextBox fileContentsTextBox;
        private DataGridViewTextBoxColumn packageName;
        private DataGridViewTextBoxColumn className;
        private DataGridViewTextBoxColumn uniqueKeywords;
        private DataGridViewTextBoxColumn uniqueUdis;
        private DataGridViewTextBoxColumn uniqueConstants;
        private DataGridViewTextBoxColumn uniqueSpecialChars;
        private DataGridViewTextBoxColumn totalKeywords;
        private DataGridViewTextBoxColumn totalUdis;
        private DataGridViewTextBoxColumn totalConstants;
        private DataGridViewTextBoxColumn totalSpecialChars;
        private DataGridViewTextBoxColumn totalChars;
        private DataGridViewTextBoxColumn totalWhiteSpace;
        private DataGridViewTextBoxColumn totalCommentChars;
        private DataGridViewTextBoxColumn percentWhitespace;
        private DataGridViewTextBoxColumn percentComments;
        private DataGridViewTextBoxColumn filepath;
    }
}

