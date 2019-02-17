using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cabal4
{
    public struct Adresses
    {
        internal int Base;
        internal int baseCam;
        internal int bypassHackDetection;
        internal int castingTime;
        internal int comboTime;
        internal int dex;
        internal int fullZoom1;
        internal int fullZoom2;
        internal int game;
        internal int id;
        internal int intele;
        internal int level;
        internal int nation;
        internal int pickupRange1;
        internal int pickupRange2;
        internal int skillCoolDown;
        internal int skillDelay;
        internal int str;
        internal int tp;
        internal int wallBase;
        internal int wallHackDetection;
        internal int x;
        internal int y;
        internal int zoom;
    }

    public class GameData
    {
        public int dex = 0;
        public string id = "";
        public int intele = 0;
        public int level = 0;
        public int nation = 0;
        public int str = 0;
        public float x = 0f;
        public float y = 0f;
    }

    internal static class Offsets
    {
        // Aufbau: name level
        // ex:     x1

        static readonly public int castingTime0 = 0xc39340;
        static readonly public int castingTime1 = 0x8a14;
        static readonly public int comboTime0 = 0x00839340;
        static readonly public int comboTime1 = 0x4c85;
        static readonly public int dex0 = 0x00839340;
        static readonly public int dex1 = 0x4b2c;
        static readonly public int id0 = 0xC5D778;
        static readonly public int intel0 = 0x00839340;
        static readonly public int intel1 = 0x4B28;
        static readonly public int level0 = 0x00839340;
        static readonly public int level1 = 0x804;
        static readonly public int level2 = 0x104;
        static readonly public int level3 = 0x1B58;
        static readonly public int map0 = 0x4BBC;
        static readonly public int nation0 = 0x00839340;
        static readonly public int nation1 = 0x474;
        static readonly public int PerfCombo0 = 0x00839340;
        static readonly public int PerfCombo1 = 0x4c85;
        static readonly public int pickupRange0 = 0x2057D2;
        static readonly public int skillDelay0 = 0xc39340;
        static readonly public int skillDelay1 = 0x4bac;
        static readonly public int str0 = 0x00839340;
        static readonly public int str1 = 0x4b24;
        static readonly public int tp0 = 0x00839340;
        static readonly public int tp1 = 0x804;
        static readonly public int tp2 = 0x128;
        static readonly public int tp3 = 0x26C;
        static readonly public int tp4 = 0x550;
        static readonly public int tp5 = 0x2E8;
        static readonly public int wallHack0 = 0x40814;
        static readonly public int x0 = 0x839340;
        static readonly public int x1 = 0x36c;
        static readonly public int y0 = 0x00839340;
        static readonly public int y1 = 0x370;
    }

    internal static class Pattern
    {
        static readonly public byte[] Base = new byte[] { 0xA1, 0x00, 0x00, 0x00, 0x00, 0x8A, 0x0E };
        static readonly public byte[] baseCam = new byte[] { 0xA3, 0x00, 0x00, 0x00, 0x00, 0x3B, 0x46, 0x74 };
        static readonly public byte[] bypassHackDetection = new byte[] { 0x2D, 0x00, 0x00, 0x00, 0x00, 0x0F, 0x85, 0x00, 0x00, 0x00, 0x00, 0x6A };
        static readonly public byte[] damageSkillCount = new byte[] { 0x33, 0xC0, 0xA3, 0x00, 0x00, 0x00, 0x00, 0xA3, 0x00, 0x00, 0x00, 0x00, 0xA3, 0x00, 0x00, 0x00, 0x00, 0xA3, 0x00, 0x00, 0x00, 0x00, 0xA1 };
        static readonly public byte[] fullZoom = new byte[] { 0xF3, 0x0F, 0x11, 0x05, 0x00, 0x00, 0x00, 0x00, 0x66, 0x0F, 0x6E, 0xC0 };
        static readonly public byte[] keyClass = new byte[] { 0x8B, 0x34, 0x95, 0x00, 0x00, 0x00, 0x00, 0xEB, 0x2C };
        static readonly public byte[] pickupRange = new byte[] { 0x83, 0xF9, 0x02, 0x0F, 0x8F, 0x00, 0x00, 0x00, 0x00, 0x83 };
        static readonly public byte[] skillCoolDown = new byte[] { 0x66, 0x8B, 0x81, 0x00, 0x00, 0x00, 0x00, 0xC3, 0xF6 };
        static readonly public byte[] wallBase = new byte[] { 0x8B, 0x35, 0x00, 0x00, 0x00, 0x00, 0x8B, 0xC3 };
        static readonly public byte[] wallHackDetection = new byte[] { 0xB8, 0x00, 0x00, 0x00, 0x00, 0xE8, 0x00, 0x00, 0x00, 0x00, 0x81, 0xEC, 0x00, 0x00, 0x00, 0x00, 0x53, 0x56, 0x8B, 0x75, 0x08, 0x57, 0x66, 0x8B, 0x4E, 0x0A };
        static readonly public byte[] zoom = new byte[] { 0x77, 0x06, 0xF3, 0x0F, 0x10, 0xC0, 0xEB, 0x04, 0xF3, 0x0F, 0x10, 0x00 };
    }

    internal static class PatternMask
    {
        static readonly public string Base = "x????xx";
        static readonly public string baseCam = "x????xxx";
        static readonly public string bypassHackDetection = "x????xx????x";
        static readonly public string damageSkillCount = "xxx????x????x????x????x";
        static readonly public string fullZoom = "xxxx????xxxx";
        static readonly public string keyClass = "xxx????xx";
        static readonly public string pickupRange = "xxxxx????x";
        static readonly public string skillCoolDown = "xxx????xx";
        static readonly public string wallBase = "xx????xx";
        static readonly public string wallHackDetection = "x????x????xx????xxxxxxxxxx";
        static readonly public string zoom = "xxxxxxxxxxxx";
    }

    internal static class PatternSearchArea
    {
        static readonly public int lowerBound = 0x400000;
        static readonly public int upperBound = 0xB00000;
    }

    internal static class StringLength
    {
        static readonly public int id = 100;
    }
}