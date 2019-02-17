using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Cabal4
{
    internal static class DebugOutput
    {
        static public void DumpAdresses(Adresses a)
        {
#if DEBUG
            Debug.Listeners.Remove(Program.ListenerConsole);
            BindingFlags flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance | BindingFlags.DeclaredOnly;

            Debug.WriteLine("All Adresses");

            Debug.Indent();
            foreach (FieldInfo prop in typeof(Adresses).GetFields(flags))
            {
                Debug.WriteLine(prop.Name + " = 0x" + ((int)prop.GetValue(a)).ToString("X4"), "Adresses");
            }
            Debug.Unindent();
            Debug.Listeners.Add(Program.ListenerConsole);
#endif
        }

        static public void GenerateCheatTable(Adresses a)
        {
#if DEBUG
            Tuple<string, string, int, bool>[] info = new Tuple<string, string, int, bool>[]
            {
                new Tuple<string, string ,int, bool>("bypassHackDetection", "Array of byte", 6, true ),
                new Tuple<string, string ,int, bool>("wallHackDetection", "Array of byte", 6, true),
                new Tuple<string, string ,int, bool>("wallHack", "Array of byte", 200, true),
                new Tuple<string, string ,int, bool>("wallBase", "Array of byte", 200, true),
                new Tuple<string, string ,int, bool>("pickupRange1", "Array of byte", 3, false),
                new Tuple<string, string ,int, bool>("pickupRange2", "Array of byte", 3, false),

                new Tuple<string, string ,int, bool>("Base", "4 Bytes", 0, true),
                new Tuple<string, string ,int, bool>("baseCam", "4 Bytes", 0, true),
                 new Tuple<string, string ,int, bool>("wallBase", "4 Bytes", 0, true),

                new Tuple<string, string ,int, bool>("id", "String", 99, true),

                new Tuple<string, string ,int, bool>("x", "Float", 0, false),
                new Tuple<string, string ,int, bool>("y", "Float", 0, false),
            };

            Debug.Listeners.Remove(Program.ListenerConsole);
            Debug.Listeners.Remove(Program.ListenerLog);
            Debug.Listeners.Add(new TextWriterTraceListener(new System.IO.StreamWriter("table.ct", false).BaseStream, "CheatTable"));

            BindingFlags flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance | BindingFlags.DeclaredOnly;

            int i = 0;
            Debug.WriteLine("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
            Debug.WriteLine("<CheatTable CheatEngineTableVersion=\"28\">");
            Debug.Indent();
            Debug.WriteLine("<CheatEntries>");
            Debug.Indent();
            Debug.WriteLine("<CheatEntry>");
            Debug.Indent();
            Debug.WriteLine("<ID>" + i + "</ID>"); i++;
            Debug.WriteLine("<Description>\"" + DateTime.Now + "\"</Description>");
            Debug.WriteLine("<Options moHideChildren=\"1\" moAllowManualCollapseAndExpand=\"1\"/>");
            Debug.WriteLine("<LastState Value=\"\" Activated=\"1\" RealAddress=\"00000000\"/>");
            Debug.WriteLine("<GroupHeader>1</GroupHeader>");
            Debug.WriteLine("<CheatEntries>");

            foreach (FieldInfo prop in typeof(Adresses).GetFields(flags))
            {
                Tuple<string, string, int, bool> selected = info.FirstOrDefault(x => x.Item1 == prop.Name);
                if (selected == null)
                {
                    selected = new Tuple<string, string, int, bool>(prop.Name, "4 Bytes", 0, false);
                }

                Debug.WriteLine("<CheatEntry>");
                Debug.Indent();
                Debug.WriteLine("<ID>" + i + "</ID>");
                Debug.WriteLine("<Description>\"" + prop.Name + "\"</Description>");
                Debug.WriteLine("<VariableType>" + selected.Item2 + "</VariableType>");
                if (selected.Item4 && selected.Item1 != "String")
                {
                    Debug.WriteLine("<ShowAsHex>1</ShowAsHex>");
                }
                if (selected.Item2 == "Array of byte")
                {
                    Debug.WriteLine("<ByteLength>" + selected.Item3 + "</ByteLength>");
                }
                if (selected.Item2 == "String")
                {
                    Debug.WriteLine("<Length>" + selected.Item3 + "</Length>");
                    Debug.WriteLine("<Unicode>0</Unicode>");
                    Debug.WriteLine("<CodePage>0</CodePage>");
                    Debug.WriteLine("<ZeroTerminate>1</ZeroTerminate>");
                }
                Debug.WriteLine("<Address>" + ((int)prop.GetValue(a)).ToString("X4") + "</Address>");
                Debug.Unindent();
                Debug.WriteLine("</CheatEntry>");

                i++;
            }
            Debug.Unindent();
            Debug.WriteLine("</CheatEntries>");
            Debug.Unindent();
            Debug.WriteLine("</CheatEntry>");
            Debug.Unindent();
            Debug.WriteLine("</CheatEntries>");
            Debug.WriteLine("<UserdefinedSymbols/>");
            Debug.Unindent();
            Debug.WriteLine("</CheatTable>");

            Debug.Flush();
            Debug.Listeners.Remove("CheatTable");
            Debug.Listeners.Add(Program.ListenerConsole);
            Debug.Listeners.Add(Program.ListenerLog);

            string s = Path.GetFullPath(@"table.ct");
            string t = Path.GetFullPath(@"..\..\..\table.ct");
            try
            {
                System.IO.File.Copy(s, t, true);
            }
            catch (Exception) { }

#endif
        }

        static public void ListZeroAdresses(Adresses a)
        {
#if DEBUG
            BindingFlags flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance | BindingFlags.DeclaredOnly;

            var list = new List<Tuple<string, int>>();
            foreach (FieldInfo prop in typeof(Adresses).GetFields(flags))
            {
                if ((int)prop.GetValue(a) <= 0x401000 && prop.Name != "game")
                {
                    list.Add(new Tuple<string, int>(prop.Name, (int)prop.GetValue(a)));
                }
            }

            if (list.Count > 0)
            {
                Debug.WriteLine("These addresses are below 0x400000 and dangerous:");
                Debug.Indent();
                foreach (var item in list)
                {
                    Debug.WriteLine(item.Item1);
                }
                Debug.Unindent();
            }
#endif
        }
    }
}