using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Antlr.Runtime;
using Antlr.Runtime.Misc;

namespace statsAnalysis._04_entityObjects
{
    class fileStatistics
    {
        String packageName, className;
        int uniqueKeywords, uniqueUdis, uniqueConstants, uniqueSpecialChars;
        int totalKeywords, totalUdis, totalConstants, totalSpecialChars;
        int totalChars, totalWhiteSpace, totalCommentChars;
        float percentWhiteSpace, percentCommentChars;
        String filepath;

        public fileStatistics (String file)
        {
            filepath = file;
            //for testing only
            //setTestValues();
            setStatValues();

        }

        //Constructor Overload for DB Wrapper to use
        public fileStatistics (String pckName, String clsName,
            int uKeywords, int uUdis, int uConstants, int uSpecialChars,
            int tKeywords, int tUdis, int tConstants, int tSpecialChars,
            int tChars, int tWhiteSpace, int tComments,
            float perWhiteSpace, float perComments, String file)
        {
            packageName = pckName;
            className = clsName;
            
            uniqueKeywords = uKeywords;
            uniqueUdis = uUdis;
            uniqueConstants = uConstants;
            uniqueSpecialChars = uSpecialChars;

            totalKeywords = tKeywords;
            totalUdis = tUdis;
            totalConstants = tConstants;
            totalSpecialChars = tSpecialChars;
            totalChars = tChars;
            totalWhiteSpace = tWhiteSpace;
            totalCommentChars = tComments;

            percentWhiteSpace = perWhiteSpace;
            percentCommentChars = perComments;
            filepath = file;
        }

        public String getPackageName() { return packageName; }
        public String getClassName() { return className; }
        public int getUniqueKeywords() { return uniqueKeywords; }
        public int getUniqueUdis() { return uniqueUdis; }
        public int getUniqueConstants() { return uniqueConstants; }
        public int getUniqueSpecialChars() { return uniqueSpecialChars; }
        public int getTotalKeywords() { return totalKeywords; }
        public int getTotalUdis() { return totalUdis; }
        public int getTotalConstants() { return totalConstants; }
        public int getTotalSpecialChars() { return totalSpecialChars; }
        public int getTotalChars() { return totalChars; }
        public int getTotalWhiteSpace() { return totalWhiteSpace; }
        public int getTotalCommentChars() { return totalCommentChars; }
        public float getPercentWhitespace() { return percentWhiteSpace; }
        public float getPercentCommentsChars() { return percentCommentChars; }
        public String getFilepath() { return filepath; }

        

        private void setStatValues()
        {
            if (File.Exists(filepath))
            {
                
                Stream inputStream = Console.OpenStandardInput();
                ANTLRFileStream input = new ANTLRFileStream(filepath);
                JavaLexer lexer = new JavaLexer(input);
                CommonTokenStream tokens = new CommonTokenStream(lexer);
                JavaParser parser = new JavaParser(tokens);
                parser.compilationUnit();
                

                //assign parser variables to actual variables
                String textRead = File.ReadAllText(filepath);
                totalChars = textRead.Length;

                className = parser.className;
                packageName = parser.packageName;
                totalKeywords = parser.totalKeywords;
                totalUdis = parser.totalUdis - parser.UdisToRemove;
                totalConstants = parser.totalConstants;
                totalSpecialChars = parser.totalSpecialChars;
                totalWhiteSpace = lexer.totalWhiteSpace;

                uniqueKeywords = parser.uniqueKeywordSet.Count;
                uniqueUdis = parser.uniqueUdiSet.Count;
                uniqueConstants = parser.uniqueConstantsSet.Count;
                uniqueSpecialChars = parser.uniqueSpecialCharSet.Count;

                //calculated values
                percentWhiteSpace = (float) totalWhiteSpace / totalChars * 100;
                
                int commentChars = 0;
                for (int k = 0; k < lexer.commentsSet.Count; k++ )
                {
                    String com = lexer.commentsSet.ElementAt(k).ToString();
                    commentChars += com.Length;
                    Console.WriteLine("Comment Block Size: " + com.Length);
                    Console.WriteLine("Comment Block Contents\n" + com);
                    Console.WriteLine("---------Comment Block End ------------");

                    

                }
                totalCommentChars = commentChars;

                percentCommentChars = (float)totalCommentChars / totalChars * 100;

 
                //FOR TESTING
                Console.WriteLine("Package Name: " + parser.packageName);
                Console.WriteLine("Class Name: " + parser.className);
                Console.WriteLine("Total Keywords: " + parser.totalKeywords);
                Console.WriteLine("Total UDIs: " + (parser.totalUdis - parser.UdisToRemove));
                Console.WriteLine("Total Constants: " + parser.totalConstants);
                Console.WriteLine("Total Special Chars: " + parser.totalSpecialChars);
                Console.WriteLine("Total White Space: " + lexer.totalWhiteSpace);

                Console.WriteLine("Unique Keywords size: " + parser.uniqueKeywordSet.Count);
                Console.WriteLine("****** Unique Keyword Contents *********");
                for (int i=0; i<parser.uniqueKeywordSet.Count; i++)
                {
                    Console.WriteLine(parser.uniqueKeywordSet.ElementAt(i));
                }

                Console.WriteLine("Unique UDI size: " + (parser.uniqueUdiSet.Count - parser.udisToRemoveSet.Count));
                Console.WriteLine("****** Unique UDIS Contents *********");
                for (int i = 0; i < parser.uniqueUdiSet.Count; i++)
                {
                    Console.WriteLine(parser.uniqueUdiSet.ElementAt(i));
                }


                Console.WriteLine("Unique Constant size: " + parser.uniqueConstantsSet.Count);
                Console.WriteLine("****** Unique Constants Contents *********");
                for (int i = 0; i < parser.uniqueConstantsSet.Count; i++)
                {
                    Console.WriteLine(parser.uniqueConstantsSet.ElementAt(i));
                }


                Console.WriteLine("Unique Special Chars size: " + parser.uniqueSpecialCharSet.Count);
                Console.WriteLine("******* Unique Special Char Contents ********");
                for (int j=0; j<parser.uniqueSpecialCharSet.Count; j++)
                {
                    Console.WriteLine(parser.uniqueSpecialCharSet.ElementAt(j));
                }

                Console.WriteLine("Comments Size: " + lexer.commentsSet.Count);
                Console.WriteLine("*********** Comments Contents ************");
                for (int j = 0; j < lexer.commentsSet.Count; j++)
                {
                    Console.WriteLine(lexer.commentsSet.ElementAt(j));
                }

                Console.WriteLine("ALL UDIS FOUND");
                for (int k = 0; k<parser.everyUdi.Count; k++)
                {
                    Console.WriteLine(parser.everyUdi.ElementAt(k));
                }
                
            }
        }



    }
}
