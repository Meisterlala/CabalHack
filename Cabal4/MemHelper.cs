using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Cabal4
{
    public class MemHelper
    {
        private Process gameProcess = null;
        private int pid;
        private IntPtr processHandle;

        public MemHelper()
        {
        }

        #region imports

        [Flags]
        public enum ProcessAccessFlags : uint
        {
            All = 0x001F0FFF,
            Terminate = 0x00000001,
            CreateThread = 0x00000002,
            VMOperation = 0x00000008,
            VMRead = 0x00000010,
            VMWrite = 0x00000020,
            DupHandle = 0x00000040,
            SetInformation = 0x00000200,
            QueryInformation = 0x00000400,
            Synchronize = 0x00100000
        }

        [DllImport("kernel32.dll")]
        public static extern IntPtr OpenProcess(ProcessAccessFlags dwDesiredAccess, bool bInheritHandle, int dwProcessId);

        [DllImport("kernel32.dll")]
        public static extern bool ReadProcessMemory(IntPtr hProcess, int lpBaseAddress, byte[] lpBuffer, int dwSize, ref int lpNumberOfBytesRead);

        /*
        public T ByteArrayToObject<T>(byte[] data) where T : struct
        {
            IFormatter br = new BinaryFormatter();

            using (MemoryStream ms = new MemoryStream(data))
            {
                return (T)br.Deserialize(ms);
            }
        }
        */

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool WriteProcessMemory(IntPtr hProcess, int lpBaseAddress, byte[] lpBuffer, int dwSize, ref int lpNumberOfBytesWritten);

        private byte[] ObjectToByteArray(Object obj)
        {
            if (obj == null)
                return null;

            BinaryFormatter bf = new BinaryFormatter();
            MemoryStream ms = new MemoryStream();
            bf.Serialize(ms, obj);

            return ms.ToArray();
        }

        #endregion imports

        public int FindPattern(byte[] pszPatt, string mask)
        {
            int num = 0;

            int num2 = mask.Length - 1;

            for (int index = PatternSearchArea.lowerBound; index < PatternSearchArea.upperBound; index++)
            {
                var read = ReadMemoryByte(index);

                if (read == pszPatt[num] || mask[num] == '?')
                {
                    if (mask.Length <= num + 1)
                    {
                        return index - num2;
                    }
                    num += 1;
                }
                else
                {
                    num = 0;
                }
            }

            return 0;
        }

        public int FindPattern(byte[] pszPatt, string mask, byte[] toSearch)
        {
            int num = 0;

            int num2 = mask.Length - 1;

            for (int index = 0; index < toSearch.Length; index++)
            {
                var read = toSearch[index];

                if (read == pszPatt[num] || mask[num] == '?')
                {
                    if (mask.Length <= num + 1)
                    {
                        return index - num2;
                    }
                    num += 1;
                }
                else
                {
                    num = 0;
                }
            }

#if DEBUG
            BindingFlags flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static;

            var list = new List<Tuple<string, int>>();
            foreach (FieldInfo prop in typeof(Pattern).GetFields(flags))
            {
                if ((byte[])prop.GetValue(null) == pszPatt)
                {
                    Debug.WriteLine("Could not find pattern for: " + prop.Name);
                }
            }
#endif

            return 0;
        }

        public int GetProcess(String s)
        {
            do
            {
                try
                {
                    gameProcess = Process.GetProcessesByName(s)[0];
                }
                catch (Exception)
                {
                    gameProcess = null;
                }
            } while (gameProcess == null);

            // processHandle = OpenProcess(0x1F0FFF, false, gameProcess.Id);
            processHandle = OpenProcess(ProcessAccessFlags.All, false, gameProcess.Id);
            pid = gameProcess.Id;
            return (int)gameProcess.MainModule.BaseAddress;
        }

        public int LoadProcess(Process p)
        {
            gameProcess = p;
            processHandle = OpenProcess(ProcessAccessFlags.All, false, gameProcess.Id);
            pid = gameProcess.Id;
            return (int)gameProcess.MainModule.BaseAddress;
        }

        public byte[] ReadMemoryBuffer(int address, int length)
        {
            int bytesRead = 0;
            byte[] buffer = new byte[length];

            ReadProcessMemory(processHandle, address, buffer, buffer.Length, ref bytesRead);

            return buffer;
        }

        public byte ReadMemoryByte(int address)
        {
            var b = ReadMemory<Byte>(address);
            return b[0];
        }

        public float ReadMemoryFloat(int address)
        {
            var b = ReadMemory<Int32>(address);
            return ByteArrayToObjectFloat(b);
        }

        public int ReadMemoryInt(int address)
        {
            var b = ReadMemory<Int32>(address);
            return ByteArrayToObjectInt(b);
        }

        public string ReadMemoryString(int address, int size)
        {
            int bytesRead = 0;
            byte[] buffer = new byte[size * sizeof(Char)];

            ReadProcessMemory(processHandle, address, buffer, buffer.Length, ref bytesRead);

            return ByteArrayToObjectString(buffer);
        }

        public int ResovePointer(params int[] offsets)
        {
            int currentPtr = 0;

            for (int i = 0; i < offsets.Length; i++)
            {
                if (i + 1 == offsets.Length && i != 0)
                {
                    return currentPtr + offsets[i];
                }

                currentPtr = ReadMemoryInt(currentPtr + offsets[i]);
            }

            return currentPtr;
        }

        public void WriteMemory(int address, int value)
        {
            int bytesRead = 0;
            byte[] buffer = BitConverter.GetBytes(value);
            WriteProcessMemory(processHandle, address, buffer, buffer.Length, ref bytesRead);
            return;
        }

        public void WriteMemory(int address, byte value)
        {
            int bytesRead = 0;
            byte[] buffer = new byte[] { value };
            WriteProcessMemory(processHandle, address, buffer, buffer.Length, ref bytesRead);
            return;
        }

        public void WriteMemory(int address, byte[] value)
        {
            int bytesRead = 0;
            var x = value.Length;
            WriteProcessMemory(processHandle, address, value, value.Length, ref bytesRead);
            return;
        }

        public void WriteMemory(int address, float value)
        {
            int bytesRead = 0;
            byte[] buffer = BitConverter.GetBytes(value);
            WriteProcessMemory(processHandle, address, buffer, buffer.Length, ref bytesRead);
            return;
        }

        public void WriteMemory(int address, bool value)
        {
            int bytesRead = 0;
            byte[] buffer = BitConverter.GetBytes(value);
            WriteProcessMemory(processHandle, address, buffer, buffer.Length, ref bytesRead);
            return;
        }

        internal void WriteMemoryDirect(int address, byte[] buffer, int length)
        {
            int bytesRead = 0;
            WriteProcessMemory(processHandle, address, buffer, length, ref bytesRead);
            return;
        }

        private float ByteArrayToObjectFloat(byte[] b)
        {
            return BitConverter.ToSingle(b, 0);
        }

        private int ByteArrayToObjectInt(byte[] b)
        {
            return BitConverter.ToInt32(b, 0);
        }

        private string ByteArrayToObjectString(byte[] b)
        {
            return System.Text.Encoding.UTF8.GetString(b);
        }

        private byte[] ReadMemory<T>(int address) where T : struct
        {
            int bytesRead = 0;
            byte[] buffer = new byte[Marshal.SizeOf(typeof(T))];

            ReadProcessMemory(processHandle, address, buffer, buffer.Length, ref bytesRead);

            return buffer;
        }
    }
}