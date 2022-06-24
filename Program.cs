using System;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ServerUserData
{
    public class Program 
    {
        System.IO.FileSystemWatcher fileSystemWatcher;
        public static void Main(string[] args)
        {
            FileSystemWatcherSetting();
        }

        void FileSystemWatcherSetting()
        {
            fileSystemWatcher = new FileSystemWatcher("C:\\Users\\midkees\\Downloads\\산업재해안전체험");
            fileSystemWatcher.EnableRaisingEvents = true;
            fileSystemWatcher.IncludeSubdirectories = true;
            fileSystemWatcher.Filter ="*.*";


            fileSystemWatcher.Created = UserUUIDCreatedEvent;

        }

        void UserUUIDCreatedEvent(object sender, System.IO.FIleSystemEventArgs e)
        {
            Console.WriteLine($"신규 UUID가 추가되었습니다. {e.ChangeType}");
        }
    }
}
