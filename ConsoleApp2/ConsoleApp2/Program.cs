using System;
using System.Diagnostics;
using System.IO;
using System.Timers;
using System.Threading;

namespace RunProcess
{
    internal class Program
    {
        static System.Timers.Timer aTimer;
        static System.Timers.Timer bTimer;
        static Process mainProcess;
        static Thread thread = null;
        static string[] dir;
        private static void Main(string[] args)
        {
            if (dir == null)
                dir = args;

            #region 서버와 같이 실행
            try
            {
                mainProcess = new Process //새 프로세스 생성후 설정
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = @"D:\Lunaserver\log4j\Server.bat", //경로지정
                        UseShellExecute = false, //몰?루 false는 필수해야 RedirectStandardInput이 예외처리 된다함
                        RedirectStandardOutput = true, // 출력 가능 불가능
                        RedirectStandardInput = true, //입력가능 불가능
                        CreateNoWindow = false// 백그라운드 실행 할꺼냐 안할꺼냐 false가 안할꺼다
                    }
                };

                mainProcess.Start(); //프로세스 실행
                SetTimer(); // 타이머 셋팅
                thread = new Thread(CreateLine);
                thread.Start();

            }
            catch (Exception e) // 오류 발견
            {
                Console.WriteLine(e.Message);
            }

            void CreateLine()
            {
                while (true)
                {
                    string line = mainProcess.StandardOutput.ReadLine();
                    if (line.Contains("Can't write connected train to nbt. ")) //ㅇㅕㄹㅊㅏ ㅇㅕㅂㅜ ㅎㅗㅏㄱㅇㅣㄴ
                    {

                        string[] splittemp = line.Split(" ");//13
                        mainProcess.StandardInput.WriteLine(string.Format("broadcast {0}에 제거되지 않은 열차가 있습니다!", splittemp[13]));
                        Console.WriteLine(string.Format("broadcast {0}에 제거되지 않은 열차가 있습니다!", splittemp[13]));
                        continue;
                    }
                    Console.WriteLine(line);
                }
            }

            void SetTimer()
            {
                aTimer = new System.Timers.Timer(60 * 1000 * 1000 * 6);
                aTimer.Elapsed += new ElapsedEventHandler(timer_Elapsed);
                //aTimer.Interval = 60 * 60 * 1000 * 12; //12시간마다
                aTimer.AutoReset = true;
                aTimer.Enabled = true;
                aTimer.Start();
            }
        }

        static int sec = 60;

        static void second(object sender, ElapsedEventArgs e)
        {
            if (sec == 0)
            {
                mainProcess.StandardInput.WriteLine("stop 6시간마다 서버 리붓");
                bTimer.Stop();
                aTimer.Start();
                sec = 120;
                return;
            }
            mainProcess.StandardInput.WriteLine(string.Format("broadcast {0} 초 뒤에 서버가 리붓 됩니다. 진행중인 작업을 멈춰주세요!", sec));
            sec--;
        }

        private static void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            aTimer.Stop();
            bTimer = new System.Timers.Timer(1000);
            bTimer.AutoReset = true;
            bTimer.Elapsed += new ElapsedEventHandler(second);
            bTimer.Enabled = true;
            bTimer.Start();
        }
    }
}
#endregion