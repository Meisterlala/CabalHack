using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cabal4
{
    public partial class Main : Form
    {
        static public Main myForm = null;
        private const int HTCAPTION = 0x2;
        private const int HTCLIENT = 0x1;
        private const int WM_NCHITTEST = 0x84;

        #region imports

        [DllImport("user32.dll")]
        public static extern IntPtr GetParent(IntPtr hWnd);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        #endregion imports

        public Main()
        {
            myForm = this;
            InitializeComponent();
        }

        static public void UpdateForm()
        {
            myForm.CurrentValuesX.Text = Program.cheat.gd.x.ToString("0.0");
            myForm.CurrentValuesY.Text = Program.cheat.gd.y.ToString("0.0");

            myForm.StatsLevel.Text = Program.cheat.gd.level.ToString();
            myForm.StatsStr.Text = Program.cheat.gd.str.ToString();
            myForm.StatsInt.Text = Program.cheat.gd.intele.ToString();
            myForm.StatsDex.Text = Program.cheat.gd.dex.ToString();

            myForm.InfoID.Text = Program.cheat.gd.id.ToString();
            myForm.InfoNation.Text = Program.cheat.gd.nation.ToString();

            if (myForm.checkBox1.Checked)
            {
                myForm.TeleportX.Text = Program.cheat.gd.x.ToString("0.0000");
                myForm.TeleportY.Text = Program.cheat.gd.y.ToString("0.0000");
            }
        }
    }
}