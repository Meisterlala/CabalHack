using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cabal4
{
    internal static class Program
    {
        static public Process CabalMain;
        public static Cheat cheat;
        static public string exeName = "CabalMain";
        public static Main2 GUI;
        static public TextWriterTraceListener ListenerConsole;
        static public TextWriterTraceListener ListenerLog;
        private static Thread cheatThread;
        private static Thread gameDetection;

        static public void StartGameDetection()
        {
            gameDetection = new Thread(new ThreadStart(delegate ()
            {
                while (true)
                {
                    var proc = DetectStart();
                    Program.cheat = new Cheat(proc);
                    cheatThread = new Thread(new ThreadStart(Program.cheat.Start));
                    cheatThread.Name = "Cheat";
                    cheatThread.Start();
                    GUI.BlockDisable();

                    DetectExit();

                    cheatThread.Abort();
                    GUI.BlockEnable();
                    // Make Sure it is closes
                }
            }));
            gameDetection.Name = "GameDetection";
            gameDetection.Start();
        }

        private static void DetectExit()
        {
            if (CabalMain.HasExited)
            {
                throw new Exception("Could'nt find CabalMain.exe, but it should be running");
            }

            while (CabalMain.MainWindowTitle == "CABAL" || CabalMain.MainWindowTitle == "Cabal Hack                              FPSLATINO                               EP XX")
            {
                Thread.Sleep(100);
                CabalMain.Refresh();
                if (CabalMain.HasExited)
                {
                    break;
                }
            }

            Trace.WriteLine("Detected Exit");
        }

        static private Process DetectStart()
        {
            while (true)
            {
                var allProcesses = Process.GetProcessesByName(Program.exeName);
                while (allProcesses.Length <= 0)
                {
                    allProcesses = Process.GetProcessesByName(Program.exeName);
                    Thread.Sleep(100);
                }

                Process newest = allProcesses[0];
                foreach (var item in allProcesses)
                {
                    try
                    {
                        if (item.StartTime >= newest.StartTime)
                        {
                            newest = item;
                        }
                    }
                    catch (Exception) { }
                }
                CabalMain = newest;
                CabalMain.Refresh();

                if (CabalMain.HasExited)
                {
                    continue;
                }

                try
                {
                    IntPtr tryread = CabalMain.MainModule.BaseAddress;
                }
                catch (Exception)
                {
                    continue;
                }

                if (CabalMain.MainWindowTitle == "CABAL" || CabalMain.MainWindowTitle == "Cabal Hack                              FPSLATINO                               EP XX")
                {
                    Trace.WriteLine("Detected Start");
                    return CabalMain;
                }
            }
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            ListenerLog = new TextWriterTraceListener("log.txt", "log");
            ListenerConsole = new TextWriterTraceListener(Console.Out, "Console");
            Debug.Listeners.Clear();
            Debug.Listeners.Add(ListenerLog);
            Debug.Listeners.Add(ListenerConsole);
            Debug.AutoFlush = true;
            Debug.WriteLine("██████████████████████████████████████████ " + DateTime.Now + " ██████████████████████████████████████████");

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            GUI = new Main2();
            Application.Run(GUI);

            // EXIT

            gameDetection?.Abort();
            cheatThread?.Abort();
        }
    }
}