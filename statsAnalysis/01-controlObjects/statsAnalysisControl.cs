using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using statsAnalysis._02_dbObjects;
using statsAnalysis._04_entityObjects;
using statsAnalysis._03_guiObjects;
using statsAnalysis;

namespace statsAnalysis._01_controlObjects
{
    class statsAnalysisControl
    {
        databaseWrapper db;
        mainWindow gui;

        public statsAnalysisControl(mainWindow win)
        {
            db = new databaseWrapper(Environment.GetFolderPath(
                Environment.SpecialFolder.ApplicationData).ToString());
            gui = win;
        }

        public void addFileStat(fileStatistics file)
        {
            

            if (db.doesPackageExist(file.getPackageName()) == false && db.doesClassExist(file.getClassName()) == false)
            {
                //entry does not exists b/c its class and package don't exist.
                db.addPackage(file.getPackageName());
                db.addClass(file.getClassName());
                db.addFileStat(file);
                gui.setPackageDDSelection(file.getPackageName());
                gui.setClassDDSelection(file.getClassName());
                gui.populatePackageDD(file.getClassName());
                gui.populateClassDD(file.getPackageName());
                gui.rePopulateTable(file);

            }
            else if (db.doesPackageExist(file.getPackageName()) == true && db.doesClassExist(file.getClassName()) == false)
            {
                db.addClass(file.getClassName());
                db.addFileStat(file);
                gui.setPackageDDSelection(file.getPackageName());
                gui.setClassDDSelection(file.getClassName());
                gui.populatePackageDD(file.getClassName());
                gui.populateClassDD(file.getPackageName());
                gui.rePopulateTable(file);
            }
            else if (db.doesPackageExist(file.getPackageName()) == false && db.doesClassExist(file.getClassName()) == true)
            {
                db.addPackage(file.getPackageName());
                db.addFileStat(file);
                gui.setPackageDDSelection(file.getPackageName());
                gui.setClassDDSelection(file.getClassName());
                gui.populatePackageDD(file.getClassName());
                gui.populateClassDD(file.getPackageName());
                gui.rePopulateTable(file);
            }
            else //both package and class exist, duplicate DB entry
            {
                Boolean askUser = gui.askUserAboutDuplicate();
                if (askUser == true)
                {
                    db.replaceFileStat(file);
                    gui.setPackageDDSelection(file.getPackageName());
                    gui.setClassDDSelection(file.getClassName());
                    gui.populatePackageDD(file.getClassName());
                    gui.populateClassDD(file.getPackageName());
                    gui.rePopulateTable(file);
                }
            }
            
        }


        public List<String> getAllPackages ()
        {
            List<String> packs = db.getAllPackages();
            packs.Sort();
            return packs;
        }

        public List<String> getAllClasses ()
        {
            List<String> classes = db.getAllClasses();
            classes.Sort();
            return classes;
        }

        public List<String> getPackagesForClass (String cls)
        {
            List<String> packages = db.getPackagesForClass(cls);
            packages.Sort();
            return packages;
        }

        public List<String> getClassesForPackage (String pck)
        {
            List<String> classes = db.getClassesForPackage(pck);
            classes.Sort();
            return classes;
        }


        public List<fileStatistics> getAllStats ()
        {
            List<fileStatistics> allStats = db.getAllFileStats();
            return allStats;
        }

        public List<fileStatistics> getFileStatsForPackage (String pckName)
        {
            List<fileStatistics> stats = db.getFileStatsForPackage(pckName);
            return stats;
        }

        public List<fileStatistics> getFileStatsForClass (String clsName)
        {
            List<fileStatistics> stats = db.getFileStatsForClass(clsName);
            return stats;
        }

        public List<fileStatistics> getFileStatsForPackageClass (String pckName, String clsName)
        {
            List<fileStatistics> stats = db.getFileStatsForPackageClass(pckName, clsName);
            return stats;
        }

    }
}
