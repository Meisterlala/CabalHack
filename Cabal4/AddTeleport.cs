using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cabal4
{
    public partial class AddTeleport : Form
    {
        public string name = "";
        public float x = 0, y = 0;
        private string before;
        private CancellationTokenSource cts = new CancellationTokenSource();
        private bool disabled = true;
        private Main2 parent;
        private Task refresh;

        public AddTeleport(Main2 _parent, Cheat c)
        {
            InitializeComponent();
            parent = _parent;
            before = buttonAdd.Text;
            UpdateAdd();

            refresh = new Task(delegate
            {
                while (true)
                {
                    c.ReadGameData();
                    if (InvokeRequired)
                    {
                        Invoke((MethodInvoker)delegate
                        {
                            textBoxXR.Text = c.gd.x.ToString("0.00");
                            textBoxYR.Text = c.gd.y.ToString("0.00");
                        });
                    }

                    Thread.Sleep(10);
                }
            }, cts.Token);
            refresh.Start();
        }

        private void AddTeleport_FormClosed(object sender, FormClosedEventArgs e)
        {
            cts.Cancel();
        }

        private void ButtonAdd_Click(object sender, EventArgs e)
        {
            if (disabled)
            {
                return;
            }
            parent.AddNewTeleport(x, y, name);
            buttonAdd.Text = "Success!!";
        }

        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ButtonCopy_Click(object sender, EventArgs e)
        {
            textBoxXL.Text = textBoxXR.Text;
            textBoxYL.Text = textBoxYR.Text;
        }

        private void DiableAdd()
        {
            buttonAdd.BackColor = Color.FromArgb(11, 112, 193);
            buttonAdd.Enabled = false;
        }

        private void EnableAdd()
        {
            buttonAdd.BackColor = Color.FromArgb(100, 181, 246);
            buttonAdd.Enabled = true;
        }

        private void NewData()
        {
            buttonAdd.Text = before;

            if (!float.TryParse(textBoxXL.Text, out x)) { disabled = true; }
            if (!float.TryParse(textBoxYL.Text, out y)) { disabled = true; }

            name = (textBoxName.Text.Length > 0) ? textBoxName.Text : "x" + x.ToString("0") + "  y" + x.ToString("0");

            if (x != 0 && y != 0)
            {
                disabled = false;
            }
            UpdateAdd();
        }

        private void TextBoxName_TextChanged(object sender, EventArgs e)
        {
            NewData();
        }

        private void TextBoxXL_TextChanged(object sender, EventArgs e)
        {
            NewData();
        }

        private void TextBoxYL_TextChanged(object sender, EventArgs e)
        {
            NewData();
        }

        private void UpdateAdd()
        {
            if (disabled)
            {
                DiableAdd();
            }
            else
            {
                EnableAdd();
            }
        }
    }
}