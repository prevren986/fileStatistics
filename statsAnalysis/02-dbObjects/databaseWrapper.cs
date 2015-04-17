using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.InteropServices;
using System.Data;
using System.Data.SQLite;
using statsAnalysis._04_entityObjects;
//using System.Data.SQLite.EF6;
//using System.Data.SQLite.Linq;



namespace statsAnalysis._02_dbObjects
{
    class databaseWrapper
    {

        String dbLocation;

        //Tables
        private const String packageTable = "package_tbl";
        private const String classTable = "class_tbl";
        private const String statsTable = "statistics_tbl";
        //Fields
        private const String packageID = "package_ID";
        private const String classID = "class_ID";
        private const String name = "name";
        private const String uniqueKeywords = "unique_keywords";
        private const String uniqueUDIs = "unique_udis";
        private const String uniqueConstants = "unique_constants";
        private const String uniqueSpecialChars = "unique_special_chars";
        private const String totalKeywords = "total_keywords";
        private const String totalUDIs = "total_udis";
        private const String totalConstants = "total_constants";
        private const String totalSpecialChars = "total_special_chars";
        private const String totalChars = "total_chars";
        private const String totalWhiteSpace = "total_whitespace";
        private const String totalComments = "total_comments";
        private const String percentWhitespace = "percent_whitespace";
        private const String percentComments = "percent_comments";
        private const String filePath = "file_path";
        //SQL statements
        private const String connectionStringPrefix = "Data Source = {0};Version=3;";
        private readonly String createPackageTableStmt = String.Format ("CREATE TABLE '{0}' ("+
            "'{1}' INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE,"+
            "'{2}' VARCHAR(50) NOT NULL UNIQUE)", 
            packageTable, packageID, name);
       
        private readonly String createClassTableStmt = String.Format("CREATE TABLE '{0}' ("+
            "'{1}' INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE,"+
            "'{2}' VARCHAR(50) NOT NULL UNIQUE)",
            classTable, classID, name);
        
        private readonly String createStatsTableStmt = String.Format("CREATE TABLE '{0}' (" +
            "'{1}' INTEGER,"+
            "'{2}' INTEGER,"+
            "'{5}' INTEGER,"+
            "'{6}' INTEGER,"+
            "'{7}' INTEGER,"+
            "'{8}' INTEGER,"+
            "'{9}' INTEGER,"+
            "'{10}' INTEGER,"+
            "'{11}' INTEGER,"+
            "'{12}' INTEGER,"+
            "'{13}' INTEGER,"+
            "'{14}' INTEGER,"+
            "'{15}' INTEGER,"+
            "'{16}' REAL,"+
            "'{17}' REAL,"+
            "'{18}' VARCHAR(255),"+
            "PRIMARY KEY ({1}, {2}),"+
            "FOREIGN KEY({1}) REFERENCES {3}({1})," +
            "FOREIGN KEY({2}) REFERENCES {4}({2}))"
            ,statsTable, packageID, classID, packageTable, classTable, uniqueKeywords,
             uniqueUDIs, uniqueConstants, uniqueSpecialChars, totalKeywords, totalUDIs,
             totalConstants, totalSpecialChars, totalChars, totalWhiteSpace, totalComments, 
             percentWhitespace, percentComments, filePath);

        private const String checkForTablePrefix = "SELECT count (*) FROM sqlite_master WHERE type='table' AND name='{0}'";

        private readonly String insertFileStatPrefix = "INSERT INTO {0} " +
            "({1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, " +
            "{11}, {12}, {13}, {14}, {15}, {16})" +
            " VALUES " +
            "({17}, {18}, {19}, {20}, {21}, {22}, {23}, {24}, {25}, {26}, " +
            "{27}, {28}, {29}, {30}, {31}, \'{32}\')";


        private readonly String updateFileStatPrefix = "UPDATE {0} SET " +
            "{1} = {2}, " + //uniqueKeywords
            "{3} = {4}, " + //uniqueUDIs
            "{5} = {6}, " + //uniqueConstants
            "{7} = {8}, " + //uniqueSpecialChars
            "{9} = {10}, " + //totalKeywords
            "{11} = {12}, " + //totalUDIs
            "{13} = {14}, " + //totalConstants
            "{15} = {16}, " + //totalSpecialChars
            "{17} = {18}, " + //totalChars
            "{19} = {20}, " + //totalWhiteSpace
            "{21} = {22}, " + //totalComments
            "{23} = {24}, " + //percentWhitespace
            "{25} = {26}, " + //percentComments
            "{27} = '{28}' " + //filepath
            "WHERE {29} = {30} " + //packageID condition
            "AND {31} = {32}"; //ClassID condition


        public databaseWrapper(String dbFolder)
        {
            dbLocation = dbFolder + "\\codeAnalyzerDB.sqlite";

            String connectionString = String.Format(connectionStringPrefix, dbLocation);


            if (File.Exists(dbLocation))
            {
                if (!doTablesExist(connectionString)) createTables();
            }
            else
            {
                createDatabase();
                Console.WriteLine("Database successfully created.");
            }

            
        }

        private void createDatabase ()
        {
            SQLiteConnection.CreateFile(dbLocation);
            createTables();

        }

        private void createTables ()
        {
            executeSqlNonQuery(createPackageTableStmt);
            executeSqlNonQuery(String.Format("INSERT INTO sqlite_sequence (name, seq) Values ('{0}', 0)", packageTable));
            executeSqlNonQuery(createClassTableStmt);
            executeSqlNonQuery(String.Format("INSERT INTO sqlite_sequence (name, seq) Values ('{0}', 0)", classTable));
            executeSqlNonQuery(createStatsTableStmt);
            executeSqlNonQuery(String.Format("INSERT INTO sqlite_sequence (name, seq) Values ('{0}', 0)", statsTable));
        }

        private Boolean doTablesExist(String connect)
        {
            int count = 0;
            int tablesMissingFlag = 0;
            String packageTableSql = String.Format(checkForTablePrefix, packageTable);
            String classTableSql = String.Format(checkForTablePrefix, classTable);
            String statsTableSql = String.Format(checkForTablePrefix, statsTable);

            var connection = new SQLiteConnection(connect);
            var packageCommand = new SQLiteCommand(packageTableSql, connection);
            var classCommand = new SQLiteCommand(classTableSql, connection);
            var statsCommand = new SQLiteCommand(statsTableSql, connection);
            
                        
            using (connection)
            {
                connection.Open();
                //check for package table
                using (var reader = packageCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        count = reader.GetInt32(0);
                    }
                }
                if (count == 0) tablesMissingFlag++;
                else count = 0;

                //check for class table
                using (var reader = classCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        count = reader.GetInt32(0);
                    }
                }
                if (count == 0) tablesMissingFlag++;
                else count = 0;

                //check for Statistics table
                using (var reader = statsCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        count = reader.GetInt32(0);
                    }
                }
                if (count == 0) tablesMissingFlag++;

                connection.Close();
            }

            if (tablesMissingFlag == 0) return true;
            else return false;
        }


        private int executeSqlNonQuery (String sql)
        {
            try
            {
                using (var connection = new SQLiteConnection(getConnectionString()))
                {
                    using (var command = new SQLiteCommand(sql, connection))
                    {
                        command.Connection.Open();
                        int rows = command.ExecuteNonQuery();
                        connection.Close();
                        return rows;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Db Wrapper Error in executeSqlNonQuery function:");
                Console.WriteLine(e.ToString());
                return -1;
            }
        }


        private String getConnectionString()
        {
            return String.Format(connectionStringPrefix, dbLocation);
        }


        public Boolean doesPackageExist(String packageName)
        {
            String sql = String.Format("SELECT * FROM {0}", packageTable);
            Console.WriteLine("Sql String: " + sql);

            using (var connection = new SQLiteConnection(getConnectionString()))
            {
                var command = new SQLiteCommand (sql, connection);
                try
                {
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            //Console.WriteLine("Entries: " + reader.GetInt32(0) + ", " + reader.GetString(1));
                            if (reader.GetString(1) == packageName) return true;
                        }
                        connection.Close();
                        return false;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error in doesPackageExist function.\n" + e.ToString());
                    return false;
                }
            }           

        }


        public Boolean doesClassExist(String className)
        {
            String sql = String.Format("SELECT * FROM {0}", classTable);

            Console.WriteLine("Sql String: " + sql);

            using (var connection = new SQLiteConnection(getConnectionString()))
            {
                var command = new SQLiteCommand(sql, connection);
                try
                {
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            //Console.WriteLine("Entries: " + reader.GetInt32(0) + ", " + reader.GetString(1));
                            if (reader.GetString(1) == className) return true;
                        }
                        connection.Close();
                        return false;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error in doesPackageExist function.\n" + e.ToString());
                    return false;
                }
            }           

        }


        public void addPackage (String packageName)
        {
            String sql = String.Format("INSERT INTO {0} (name) VALUES (\"{1}\")", packageTable, packageName);
            Console.WriteLine("Sql String: " + sql);

            executeSqlNonQuery(sql);

        }

        public void addClass (String className)
        {
            String sql = String.Format("INSERT INTO {0} (name) VALUES (\"{1}\")", classTable, className);
            Console.WriteLine("Sql String: " + sql);
            executeSqlNonQuery(sql);
        }


        public void addFileStat (fileStatistics file)
        {
            int foundPackageID = getPackageId(file.getPackageName());
            int foundClassID = getClassId(file.getClassName());

            if (foundPackageID > 0 && foundClassID > 0)
            {
                String sql = String.Format(insertFileStatPrefix,
                    statsTable, packageID, classID, uniqueKeywords, uniqueUDIs, uniqueConstants,
                    uniqueSpecialChars, totalKeywords, totalUDIs, totalConstants, totalSpecialChars, totalChars,
                    totalWhiteSpace, totalComments, percentWhitespace, percentComments, filePath,
                    foundPackageID, foundClassID, file.getUniqueKeywords(), file.getUniqueUdis(), 
                    file.getUniqueConstants(), file.getUniqueSpecialChars(), file.getTotalKeywords(), 
                    file.getTotalUdis(), file.getTotalConstants(), file.getTotalSpecialChars(), file.getTotalChars(), 
                    file.getTotalWhiteSpace(), file.getTotalCommentChars(), file.getPercentWhitespace(), 
                    file.getPercentCommentsChars(), file.getFilepath());

                Console.WriteLine("Sql String: " + sql);
                executeSqlNonQuery(sql);

            }
            else Console.WriteLine("ERROR in addFileStat: Package or Class ID are invalid.");

        }


        public void replaceFileStat (fileStatistics file)
        {
            int foundPackageID = getPackageId(file.getPackageName());
            int foundClassID = getClassId(file.getClassName());

            
                String sql = String.Format(updateFileStatPrefix,
                    statsTable, uniqueKeywords, file.getUniqueKeywords(), 
                    uniqueUDIs, file.getUniqueUdis(),
                    uniqueConstants, file.getUniqueConstants(),
                    uniqueSpecialChars, file.getUniqueSpecialChars(),
                    totalKeywords, file.getTotalKeywords(),
                    totalUDIs, file.getTotalUdis(),
                    totalConstants, file.getTotalConstants(),
                    totalSpecialChars, file.getTotalSpecialChars(),
                    totalChars, file.getTotalChars(),
                    totalWhiteSpace, file.getTotalWhiteSpace(),
                    totalComments, file.getTotalCommentChars(),
                    percentWhitespace, file.getTotalWhiteSpace(),
                    percentComments, file.getPercentCommentsChars(),
                    filePath, file.getFilepath(),
                    packageID, foundPackageID, 
                    classID , foundClassID );

                Console.WriteLine("Sql String: " + sql);
                executeSqlNonQuery(sql);

            
        }


        private int getPackageId (String packageName)
        {
            int foundPackageID = -1;
            String sql = String.Format("SELECT {0} FROM {1} WHERE name=\"{2}\"", packageID, packageTable, packageName);
            Console.WriteLine("Sql String: " + sql);

            using (var connection = new SQLiteConnection(getConnectionString()))
            {
                var command = new SQLiteCommand(sql, connection);
                try
                {
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine("Package ID: " + reader.GetInt32(0));
                            foundPackageID = reader.GetInt32(0);
                        }
                        connection.Close();
                        return foundPackageID;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error in getPackageID function.\n" + e.ToString());
                    return -1;
                }
            } 
        }


        private int getClassId (String className)
        {
            int foundClassID = -1;
            String sql = String.Format("SELECT {0} FROM {1} WHERE name=\"{2}\"", classID, classTable, className);
            Console.WriteLine("Sql String: " + sql);

            using (var connection = new SQLiteConnection(getConnectionString()))
            {
                var command = new SQLiteCommand(sql, connection);
                try
                {
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine("Class ID: " + reader.GetInt32(0));
                            foundClassID = reader.GetInt32(0);
                        }
                        connection.Close();
                        return foundClassID;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error in getClassID functiion.\n" + e.ToString());
                    return -1;
                }

            }
        }


        private String getPackageName (int id)
        {
            String foundName = "";
            String sql = String.Format("SELECT {0} FROM {1} WHERE {2} = {3}", name, packageTable, packageID, id);
            Console.WriteLine("Sql String: " + sql);

            using (var connection = new SQLiteConnection(getConnectionString()))
            {
                var command = new SQLiteCommand(sql, connection);
                try
                {
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine("Package Name: " + reader.GetString(0));
                            foundName = reader.GetString(0);
                        }
                        connection.Close();
                        return foundName;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error in getPackageName functiion.\n" + e.ToString());
                    return null;
                }

            }
        }


        private String getClassName(int id)
        {
            String foundName = "";
            String sql = String.Format("SELECT {0} FROM {1} WHERE {2} = {3}", name, classTable, classID, id);
            Console.WriteLine("Sql String: " + sql);

            using (var connection = new SQLiteConnection(getConnectionString()))
            {
                var command = new SQLiteCommand(sql, connection);
                try
                {
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine("Class Name: " + reader.GetString(0));
                            foundName = reader.GetString(0);
                        }
                        connection.Close();
                        return foundName;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error in getPackageName functiion.\n" + e.ToString());
                    return null;
                }

            }
        }


        public List<fileStatistics> getAllFileStats ()
        {
            List<fileStatistics> results = new List <fileStatistics>();
            String sql = String.Format("SELECT * FROM {0} ORDER BY {1}", statsTable, packageID);
            Console.WriteLine("Sql String: " + sql);

            using (var connection = new SQLiteConnection(getConnectionString()))
            {
                var command = new SQLiteCommand(sql, connection);
                try
                {
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine("Results: " + 
                                reader.GetInt32(0) + " | " + 
                                reader.GetInt32(1) + " | " +
                                reader.GetInt32(2) + " | " +
                                reader.GetInt32(3) + " | " +
                                reader.GetInt32(4) + " | " +
                                reader.GetInt32(5) + " | " +
                                reader.GetInt32(6) + " | " +
                                reader.GetInt32(7) + " | " +
                                reader.GetInt32(8) + " | " +
                                reader.GetInt32(9) + " | " +
                                reader.GetInt32(10) + " | " +
                                reader.GetInt32(11) + " | " +
                                reader.GetInt32(12) + " | " +
                                reader.GetFloat(13) + " | " +
                                reader.GetFloat(14) + " | " +
                                reader.GetString(15));

                            fileStatistics temp = new fileStatistics(
                               getPackageName(reader.GetInt32(0)),
                               getClassName(reader.GetInt32(1)),
                               reader.GetInt32(2),
                               reader.GetInt32(3),
                               reader.GetInt32(4),
                               reader.GetInt32(5),
                               reader.GetInt32(6),
                               reader.GetInt32(7),
                               reader.GetInt32(8),
                               reader.GetInt32(9),
                               reader.GetInt32(10),
                               reader.GetInt32(11),
                               reader.GetInt32(12),
                               reader.GetFloat(13),
                               reader.GetFloat(14),
                               reader.GetString(15));

                            results.Add(temp);
                            
                        }
                        connection.Close();
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error in getAllFileStats functiion.\n" + e.ToString());
                    return null;
                }

            }
            
            return results;
        }


        public List<fileStatistics> getFileStatsForPackage(String pckName)
        {
            List<fileStatistics> results = new List<fileStatistics>();
            int pckID = getPackageId(pckName);

            String sql = String.Format("SELECT * FROM {0} WHERE {1} = {2} ORDER BY {1}", statsTable, packageID, pckID);
            Console.WriteLine("Sql String: " + sql);

            using (var connection = new SQLiteConnection(getConnectionString()))
            {
                var command = new SQLiteCommand(sql, connection);
                try
                {
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine("Results: " +
                                reader.GetInt32(0) + " | " +
                                reader.GetInt32(1) + " | " +
                                reader.GetInt32(2) + " | " +
                                reader.GetInt32(3) + " | " +
                                reader.GetInt32(4) + " | " +
                                reader.GetInt32(5) + " | " +
                                reader.GetInt32(6) + " | " +
                                reader.GetInt32(7) + " | " +
                                reader.GetInt32(8) + " | " +
                                reader.GetInt32(9) + " | " +
                                reader.GetInt32(10) + " | " +
                                reader.GetInt32(11) + " | " +
                                reader.GetInt32(12) + " | " +
                                reader.GetFloat(13) + " | " +
                                reader.GetFloat(14) + " | " +
                                reader.GetString(15));

                            fileStatistics temp = new fileStatistics(
                               getPackageName(reader.GetInt32(0)),
                               getClassName(reader.GetInt32(1)),
                               reader.GetInt32(2),
                               reader.GetInt32(3),
                               reader.GetInt32(4),
                               reader.GetInt32(5),
                               reader.GetInt32(6),
                               reader.GetInt32(7),
                               reader.GetInt32(8),
                               reader.GetInt32(9),
                               reader.GetInt32(10),
                               reader.GetInt32(11),
                               reader.GetInt32(12),
                               reader.GetFloat(13),
                               reader.GetFloat(14),
                               reader.GetString(15));

                            results.Add(temp);

                        }
                        connection.Close();
                        return results;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error in getStatsForPackage functiion.\n" + e.ToString());
                    return null;
                }
            }
        }


        public List<fileStatistics> getFileStatsForClass(String clsName)
        {
            List<fileStatistics> results = new List<fileStatistics>();
            int clsID = getClassId(clsName);

            String sql = String.Format("SELECT * FROM {0} WHERE {1} = {2} ORDER BY {1}", statsTable, classID, clsID);
            Console.WriteLine("Sql String: " + sql);

            using (var connection = new SQLiteConnection(getConnectionString()))
            {
                var command = new SQLiteCommand(sql, connection);
                try
                {
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine("Results: " +
                                reader.GetInt32(0) + " | " +
                                reader.GetInt32(1) + " | " +
                                reader.GetInt32(2) + " | " +
                                reader.GetInt32(3) + " | " +
                                reader.GetInt32(4) + " | " +
                                reader.GetInt32(5) + " | " +
                                reader.GetInt32(6) + " | " +
                                reader.GetInt32(7) + " | " +
                                reader.GetInt32(8) + " | " +
                                reader.GetInt32(9) + " | " +
                                reader.GetInt32(10) + " | " +
                                reader.GetInt32(11) + " | " +
                                reader.GetInt32(12) + " | " +
                                reader.GetFloat(13) + " | " +
                                reader.GetFloat(14) + " | " +
                                reader.GetString(15));

                            fileStatistics temp = new fileStatistics(
                               getPackageName(reader.GetInt32(0)),
                               getClassName(reader.GetInt32(1)),
                               reader.GetInt32(2),
                               reader.GetInt32(3),
                               reader.GetInt32(4),
                               reader.GetInt32(5),
                               reader.GetInt32(6),
                               reader.GetInt32(7),
                               reader.GetInt32(8),
                               reader.GetInt32(9),
                               reader.GetInt32(10),
                               reader.GetInt32(11),
                               reader.GetInt32(12),
                               reader.GetFloat(13),
                               reader.GetFloat(14),
                               reader.GetString(15));

                            results.Add(temp);

                        }
                        connection.Close();
                        return results;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error in getStatsForClass functiion.\n" + e.ToString());
                    return null;
                }
            }
        }

        
        public List<fileStatistics> getFileStatsForPackageClass (String pckName, String clsName)
        {
            List<fileStatistics> results = new List<fileStatistics>();
            int pckID = getPackageId(pckName);
            int clsID = getClassId(clsName);

            String sql = String.Format("SELECT * FROM {0} WHERE {1} = {2} AND {3} = {4} ORDER BY {1}", statsTable, packageID, pckID, classID, clsID);
            Console.WriteLine("Sql String: " + sql);

            using (var connection = new SQLiteConnection(getConnectionString()))
            {
                var command = new SQLiteCommand(sql, connection);
                try
                {
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine("Results: " +
                                reader.GetInt32(0) + " | " +
                                reader.GetInt32(1) + " | " +
                                reader.GetInt32(2) + " | " +
                                reader.GetInt32(3) + " | " +
                                reader.GetInt32(4) + " | " +
                                reader.GetInt32(5) + " | " +
                                reader.GetInt32(6) + " | " +
                                reader.GetInt32(7) + " | " +
                                reader.GetInt32(8) + " | " +
                                reader.GetInt32(9) + " | " +
                                reader.GetInt32(10) + " | " +
                                reader.GetInt32(11) + " | " +
                                reader.GetInt32(12) + " | " +
                                reader.GetFloat(13) + " | " +
                                reader.GetFloat(14) + " | " +
                                reader.GetString(15));

                            fileStatistics temp = new fileStatistics(
                               getPackageName(reader.GetInt32(0)),
                               getClassName(reader.GetInt32(1)),
                               reader.GetInt32(2),
                               reader.GetInt32(3),
                               reader.GetInt32(4),
                               reader.GetInt32(5),
                               reader.GetInt32(6),
                               reader.GetInt32(7),
                               reader.GetInt32(8),
                               reader.GetInt32(9),
                               reader.GetInt32(10),
                               reader.GetInt32(11),
                               reader.GetInt32(12),
                               reader.GetFloat(13),
                               reader.GetFloat(14),
                               reader.GetString(15));

                            results.Add(temp);

                        }
                        connection.Close();
                        return results;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error in getStatsForPackageClass functiion.\n" + e.ToString());
                    return null;
                }
            }
        }
        

        public List<String> getAllPackages()
        {
            List<String> results = new List<String>();

            String sql = String.Format("SELECT {0} FROM {1}", name, packageTable);
            Console.WriteLine("Sql String: " + sql);

            using (var connection = new SQLiteConnection(getConnectionString()))
            {
                var command = new SQLiteCommand(sql, connection);
                try
                {
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine("Package Name: " + reader.GetString(0));
                            results.Add(reader.GetString(0));
                        }
                        connection.Close();
                        return results;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error in getAllPackages functiion.\n" + e.ToString());
                    return null;
                }

            }
        }

        public List<String> getAllClasses()
        {
            List<String> results = new List<String>();

            String sql = String.Format("SELECT {0} FROM {1}", name, classTable);
            Console.WriteLine("Sql String: " + sql);

            using (var connection = new SQLiteConnection(getConnectionString()))
            {
                var command = new SQLiteCommand(sql, connection);
                try
                {
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine("Class Name: " + reader.GetString(0));
                            results.Add(reader.GetString(0));
                        }
                        connection.Close();
                        return results;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error in getAllClasses functiion.\n" + e.ToString());
                    return null;
                }

            }
        }

        
        public List<String> getPackagesForClass(String clsName)
        {
            List<String> results = new List<String>();
            int foundClsID = getClassId(clsName);

            String sql = String.Format("SELECT DISTINCT {0} FROM {1} WHERE {2} = {3}", packageID, statsTable, classID, foundClsID);
            Console.WriteLine("Sql String: " + sql);

            using (var connection = new SQLiteConnection(getConnectionString()))
            {
                var command = new SQLiteCommand(sql, connection);
                try
                {
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine("Package ID: " + reader.GetInt32(0));
                            int id = reader.GetInt32(0);
                            results.Add(getPackageName(id));
                        }
                        connection.Close();
                        return results;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error in getPackagesForClass functiion.\n" + e.ToString());
                    return null;
                }

            }

        }
        
        public List<String> getClassesForPackage(String pckName)
        {
            List<String> results = new List<String>();
            int foundPckID = getPackageId(pckName);

            String sql = String.Format("SELECT DISTINCT {0} FROM {1} WHERE {2} = {3}", classID, statsTable, packageID, foundPckID);
            Console.WriteLine("Sql String: " + sql);

            using (var connection = new SQLiteConnection(getConnectionString()))
            {
                var command = new SQLiteCommand(sql, connection);
                try
                {
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine("Class ID: " + reader.GetInt32(0));
                            int id = reader.GetInt32(0);
                            results.Add(getClassName(id));
                        }
                        connection.Close();
                        return results;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error in getClassesForPackage functiion.\n" + e.ToString());
                    return null;
                }

            }
        }
       


    }


}
