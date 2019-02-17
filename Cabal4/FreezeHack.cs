using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cabal4
{
    public class FreezeHack
    {
        public Action Disable;
        public Action Enable;
        public Action Freeze;
        public string name = "";
        private CancellationTokenSource tokenSource;
        private Task worker = null;

        public FreezeHack()
        {
        }

        public static List<FreezeHack> SetupFreezeHacks(MemHelper mem, Adresses a)
        {
            Debug.WriteLine("Setting up FreezeHacks");

            var freezeHacks = new List<FreezeHack>();
            FreezeHack currentHack;

            //////////////////////////////////////////////////
            // Pacifist Mode
            //////////////////////////////////////////////////
            currentHack = new FreezeHack() { name = "Tree Mode" };
            currentHack.Enable = delegate ()
            {
                mem.WriteMemory(a.skillDelay, 0);
            };
            currentHack.Disable = delegate ()
            {
                mem.WriteMemory(a.skillDelay, 1);
            };
            freezeHacks.Add(currentHack);

            //////////////////////////////////////////////////
            // Perfect Combos
            //////////////////////////////////////////////////
            currentHack = new FreezeHack() { name = "perfect combos" };
            currentHack.Freeze = delegate ()
            {
                mem.WriteMemory(a.comboTime, 0);
                Thread.Sleep(10);
            };
            freezeHacks.Add(currentHack);

            //////////////////////////////////////////////////
            // TP
            //////////////////////////////////////////////////
            currentHack = new FreezeHack() { name = "teleport on click" };
            currentHack.Freeze = delegate ()
            {
                mem.WriteMemory(a.tp, 0);
                Thread.Sleep(10);
            };
            freezeHacks.Add(currentHack);

            //////////////////////////////////////////////////
            // No Casting Time
            //////////////////////////////////////////////////
            currentHack = new FreezeHack() { name = "No Casting Time" };
            currentHack.Freeze = delegate ()
            {
                mem.WriteMemory(a.castingTime, 0);
                Thread.Sleep(10);
            };
            freezeHacks.Add(currentHack);

            //////////////////////////////////////////////////
            // No Skill Delay
            //////////////////////////////////////////////////
            currentHack = new FreezeHack() { name = "No Skill Delay" };
            currentHack.Freeze = delegate ()
            {
                int org = 1693093;

                mem.WriteMemory(a.skillDelay, org);
                Thread.Sleep(10);
            };
            freezeHacks.Add(currentHack);

            //////////////////////////////////////////////////
            // Range Hack
            //////////////////////////////////////////////////
            currentHack = new FreezeHack() { name = "increased loot range", };
            currentHack.Enable = delegate ()
            {
                mem.WriteMemory(a.pickupRange1, new byte[] { 0x83, 0xF9, 0x09 });
                mem.WriteMemory(a.pickupRange2, new byte[] { 0x83, 0xF9, 0x09 });
            };
            currentHack.Disable = delegate ()
            {
                mem.WriteMemory(a.pickupRange1, new byte[] { 0x83, 0xF9, 0x02 });
                mem.WriteMemory(a.pickupRange2, new byte[] { 0x83, 0xF9, 0x02 });
            };
            freezeHacks.Add(currentHack);

            //////////////////////////////////////////////////
            // WallHack
            //////////////////////////////////////////////////
            int WHsize = 0x3FFFF;
            int level = -1;
            byte[] wallBackup = new byte[WHsize];
            currentHack = new FreezeHack() { name = "WallHack" };
            currentHack.Enable = delegate ()
            {
                level = mem.ReadMemoryInt(a.level);
                wallBackup = mem.ReadMemoryBuffer(mem.ResovePointer(a.wallBase) + Offsets.wallHack0, WHsize);
                mem.WriteMemory(mem.ResovePointer(a.wallBase) + Offsets.wallHack0, new byte[WHsize]);
            };
            currentHack.Disable = delegate ()
            {
                if (level == mem.ReadMemoryInt(a.level))
                {
                    mem.WriteMemory(mem.ResovePointer(a.wallBase) + Offsets.wallHack0, wallBackup);
                }
            };
            freezeHacks.Add(currentHack);

            //////////////////////////////////////////////////
            // Zoom Hack
            //////////////////////////////////////////////////
            currentHack = new FreezeHack() { name = "Unlimited Zoom" };
            currentHack.Enable = delegate ()
            {
                mem.WriteMemory(a.fullZoom1, new byte[] { 0x20, 0x9D });
                mem.WriteMemory(a.fullZoom2, new byte[] { 0x20, 0x9D });
                mem.WriteMemory(a.zoom, new byte[] { 0xEB, 0x06 });
            };
            currentHack.Disable = delegate ()
            {
                mem.WriteMemory(a.zoom, new byte[] { 0x77, 0x06 });
            };
            freezeHacks.Add(currentHack);

            //////////////////////////////////////////////////
            // Game Master
            //////////////////////////////////////////////////
            currentHack = new FreezeHack() { name = "view every inventory" };
            currentHack.Enable = delegate ()
            {
                mem.WriteMemory(a.nation, 3);
            };
            currentHack.Disable = delegate ()
            {
                mem.WriteMemory(a.nation, 0);
            };
            freezeHacks.Add(currentHack);

            freezeHacks.Sort((x, y) => x.name.CompareTo(y.name));

            return freezeHacks;
        }

        public void FDisable()
        {
            Debug.WriteLine("Disabling: " + name);
            Disable?.Invoke();
            tokenSource?.Cancel();
        }

        public void FEnable()
        {
            Debug.WriteLine("Enabling:  " + name);
            Enable?.Invoke();
            if (Freeze != null)
            {
                worker?.Dispose();
                tokenSource = new CancellationTokenSource();
                worker = new Task(FreezeLoop, tokenSource.Token);
                worker.Start();
            }
        }

        public override string ToString()
        {
            return name;
        }

        private void FreezeLoop()
        {
            while (!tokenSource.Token.IsCancellationRequested)
            {
                Freeze.Invoke();
            }
        }
    }
}