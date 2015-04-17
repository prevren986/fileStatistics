using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using statsAnalysis._01_controlObjects;
using statsAnalysis._04_entityObjects;
using System.IO;

namespace statsAnalysis._03_guiObjects
{
    partial class mainWindow : Form
    {

        statsAnalysisControl controlObj;
        System.Windows.Forms.DataGridView statsViewTable;


        public mainWindow()
        {
            InitializeComponent();
            controlObj = new statsAnalysisControl(this);
            mainWindowInit();
        }

        public void handlePackageDDSelectionChange (object sender, EventArgs e)
        {
            String currentSelection = getClassDropDownSelection() ;
            populateClassDD(getPackageDropDownSelection());
            classDropDown.SelectedItem = currentSelection;
        }

        public void handleClassDDSelectionChange (object sender, EventArgs e)
        {
            String currentSelection = getPackageDropDownSelection() ;
            populatePackageDD(getClassDropDownSelection());
            packageDropDown.SelectedItem = currentSelection;
        }

        public void handleTableRowSelection (object sender, EventArgs e)
        {
            fileContentsTextBox.Clear();
            if (statsViewTable.SelectedRows.Count > 0 && statsViewTable.SelectedRows.Count <= statsViewTable.Rows.Count)
            {

                int row = statsViewTable.SelectedRows[0].Index;
                String file = statsViewTable.Rows[row].Cells[15].Value.ToString();
                Console.WriteLine("Row: {0}, Value: {1}", row, file);
                displayFileContents(file);
            }
            
        }

        private void displayFileContents (String file)
        {
            if (File.Exists(file))
            {
                String fullText = File.ReadAllText(file);
                fileContentsTextBox.Text = fullText;
            }
            else
            {

            }
        }

        

        private void mainWindowInit ()
        {
            packageDropDown.SelectionChangeCommitted += new EventHandler(handlePackageDDSelectionChange);
            classDropDown.SelectionChangeCommitted += new EventHandler (handleClassDDSelectionChange);
            statsViewTable.SelectionChanged += new EventHandler(handleTableRowSelection);
            statsViewTable.GotFocus += new EventHandler(handleTableRowSelection);
            populatePackageDD();
            populateClassDD();
            displayAllStats();
        }


        private void mainMenuFileImport_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Multiselect = true;

            DialogResult userSelection = openFile.ShowDialog();

            if (userSelection == DialogResult.OK)
            {
                String [] filesSelected = openFile.FileNames;
                for (int i = 0; i < filesSelected.Length; i++)
                {
                    fileContentsTextBox.Text += filesSelected[i] + "\n";
                    fileStatistics newFile = new fileStatistics(filesSelected[i]);
                    controlObj.addFileStat(newFile);
                }
            }

        }


        private void mainMenuFileExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


        private void searchButton_Click(object sender, EventArgs e)
        {

            String package = getPackageDropDownSelection();
            String className = getClassDropDownSelection();

            if (package.Equals("<All>") && className.Equals("<All>"))
            {
                displayAllStats();
            }
            else if (!package.Equals("<All>") && className.Equals("<All>"))
            {
                List<fileStatistics> stats = controlObj.getFileStatsForPackage(package);
                rePopulateTable(stats);
            }
            else if (package.Equals("<All>") && !className.Equals("<All>"))
            {
                List<fileStatistics> stats = controlObj.getFileStatsForClass(className);
                rePopulateTable(stats);
            }
            else //both aren't <All>
            {
                List<fileStatistics> stats = controlObj.getFileStatsForPackageClass(package, className);
                rePopulateTable(stats);
            }
            
        }

        private String getPackageDropDownSelection ()
        {
            if (packageDropDown.SelectedItem == null) return "<All>";
            else return packageDropDown.SelectedItem.ToString();
        }

        private String getClassDropDownSelection ()
        {
            if (classDropDown.SelectedItem == null) return "<All>";
            else return classDropDown.SelectedItem.ToString();
        }


        private void clearButton_Click(object sender, EventArgs e)
        {
            fileContentsTextBox.Text += "";
            packageDropDown.SelectedItem = null;
            classDropDown.SelectedItem = null;
            statsViewTable.Rows.Clear();
        }


        private void testingButton_Click(object sender, EventArgs e)
        {
            fileStatistics newFile = new fileStatistics("File path 1.");
            controlObj.addFileStat(newFile);
            addRowToViewTable(newFile);
        }


        public void addRowToViewTable (fileStatistics file)
        {
                this.statsViewTable.Rows.Add(
                file.getPackageName(), 
                file.getClassName(), 
                file.getUniqueKeywords(), 
                file.getUniqueUdis(), 
                file.getUniqueConstants(), 
                file.getUniqueSpecialChars(), 
                file.getTotalKeywords(), 
                file.getTotalUdis(), 
                file.getTotalConstants(), 
                file.getTotalSpecialChars(), 
                file.getTotalChars(), 
                file.getTotalWhiteSpace(), 
                file.getTotalCommentChars(), 
                file.getPercentWhitespace(), 
                file.getPercentCommentsChars(), 
                file.getFilepath());
        }


        public void updateRowInViewTable (fileStatistics file)
        {
            removeRowFromViewTable(file.getPackageName(), file.getClassName());
            addRowToViewTable(file);
        }


        private void removeRowFromViewTable (String pckName, String clsName)
        {
            foreach (DataGridViewRow row in statsViewTable.Rows)
            {
                if (statsViewTable[0, row.Index].Value.ToString().Equals(packageName) &&
                    statsViewTable[1, row.Index].Value.ToString().Equals(className))
                {
                    Console.WriteLine("Index found: " + row.Index);
                    statsViewTable.Rows.Remove(row);
                }
            }
        }


        public Boolean askUserAboutDuplicate()
        {
            DialogResult userAction = MessageBox.Show ("A file with that package and class combination already exists.\n" +
            "Do you want to replace the one that already exists with the file selected? Clicking 'Yes' will cause all old data to be lost.",
            "Error!", MessageBoxButtons.YesNo);
            if (userAction == DialogResult.Yes) return true;
            else return false;
        }

       
        public void populatePackageDD ()
        {
            packageDropDown.Items.Clear();
            List<String> packages = controlObj.getAllPackages();
            packageDropDown.Items.Add("<All>");
            for (int i=0; i<packages.Count; i++)
            {
                packageDropDown.Items.Add(packages.ElementAt(i));
            }

        }

        //overload for package DD
        public void populatePackageDD (String cls)
        {
            String package = getPackageDropDownSelection();
            if (getClassDropDownSelection() == "<All>") populatePackageDD();
            else
            {
                List<String> packages = controlObj.getPackagesForClass(cls);
                packageDropDown.Items.Clear();
                packageDropDown.Items.Add("<All>");
                for (int i=0; i<packages.Count; i++)
                {
                    packageDropDown.Items.Add(packages.ElementAt(i));
                }
            }
            packageDropDown.SelectedItem = package;
        }

        public void populateClassDD ()
        {
            classDropDown.Items.Clear();
            List<String> classes = controlObj.getAllClasses();
            classDropDown.Items.Add("<All>");
            for (int i = 0; i < classes.Count; i++)
            {
                classDropDown.Items.Add(classes.ElementAt(i));
            }
        }

        //overload or DD populate
        public void populateClassDD (String pckName)
        {
            String currentSelection = getClassDropDownSelection();
            Console.WriteLine("Package DD: " + packageDropDown.SelectedItem);
            if (getPackageDropDownSelection() == "<All>") populateClassDD();
            else
            {
                List<String> classes = controlObj.getClassesForPackage(pckName);
                classDropDown.Items.Clear();
                classDropDown.Items.Add("<All>");
                for (int i=0; i<classes.Count; i++)
                {
                    classDropDown.Items.Add(classes.ElementAt(i));
                }
            }
            classDropDown.SelectedItem = currentSelection;

        }


        private void displayAllStats ()
        {
            packageDropDown.SelectedItem = "<All>";
            classDropDown.SelectedItem = "<All>";

            List<fileStatistics> stats = controlObj.getAllStats();
            statsViewTable.Rows.Clear();

            for (int i=0; i<stats.Count; i++)
            {
                addRowToViewTable(stats.ElementAt(i));
            }
        }

        private void rePopulateTable (List<fileStatistics> stats)
        {
            statsViewTable.Rows.Clear();
            for (int i=0; i<stats.Count; i++)
            {
                addRowToViewTable(stats.ElementAt(i));
                
            }
        }

        //overload for rePopulateTable
        public void rePopulateTable (fileStatistics file)
        {
            statsViewTable.Rows.Clear();
            addRowToViewTable(file);
        }


        
       
         

        public void setPackageDDSelection (String packageName)
        {
            packageDropDown.SelectedItem = packageName;
        }

        public void setClassDDSelection (String className)
        {
            classDropDown.SelectedItem = className;
        }


    }
}
