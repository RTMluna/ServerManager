using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ServerUserData
{
    public class Program
    {
        class UUIDList
        {
            public List<string> UUIDs;
        }


        static FileSystemWatcher fileSystemWatcher;
        public static void Main(string[] args)
        {
            FileSystemWatcherSetting();
        }

        static void FileSystemWatcherSetting()
        {
            fileSystemWatcher = new FileSystemWatcher("C:\\Users\\midkees\\Downloads\\산업재해안전체험");
            fileSystemWatcher.EnableRaisingEvents = true;
            fileSystemWatcher.IncludeSubdirectories = true;
            fileSystemWatcher.Filter = "*.dat";


            fileSystemWatcher.Created += new FileSystemEventHandler((a, e) =>
            {
                Console.WriteLine($"신규 UUID가 추가되었습니다. {e.Name}");
            });
            new System.Threading.AutoResetEvent(false).WaitOne();
        }

    }
}
