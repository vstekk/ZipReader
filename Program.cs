using System;
using System.IO;
using System.IO.Compression;
using System.Linq;

namespace ZipReader
{
    static class Program
    {
        static void Main()
        {
            string sourcePath = @"C:\Users\jakub\Downloads\směrnice ND.zip";
            //string sourcePath = @"..\..\..\data\src.zip";
            
            PrintArchive(sourcePath);
            
            Console.WriteLine();
            
            Menu(sourcePath);

        }

        private static void Menu(string sourcePath)
        {
            
            Console.WriteLine("Press [R] to rename files or [E] to exit");
            
            ConsoleKey response;
            do 
            {
                response = Console.ReadKey(false).Key;
            }   while (response != ConsoleKey.R
                       && response != ConsoleKey.E);
            switch (response)
            {
                case ConsoleKey.R:
                    RenameFilesInArchive(sourcePath);
                    break;
                case ConsoleKey.E:
                    break;
            }
        }

        private static void RenameFilesInArchive(string sourcePath)
        {
            Console.WriteLine();
            Console.WriteLine("Renaming files started.");
            Console.WriteLine();
            
            using (var archive = 
                new ZipArchive(File.Open(sourcePath, FileMode.Open, FileAccess.ReadWrite), ZipArchiveMode.Update))
            {
                var entries = archive.Entries.ToArray();
                foreach (var entry in entries)
                {
                    var oldEntryFullName = entry.FullName;
                    var newEntryFullName = RenameEntry(entry.FullName);
                    var newEntry = archive.CreateEntry(newEntryFullName);
                    using (var a = entry.Open())
                        using (var b = newEntry.Open())
                            a.CopyTo(b);
                    entry.Delete();
                    Console.WriteLine($"originalFile: {oldEntryFullName} | newFile: {newEntryFullName}");
                }
            }
            Console.WriteLine();
            Console.WriteLine("Renaming files completed.");
            PrintArchive(sourcePath);
        }
        private static string RenameEntry(string entryFullName)
        {
            entryFullName = entryFullName
                .Replace("²", "ř")
                .Replace("ƒ", "č")
                .Replace("∞", "ý")
                .Replace("¼", "Č")
                .Replace("à", "ů")
                .Replace("╪", "ě")
                .Replace("τ", "š")
                .Replace("ⁿ", "Ř")
                .Replace("º", "ž")
                .Replace("ª", "Ž")
                .Replace("σ", "ň")
                .Replace("Θ", "Ú")
                .Replace("", "")
                .Replace("", "");
            
                
            return entryFullName;
        }
        private static void PrintArchive(string sourcePath)
        {
            Console.WriteLine();
            Console.WriteLine("Content of the archive:");
            Console.WriteLine();
            using var archive = ZipFile.OpenRead(sourcePath);
            foreach (var entry in archive.Entries)
            {
                var folderName = entry.FullName.Split('/');
                Console.WriteLine(
                    $"folder: {folderName[0]} | name: {entry.Name} | uncompressed size: {entry.Length} bytes");
            }
        }
    }
}