using System;
using System.Diagnostics;
using System.Timers;

namespace RunProcess
{
    internal class Program
    {
        private static void Main(string[] args)
        {

            try
            {
                var process = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = @"D:\Lunaserver\log4j\Server.bat",
                        UseShellExecute = false, 
                        RedirectStandardOutput = true,
                        CreateNoWindow = false
                    }
                };

                process.Start();
                int tpsCount = 0;
                while(!process.StandardOutput.EndOfStream)
                {
                    var line = process.StandardOutput.ReadLine();
                    Console.WriteLine(line);
                    if(line.Contains("/테스트 입갤"))
                        tpsCount++;
                    else if(tpsCount > 3)
                    {
                        Console.WriteLine("테스트 입갤함");
                    }
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
