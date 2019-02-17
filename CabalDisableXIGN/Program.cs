using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;

using System.Runtime.InteropServices;
using System.ComponentModel;

namespace CabalDisableXIGN
{
    internal class Program
    {
        #region Fields

        private readonly static string cabalWindowName = "CABAL";
        private readonly static string exeNameCabal = "CabalMain";
        private readonly static string exeNameXIGN = "xcoronahost.xem";
        private readonly static string modulXIGN = "xcorona.xem";
        private readonly static int xcoronaOffset = 1135869; //0x1154FD
        private static Process CabalMain;

        #endregion Fields

        #region Enums

        [Flags]
        public enum ThreadAccess : int
        {
            Terminate = 0x0001,
            SuspendResume = 0x0002,
            GetContext = 0x0008,
            SetContext = 0x0010,
            SetInformation = 0x0020,
            QueryInformation = 0x0040,
            SetThreadToken = 0x0080,
            Impersonate = 0x0100,
            DirectImpersonation = 0x0200
        }

        public enum ThreadInfoClass : int
        {
            ThreadQuerySetWin32StartAddress = 9
        }

        #endregion Enums

        #region Imports

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool CloseHandle(IntPtr hObject);

        [DllImport("ntdll.dll", SetLastError = true)]
        private static extern int NtQueryInformationThread(
    IntPtr threadHandle,
    ThreadInfoClass threadInformationClass,
    IntPtr threadInformation,
    int threadInformationLength,
    IntPtr returnLengthPtr);

        [System.Runtime.InteropServices.DllImport("kernel32.dll")]
        private static extern IntPtr OpenThread(uint dwDesiredAccess, bool bInheritHandle, uint dwThreadId);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr OpenThread(ThreadAccess dwDesiredAccess, bool bInheritHandle, int dwThreadId);

        [System.Runtime.InteropServices.DllImport("kernel32.dll")]
        private static extern bool TerminateThread(IntPtr hThread, uint dwExitCode);

        #endregion Imports

        #region ExternalFunktions

        private static IntPtr GetThreadStartAddress(int threadId)
        {
            var hThread = OpenThread(ThreadAccess.QueryInformation, false, threadId);
            if (hThread == IntPtr.Zero)
                throw new Win32Exception();
            var buf = Marshal.AllocHGlobal(IntPtr.Size);
            try
            {
                var result = NtQueryInformationThread(hThread,
                                 ThreadInfoClass.ThreadQuerySetWin32StartAddress,
                                 buf, IntPtr.Size, IntPtr.Zero);
                if (result != 0)
                    throw new Win32Exception(string.Format("NtQueryInformationThread failed; NTSTATUS = {0:X8}", result));
                return Marshal.ReadIntPtr(buf);
            }
            finally
            {
                CloseHandle(hThread);
                Marshal.FreeHGlobal(buf);
            }
        }

        #endregion ExternalFunktions

        private static IntPtr CalculateOffset(IntPtr ThreadAdress)
        {
            int modulePtr = 0;

            for (int j = 0; j < CabalMain.Modules.Count; j++)
            {
                if (CabalMain.Modules[j].ModuleName == modulXIGN)
                {
                    modulePtr = CabalMain.Modules[j].BaseAddress.ToInt32();

                    break;
                }
            }

            return IntPtr.Subtract(ThreadAdress, modulePtr);
        }

        private static void CheckIfAlreadyRunning()
        {
            var allProcesses = Process.GetProcessesByName(exeNameCabal);

            if (allProcesses.Length > 0)
            {
                CabalMain = allProcesses[0];

                if (!(CabalMain.MainWindowTitle == "CABAL"))
                {
                    return;
                }

                Console.WriteLine("Cabal is alredy running, closing Cabal");
                Console.WriteLine("Press any key to abort");
                Thread.Sleep(3000);

                if (Console.KeyAvailable)
                {
                    Environment.Exit(1);
                }

                try
                {
                    CabalMain.Kill();
                }
                catch (Exception e)
                {
                    Console.WriteLine("ERROR: " + e.Message);
                    Console.WriteLine("Could not close Cabal. You may have to reboot your PC if you continue seeing this message.");
                    Console.WriteLine("Press any key to exit");

                    Console.ReadKey(true);
                    Environment.Exit(1);
                }

                Thread.Sleep(1000);
                CheckIfAlreadyRunning();
            }
            else
            {
                return;
            }
        }

        private static bool DetachCabalMainFromXIGN()
        {
            SearchCabalMain();

            var allThreads = CabalMain.Threads;
            List<int> detachThreads = new List<int>();

            Console.WriteLine(string.Format("Threads of {0}: ", exeNameCabal, allThreads.Count));
            Console.WriteLine(string.Format("TID \t|  Starting Adress \t| Offsets", exeNameCabal));
            for (int i = 0; i < allThreads.Count; i++)
            {
                var startAdress = GetThreadStartAddress(allThreads[i].Id);
                var ID = allThreads[i].Id;

                var off = CalculateOffset(startAdress);

                if (off.ToInt32() <= 0)
                {
                    continue;
                }

                if (xcoronaOffset == off.ToInt32())
                {
                    detachThreads.Add(ID);
                }

                Console.WriteLine(string.Format("{0}\t|  {1} \t\t| {2}", ID, startAdress, off.ToInt32()));
            }

            if (detachThreads.Count > 2)
            {
                Thread.Sleep(1000);
                DetachCabalMainFromXIGN();
            }

            Console.WriteLine(string.Format("Number of Stopped Threads: {0}", detachThreads.Count));
            foreach (var threadID in detachThreads)
            {
                var pointer = OpenThread(ThreadAccess.Terminate, false, threadID);
                TerminateThread(pointer, 0);
                Console.WriteLine(string.Format("Terminated TID: {0}", threadID));
            }

            if (detachThreads.Count > 0)
            {
                return true;
            }

            return false;
        }

        private static void Main(string[] args)
        {
            CheckIfAlreadyRunning();

            Console.Write("Waiting for Cabal");

            while (!SearchCabalMain()) { Thread.Sleep(500); Console.Write("."); };

            Console.WriteLine();
            Thread.Sleep(2000);

            while (!DetachCabalMainFromXIGN()) { Thread.Sleep(200); }

            while (!StopXIGN()) { Thread.Sleep(200); }

            Console.WriteLine("Done");
            Thread.Sleep(5000);
            Environment.Exit(0);
        }

        private static bool SearchCabalMain()
        {
            var allProcesses = Process.GetProcessesByName(exeNameCabal);

            if (allProcesses.Length > 0)
            {
                Process newest = allProcesses[0];
                foreach (var item in allProcesses)
                {
                    if (item.StartTime >= newest.StartTime)
                    {
                        newest = item;
                    }
                }

                CabalMain = newest;

                var allThreads = CabalMain.Threads;
                if (allThreads.Count == 0)
                {
                    return false;
                }

                return true;
            }

            return false;
        }

        private static bool StopXIGN()
        {
            Process XIGN = null;

            do
            {
                var allProcesses = Process.GetProcesses();
                foreach (var process in allProcesses)
                {
                    if (process.ProcessName == exeNameXIGN)
                    {
                        XIGN = process;
                        break;
                    }
                }
                Thread.Sleep(100);
            } while (XIGN == null);

            Console.WriteLine(string.Format("-- Terminating {0} --", exeNameXIGN));

            Console.Write("Wating for Cabal to open");

            do
            {
                Thread.Sleep(100);
                SearchCabalMain();

                Console.Write(".");
            } while (CabalMain.MainWindowTitle != cabalWindowName);

            Console.WriteLine();
            XIGN.Kill();

            Console.WriteLine(string.Format("Name: {0}\tPID: {1}", XIGN.ProcessName, XIGN.Id));
            return true;
        }
    }
}