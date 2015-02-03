using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RenameFiles
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Executing this file will rename all the files in its subdirectory...");
            Console.WriteLine("Press Y to continue");
            var key = Console.ReadKey();
            if (key.Key != ConsoleKey.Y)
            {
                return;
            }
            Console.WriteLine("Enter one file extension to be renamed:");
            Console.WriteLine("E.g. MP3, MP4, AVI, or enter * for all types");
            var extension = Console.ReadLine();
            DirectoryInfo directory=new DirectoryInfo(".");
            var subDirectories = directory.GetDirectories();
            RenameFilesInDirectory(subDirectories, "*." + extension);
        }

        private static void RenameFilesInDirectory(DirectoryInfo[] directories, string extension)
        {
            if (directories == null || !directories.Any())
            {
                return;
            }
            foreach (var directory in directories)
            {
                var files = directory.GetFiles(extension).OrderBy(info => info.LastWriteTime);
                int counter = 1;
                foreach (var fileInfo in files)
                {
                    var newFileName = Path.Combine(fileInfo.DirectoryName,
                        directory.Name + " " + counter + fileInfo.Extension);
                    if (File.Exists(newFileName))
                    {
                        newFileName = newFileName + " - Copy";
                    }
                    File.Move(fileInfo.FullName, newFileName);
                    counter++;
                }
                var subDirectories = directory.GetDirectories();
                RenameFilesInDirectory(subDirectories,extension);
            }
        }
    }
}
