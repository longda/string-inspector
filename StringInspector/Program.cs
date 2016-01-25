using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;
using StringInspector.Core.Helpers;
using StringInspector.Core.Models;

namespace StringInspector
{
    class Program
    {
        private static List<string> files = new List<string>();
        private static List<Article> articles = new List<Article>();
        private static StringHelperOptions options = new StringHelperOptions() { IgnoreSpaces = true };

        static void Main(string[] args)
        {
            Run();
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }

        private static void Run()
        {
            Init();
            ProcessFiles();
        }

        private static void Init()
        {
            // Pull in the list of files to check.  This can come from a file, database, api, message queue, etc.
            // For this example, we'll just load the current list from the file system.

            files = JsonConvert.DeserializeObject<List<string>>(File.ReadAllText("files.json"));             
        }

        private static void ProcessFiles()
        {
            // Load in file meta data in preparation for the calculation step.

            Parallel.ForEach(files, s => Process(s));
        }

        private static void Print(string s)
        {
            Console.WriteLine(s);
        }

        private static void Process(string path)
        {
            // Load file contents
            // Calculate most common character
            // Save result
            var a = new Article() { Path = path };
            a.Content = File.ReadAllText(path);

            var ai = StringHelper.CalculateMostCommonCharacter(a, options);
            ai.Article = a;

            // Could do many things here... send off to a storage service, put it on a queue for further processing, etc.
            // We'll keep it simple and serialize this out to a simple json file.
            File.WriteAllText(string.Format("article-info-{0}", path), JsonConvert.SerializeObject(ai));
        }
    }
}
