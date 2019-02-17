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
    public partial class Main2 : Form
    {
        public static Main2 forExternal;

        private int MoveBlockDownWaitExtraTicks = 0;

        private bool MoveBlockDownWaitOnlyOnce = false;

        public Main2()
        {
            forExternal = this;
            Thread.CurrentThread.Name = "Interface";
            InitializeComponent();
            LoadDefaultTP();
            BlockEnable();

            Program.StartGameDetection();
        }

        public void AddNewTeleport(float x, float y, string name)
        {
            listBoxTeleport.Items.Add(new TeleportListBoxItem() { name = name, x = x, y = y });
        }

        public void LoadFreezeList(List<FreezeHack> all)
        {
            Debug.WriteLine("Attatching FreezeHacks");
            Debug.Indent();
            Invoke((MethodInvoker)delegate ()
            {
                for (int i = 0; i < groupBoxToggleHacks.Controls.Count; i++)
                {
                    for (int j = 0; j < all.Count; j++)
                    {
                        if (all[j].name.ToLower() == groupBoxToggleHacks.Controls[i].Text.ToLower())
                        {
                            var hack = all[j];
                            var box = (CheckBox)groupBoxToggleHacks.Controls[i];
                            box.CheckedChanged += delegate
                            {
                                if (box.Checked)
                                {
                                    hack.FEnable();
                                }
                                else
                                {
                                    hack.FDisable();
                                }
                            };
                            all.RemoveAt(j);
                            break;
                        }
                    }
                }
                if (all.Count > 0)
                {
                    foreach (FreezeHack item in all)
                    {
                        Debug.WriteLine("Could not attatch " + item.name + " to a controll");
                    }
                }
                Debug.Unindent();
            });
        }

        internal void BlockDisable()
        {
            Invoke((MethodInvoker)delegate
            {
                labelBlock.Text = "loading hack...";
                panelBlock.BackColor = Color.Green;
                panelBlock.BringToFront();
                labelBlock.BringToFront();
                this.DoubleBuffered = true;

                System.Windows.Forms.Timer t = new System.Windows.Forms.Timer();
                t.Tick += MoveBlockDown;
                t.Interval = 2;
                t.Start();
            });
        }

        internal void BlockEnable()
        {
            MethodInvoker toExec = delegate ()
            {
                // If not initilized
                if (panelBlock == null)
                {
                    // panelBlock
                    panelBlock = new Panel
                    {
                        BackColor = System.Drawing.Color.DarkRed,
                        Size = tabControl1.Size,
                        Location = tabControl1.Location,
                        Name = "panelBlock",
                        TabIndex = 5
                    };

                    // labelBlock
                    labelBlock = new Label
                    {
                        AutoSize = false,
                        TextAlign = ContentAlignment.MiddleCenter,
                        Size = new System.Drawing.Size(300, 100),
                        Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0))),
                        Location = new Point((panelBlock.Size.Width / 2) - 150, (panelBlock.Size.Height / 2) - 70),
                        Name = "labelBlock",
                        TabIndex = 0,
                        Text = "waiting for Cabal ..."
                    };

                    panelBlock.Controls.Add(labelBlock);
                    Controls.Add(this.panelBlock);
                    panelBlock.BringToFront();
                }
                else
                {
                    panelBlock.BackColor = System.Drawing.Color.DarkRed;
                    labelBlock.Text = "waiting for Cabal ...";
                    panelBlock.Enabled = true;
                    panelBlock.Visible = true;
                    labelBlock.Enabled = true;
                    labelBlock.Visible = true;
                    panelBlock.BringToFront();

                    System.Windows.Forms.Timer tsd = new System.Windows.Forms.Timer();
                    tsd.Tick += MoveBlockUp;
                    tsd.Interval = 2;
                    tsd.Start();
                }
            };

            if (InvokeRequired)
            {
                Invoke(toExec);
            }
            else
            {
                toExec();
            }
        }

        protected override void WndProc(ref Message m)
        {
            const int HTCAPTION = 0x2;
            const int HTCLIENT = 0x1;
            const int WM_NCHITTEST = 0x84;
            switch (m.Msg)
            {
                case WM_NCHITTEST:
                    base.WndProc(ref m);
                    if ((int)m.Result == HTCLIENT)
                    {
                        m.Result = (IntPtr)HTCAPTION;
                    }

                    return;
            }

            base.WndProc(ref m);
        }

        private void ButtonClose_Click(object sender, EventArgs e)
        {
            Exit();
        }

        private void ButtonMinimize_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void ButtonTPAdd_Click(object sender, EventArgs e)
        {
            var AddDialog = new AddTeleport(this, Program.cheat);

            var result = AddDialog.ShowDialog();
        }

        private void ButtonTPRemove_Click(object sender, EventArgs e)
        {
            listBoxTeleport.Items.RemoveAt(listBoxTeleport.SelectedIndex);
        }

        private void Exit()
        {
            Application.Exit();
        }

        private void ListBoxTeleport_DoubleClick(object sender, EventArgs e)
        {
            {
                var i = (TeleportListBoxItem)listBoxTeleport.SelectedItem;

                if (i == null)
                {
                    return;
                }

                Program.cheat.Teleport(i.x, i.y);
            }
        }

        private void LoadDefaultTP()
        {
            listBoxTeleport.Items.Add(new TeleportListBoxItem() { name = "Return to City", x = 10f, y = 10f, preAdded = true });
            listBoxTeleport.Items.Add(new TeleportListBoxItem() { name = "Test Out", x = 13919f, y = 24169f, preAdded = true });
            listBoxTeleport.Items.Add(new TeleportListBoxItem() { name = "Test In", x = 8549f, y = 22767f, preAdded = true });
        }

        private void Main2_FormClosed(object sender, FormClosedEventArgs e)
        {
            Exit();
        }

        private void MoveBlockDown(object sender, EventArgs e)
        {
            if (MoveBlockDownWaitExtraTicks > 0)
            {
                MoveBlockDownWaitExtraTicks--;
                return;
            }

            if (panelBlock.Top == tabControl1.Top && !MoveBlockDownWaitOnlyOnce)
            {
                MoveBlockDownWaitOnlyOnce = true;
                MoveBlockDownWaitExtraTicks = 80;
                return;
            }

            panelBlock.Top += 6;

            if (panelBlock.Top >= this.Height)
            {
                ((System.Windows.Forms.Timer)sender).Stop();
                panelBlock.Enabled = false;
                panelBlock.Visible = false;
                labelBlock.Enabled = false;
                labelBlock.Visible = false;
            }
        }

        private void MoveBlockUp(object sender, EventArgs e)
        {
            if (panelBlock.Top - 6 <= tabControl1.Top)
            {
                panelBlock.Top = tabControl1.Top;
                ((System.Windows.Forms.Timer)sender).Stop();
            }
            else
            {
                panelBlock.Top -= 6;
            }
        }

        public partial class MyLabel : Label
        {
            protected override void OnPaint(PaintEventArgs e)
            {
                TextRenderer.DrawText(e.Graphics, this.Text.ToString(), this.Font, ClientRectangle, ForeColor);
            }
        }

        public partial class TabControlWithoutHeader : TabControl
        {
            protected override void WndProc(ref Message m)
            {
                if (m.Msg == 0x1300 + 40)
                {
                    RECT rc = (RECT)m.GetLParam(typeof(RECT));
                    rc.Left -= 7;
                    rc.Right += 7;
                    rc.Top -= 2;
                    rc.Bottom += 7;
                    Marshal.StructureToPtr(rc, m.LParam, true);
                }
                base.WndProc(ref m);
            }

            private struct RECT
            {
                public int Left, Top, Right, Bottom;
            }
        }

        internal class TeleportListBoxItem
        {
            public string name = "undefined";

            public bool preAdded = false;

            public float x, y;

            public override string ToString()
            {
                return name;
            }
        }
    }
}