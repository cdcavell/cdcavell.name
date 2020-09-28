using CDCavell.ClassLibrary.Commons;
using CDCavell.ClassLibrary.Commons.Xml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace XmlToMarkdown
{
    /// <summary>
    /// Command console program to transform all xml files in current directory utilizing supplied 
    /// template file to Wiki Markdown files.
    /// 
    /// Usage: dotnet XmlToMarkdown.dll [Template file]
    /// </summary>
    /// <revision>
    /// __Revisions:__~~
    /// | Contributor | Build | Revison Date | Description |~
    /// |-------------|-------|--------------|-------------|~
    /// | Christopher D. Cavell | 1.0.0 | 07/05/2020 | Initial build |~ 
    /// </revision>
    class Program
    {
        private static List<KeyValuePair<string, string>> kvpList = new List<KeyValuePair<string, string>>();

        /// <summary>
        /// Entry point method
        /// </summary>
        /// <param name="args">string[]</param>
        /// <method>Main(string[] args)</method>
        static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.WriteLine("Usage: dotnet XmlToMarkdown.dll [Template file]");
                return;
            }

            char[] anyOf = { '\\', '/' };
            string templateFile = args[0];

            Console.WriteLine(string.Empty);
            Console.WriteLine(" Process started");
            Console.WriteLine(string.Empty);

            try
            {
                ClearWiki();

                foreach (string file in Directory.EnumerateFiles(Directory.GetCurrentDirectory()))
                {
                    int extIndex = file.LastIndexOf(".xml", StringComparison.CurrentCultureIgnoreCase);
                    int slashIndex = file.LastIndexOfAny(anyOf);

                    if (extIndex > 0)
                    {
                        string fileNameOnly = file.Substring(slashIndex + 1);
                        fileNameOnly = fileNameOnly.Substring(0, fileNameOnly.LastIndexOf(".xml", StringComparison.CurrentCultureIgnoreCase));
                        Console.Write(" Processing " + fileNameOnly + ".xml");

                        string outputFile = Directory.GetCurrentDirectory() + "/wiki/" + fileNameOnly + ".md";
                        if (File.Exists(outputFile))
                            File.Delete(outputFile);

                        Transform.Write(Directory.GetCurrentDirectory() + "/" + templateFile, file, outputFile);

                        kvpList.Add(new KeyValuePair<string, string>(fileNameOnly, fileNameOnly));
                        Console.WriteLine(" --> Generated " + fileNameOnly + ".md");
                    }
                }

                if (kvpList.Count > 0)
                    WriteHome();

                Console.WriteLine(string.Empty);
                Console.WriteLine(" Process completed");
                Console.WriteLine(string.Empty);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private static void WriteHome()
        {
            Console.WriteLine(" Generating Home.md");
            string homeFile = Directory.GetCurrentDirectory() + "/wiki/Home.md";
            if (File.Exists(homeFile))
                File.Delete(homeFile);

            StringBuilder sb = new StringBuilder();
            sb.Append(AsciiCodes.CRLF);
            sb.Append("| Project Source Code Documentation |");
            sb.Append(AsciiCodes.CRLF);
            sb.Append("|-----------------------------------|");
            sb.Append(AsciiCodes.CRLF);

            kvpList.Sort((x, y) => x.Key.CompareTo(y.Key));
            foreach (KeyValuePair<string, string> kvp in kvpList)
            {
                sb.Append("| [" + kvp.Key + "](" + kvp.Value + ") |");
                sb.Append(AsciiCodes.CRLF);
            }

            File.WriteAllText(homeFile, sb.ToString());

            Console.WriteLine(" Generated Home.md");
        }

        private static void ClearWiki()
        {
            DirectoryInfo di = new DirectoryInfo(Directory.GetCurrentDirectory() + "/wiki/");
            foreach (FileInfo file in di.EnumerateFiles())
            {
                if (file.FullName.Contains(".md"))
                {
                    Console.WriteLine(" Removing " + file.FullName);
                    file.Delete();
                }
            }
        }
    }
}
