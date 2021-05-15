using System;
using System.IO.Compression;

namespace ZipReader
{
    class Program
    {
        static void Main(string[] args)
        {
            string sourcePath = @"C:\Users\jakub\RiderProjects\AppNow\ZipReader\data\src.zip";
            sourcePath = @"..\..\..\data\src.zip";
            using (var archive = ZipFile.OpenRead(sourcePath))
            {
                foreach (var entry in archive.Entries)
                {
                    var folderName = entry.FullName.Split('/');
                    Console.WriteLine($"folder: {folderName[0]} | name: {entry.Name} | uncompressed size: {entry.Length} bytes");
                }
            }

            Console.WriteLine();
            Console.WriteLine("Press 'E' to exit");
            while (Console.ReadKey().Key != ConsoleKey.E) {}
        }
    }
}