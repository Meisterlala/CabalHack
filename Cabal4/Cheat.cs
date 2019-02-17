using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cabal4
{
    public delegate void Loop();

    public delegate void Switch();

    public class Cheat
    {
        public Adresses a;
        public List<FreezeHack> freezeHacks = new List<FreezeHack>();
        public GameData gd = new GameData();
        private MemHelper mem = new MemHelper();

        public Cheat(Process cabalProcess)
        {
            a.game = mem.LoadProcess(cabalProcess);
        }

        public void ReadGameData()
        {
            gd.x = mem.ReadMemoryFloat(a.x);
            gd.y = mem.ReadMemoryFloat(a.y);
        }

        public void Start()
        {
            Debug.WriteLine("Cheat Setup");
            Debug.Indent();

            // a.game = mem.GetProcess(Program.exeName);

            SetupPointers();
            SetupPatterns();

            freezeHacks = FreezeHack.SetupFreezeHacks(mem, a);
            Main2.forExternal.LoadFreezeList(freezeHacks);

            RemoveProtection();
            Debug.Unindent();

            DebugOutput.ListZeroAdresses(a);
            DebugOutput.DumpAdresses(a);
            DebugOutput.GenerateCheatTable(a);

            Debug.WriteLine("Cheat Loop");
            int counter = 99999;
            while (true)
            {
                if (counter >= 500)
                {
                    LoopRare();
                    counter = 0;
                }
                Loop();

                counter++;
                Thread.Sleep(10);
            }
        }

        public void Teleport(float x, float y)
        {
            // If to Base
            if (x < 20 && y < 20)
            {
                TeleportDirect(x, y);
                return;
            }

            int maxDistance = 10;
            int delay = 50;

            // Current Position
            float startX = mem.ReadMemoryFloat(a.x);
            float startY = mem.ReadMemoryFloat(a.y);

            if (startX == x && startY == y)
            {
                return;
            }

            float stepX = x;
            float stepY = y;

            // If tp to far
            if (Math.Abs(x - startX) > maxDistance)
            {
                if (x > startX)
                {
                    stepX = startX + maxDistance;
                }
                else
                {
                    stepX = startX - maxDistance;
                }
            }
            if (Math.Abs(y - startY) > maxDistance)
            {
                if (y > startY)
                {
                    stepY = startY + maxDistance;
                }
                else
                {
                    stepY = startY - maxDistance;
                }
            }
            // Check to not overshoot

            if (x > startX && stepX > x)
            {
                stepX = x;
            }
            else if (x < startX && stepX < x)
            {
                stepX = x;
            }

            if (y > startY && stepY > y)
            {
                stepY = y;
            }
            else if (y < startY && stepY < y)
            {
                stepY = y;
            }

            TeleportDirect(stepX, stepY);
            Thread.Sleep(delay);

            Teleport(x, y);
        }

        private void Loop()
        {
            ReadGameData();

            UpdateMainForm();
        }

        private void LoopRare()
        {
            gd.level = mem.ReadMemoryInt(a.level);
            gd.str = mem.ReadMemoryInt(a.str);
            gd.intele = mem.ReadMemoryInt(a.intele);
            gd.dex = mem.ReadMemoryInt(a.dex);
            gd.id = mem.ReadMemoryString(a.id, StringLength.id);
            gd.nation = mem.ReadMemoryInt(a.nation);
        }

        private void RemoveProtection()
        {
            mem.WriteMemory(a.bypassHackDetection, new byte[] { 0xE9, 0x1A, 0x01, 0x00, 0x00, 0x90 });
            mem.WriteMemory(a.wallHackDetection, new byte[] { 0xC3, 0x90, 0x90, 0x90, 0x90, 0x00 });
        }

        private void SetupPatterns()
        {
            Debug.WriteLine("Setting up Patterns");
            Debug.Indent();

            byte[] PatternSearchBuffer = mem.ReadMemoryBuffer(PatternSearchArea.lowerBound, PatternSearchArea.upperBound - PatternSearchArea.lowerBound);

            List<Action> patternActions = new List<Action>
            {
                delegate
                {
                    var pointer = mem.FindPattern(Pattern.Base, PatternMask.Base, PatternSearchBuffer) + 1 + PatternSearchArea.lowerBound;
                    a.Base = mem.ResovePointer(pointer);
                },
                delegate
                {
                    var pointer = mem.FindPattern(Pattern.baseCam, PatternMask.baseCam, PatternSearchBuffer) + 1 + PatternSearchArea.lowerBound;
                    a.baseCam = mem.ResovePointer(pointer);
                },
                delegate
                {
                    a.bypassHackDetection = mem.FindPattern(Pattern.bypassHackDetection, PatternMask.bypassHackDetection, PatternSearchBuffer) + 5 + PatternSearchArea.lowerBound;
                },
                delegate
                {
                    var pointer = mem.FindPattern(Pattern.fullZoom, PatternMask.fullZoom, PatternSearchBuffer) + 4 + PatternSearchArea.lowerBound;
                    a.fullZoom1 = mem.ResovePointer(pointer);
                    a.fullZoom2 = a.fullZoom1 + 4;
                },
                delegate
                {
                    var pointer = mem.FindPattern(Pattern.pickupRange, PatternMask.pickupRange, PatternSearchBuffer) + PatternSearchArea.lowerBound;
                    a.pickupRange1 = mem.ResovePointer(pointer);
                    a.pickupRange2 = mem.ResovePointer(pointer+9);
                },
                delegate
                {
                    a.skillCoolDown = mem.FindPattern(Pattern.skillCoolDown, PatternMask.skillCoolDown, PatternSearchBuffer) + PatternSearchArea.lowerBound;
                },
                delegate
                {
                    var pointer = mem.FindPattern(Pattern.wallBase, PatternMask.wallBase, PatternSearchBuffer) + 2 + PatternSearchArea.lowerBound;
                    a.wallBase = mem.ResovePointer(pointer);
                    a.level = mem.ResovePointer(a.wallBase, Offsets.map0);
                },
                delegate
                {
                    a.wallHackDetection = mem.FindPattern(Pattern.wallHackDetection, PatternMask.wallHackDetection, PatternSearchBuffer) + PatternSearchArea.lowerBound;
                },
                delegate
                {
                    a.zoom = mem.FindPattern(Pattern.zoom, PatternMask.zoom, PatternSearchBuffer) + PatternSearchArea.lowerBound;
                }
            };

            Task[] taskCollection = new Task[patternActions.Count];
            for (int i = 0; i < patternActions.Count; i++)
            {
                Task t = new Task(patternActions[i]);
                t.Start();
                taskCollection[i] = t;
            }

            Task.WaitAll(taskCollection);
            Debug.Unindent();
        }

        private void SetupPointers()
        {
            Debug.WriteLine("Setting up Pointers");

            a.id = Offsets.id0;

            a.x = mem.ResovePointer(a.game + Offsets.x0, Offsets.x1);
            a.y = mem.ResovePointer(a.game + Offsets.y0, Offsets.y1);

            a.str = mem.ResovePointer(a.game + Offsets.str0, Offsets.str1);
            a.intele = mem.ResovePointer(a.game + Offsets.intel0, Offsets.intel1);
            a.dex = mem.ResovePointer(a.game + Offsets.dex0, Offsets.dex1);
            a.nation = mem.ResovePointer(a.game + Offsets.nation0, Offsets.nation1);

            a.skillDelay = mem.ResovePointer(Offsets.skillDelay0, Offsets.skillDelay1);
            a.comboTime = mem.ResovePointer(a.game + Offsets.comboTime0, Offsets.comboTime1);
            a.castingTime = mem.ResovePointer(Offsets.castingTime0, Offsets.castingTime1);
            a.tp = mem.ResovePointer(a.game + Offsets.tp0, Offsets.tp1, Offsets.tp2, Offsets.tp3, Offsets.tp4, Offsets.tp5);
        }

        private void TeleportDirect(float x, float y)
        {
            mem.WriteMemory(a.x, x);
            mem.WriteMemory(a.y, y);
        }

        private void UpdateMainForm()
        {
            if (Main.myForm != null)
            {
                Main.myForm.Invoke((MethodInvoker)delegate () { Main.UpdateForm(); });
            }
        }
    }
}