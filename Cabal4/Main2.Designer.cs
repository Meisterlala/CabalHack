namespace Cabal4
{
    partial class Main2
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main2));
            this.ButtonMinimize = new System.Windows.Forms.Button();
            this.buttonClose = new System.Windows.Forms.Button();
            this.label1 = new Cabal4.Main2.MyLabel();
            this.groupBoxTeleport = new System.Windows.Forms.GroupBox();
            this.buttonTP = new System.Windows.Forms.Button();
            this.buttonTPRemove = new System.Windows.Forms.Button();
            this.buttonTPAdd = new System.Windows.Forms.Button();
            this.listBoxTeleport = new System.Windows.Forms.ListBox();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.tabControl1 = new Cabal4.Main2.TabControlWithoutHeader();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBoxToggleHacks = new System.Windows.Forms.GroupBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.checkBoxWallHack = new System.Windows.Forms.CheckBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.checkBox4 = new System.Windows.Forms.CheckBox();
            this.checkBox5 = new System.Windows.Forms.CheckBox();
            this.checkBox6 = new System.Windows.Forms.CheckBox();
            this.checkBox7 = new System.Windows.Forms.CheckBox();
            this.groupBoxTeleport.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBoxToggleHacks.SuspendLayout();
            this.SuspendLayout();
            // 
            // ButtonMinimize
            // 
            this.ButtonMinimize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonMinimize.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(181)))), ((int)(((byte)(246)))));
            this.ButtonMinimize.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.ButtonMinimize.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(134)))), ((int)(((byte)(195)))));
            this.ButtonMinimize.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(134)))), ((int)(((byte)(195)))));
            this.ButtonMinimize.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(134)))), ((int)(((byte)(195)))));
            this.ButtonMinimize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ButtonMinimize.ForeColor = System.Drawing.Color.White;
            this.ButtonMinimize.Location = new System.Drawing.Point(388, 12);
            this.ButtonMinimize.Name = "ButtonMinimize";
            this.ButtonMinimize.Size = new System.Drawing.Size(75, 23);
            this.ButtonMinimize.TabIndex = 0;
            this.ButtonMinimize.TabStop = false;
            this.ButtonMinimize.Text = "_";
            this.ButtonMinimize.UseVisualStyleBackColor = false;
            this.ButtonMinimize.Click += new System.EventHandler(this.ButtonMinimize_Click);
            // 
            // buttonClose
            // 
            this.buttonClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(181)))), ((int)(((byte)(246)))));
            this.buttonClose.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.buttonClose.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(134)))), ((int)(((byte)(195)))));
            this.buttonClose.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(134)))), ((int)(((byte)(195)))));
            this.buttonClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(134)))), ((int)(((byte)(195)))));
            this.buttonClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonClose.ForeColor = System.Drawing.Color.White;
            this.buttonClose.Location = new System.Drawing.Point(469, 12);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(75, 23);
            this.buttonClose.TabIndex = 1;
            this.buttonClose.TabStop = false;
            this.buttonClose.Text = "X";
            this.buttonClose.UseVisualStyleBackColor = false;
            this.buttonClose.Click += new System.EventHandler(this.ButtonClose_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Enabled = false;
            this.label1.Font = new System.Drawing.Font("Consolas", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(56, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(210, 32);
            this.label1.TabIndex = 2;
            this.label1.Text = "~ CABAL Box ~";
            // 
            // groupBoxTeleport
            // 
            this.groupBoxTeleport.Controls.Add(this.buttonTP);
            this.groupBoxTeleport.Controls.Add(this.buttonTPRemove);
            this.groupBoxTeleport.Controls.Add(this.buttonTPAdd);
            this.groupBoxTeleport.Controls.Add(this.listBoxTeleport);
            this.groupBoxTeleport.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxTeleport.ForeColor = System.Drawing.Color.White;
            this.groupBoxTeleport.Location = new System.Drawing.Point(6, 6);
            this.groupBoxTeleport.Name = "groupBoxTeleport";
            this.groupBoxTeleport.Size = new System.Drawing.Size(267, 254);
            this.groupBoxTeleport.TabIndex = 3;
            this.groupBoxTeleport.TabStop = false;
            this.groupBoxTeleport.Text = "Teleport";
            // 
            // buttonTP
            // 
            this.buttonTP.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(181)))), ((int)(((byte)(246)))));
            this.buttonTP.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonTP.Location = new System.Drawing.Point(50, 212);
            this.buttonTP.Name = "buttonTP";
            this.buttonTP.Size = new System.Drawing.Size(155, 23);
            this.buttonTP.TabIndex = 7;
            this.buttonTP.Text = "teleport";
            this.buttonTP.UseVisualStyleBackColor = false;
            // 
            // buttonTPRemove
            // 
            this.buttonTPRemove.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(181)))), ((int)(((byte)(246)))));
            this.buttonTPRemove.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonTPRemove.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonTPRemove.Location = new System.Drawing.Point(161, 23);
            this.buttonTPRemove.Name = "buttonTPRemove";
            this.buttonTPRemove.Size = new System.Drawing.Size(100, 29);
            this.buttonTPRemove.TabIndex = 6;
            this.buttonTPRemove.Text = "remove";
            this.buttonTPRemove.UseVisualStyleBackColor = false;
            this.buttonTPRemove.Click += new System.EventHandler(this.ButtonTPRemove_Click);
            // 
            // buttonTPAdd
            // 
            this.buttonTPAdd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(181)))), ((int)(((byte)(246)))));
            this.buttonTPAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonTPAdd.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonTPAdd.Location = new System.Drawing.Point(6, 23);
            this.buttonTPAdd.Name = "buttonTPAdd";
            this.buttonTPAdd.Size = new System.Drawing.Size(100, 29);
            this.buttonTPAdd.TabIndex = 5;
            this.buttonTPAdd.Text = "add new";
            this.buttonTPAdd.UseVisualStyleBackColor = false;
            this.buttonTPAdd.Click += new System.EventHandler(this.ButtonTPAdd_Click);
            // 
            // listBoxTeleport
            // 
            this.listBoxTeleport.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBoxTeleport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(181)))), ((int)(((byte)(246)))));
            this.listBoxTeleport.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBoxTeleport.ForeColor = System.Drawing.Color.White;
            this.listBoxTeleport.FormattingEnabled = true;
            this.listBoxTeleport.ItemHeight = 15;
            this.listBoxTeleport.Location = new System.Drawing.Point(6, 58);
            this.listBoxTeleport.Name = "listBoxTeleport";
            this.listBoxTeleport.Size = new System.Drawing.Size(255, 139);
            this.listBoxTeleport.TabIndex = 0;
            this.listBoxTeleport.DoubleClick += new System.EventHandler(this.ListBoxTeleport_DoubleClick);
            // 
            // linkLabel1
            // 
            this.linkLabel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabel1.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(142)))), ((int)(((byte)(142)))));
            this.linkLabel1.Location = new System.Drawing.Point(305, 17);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(77, 14);
            this.linkLabel1.TabIndex = 4;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "report Bug";
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.Location = new System.Drawing.Point(9, 54);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(0);
            this.tabControl1.Multiline = true;
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(538, 389);
            this.tabControl1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(150)))), ((int)(((byte)(218)))));
            this.tabPage1.Controls.Add(this.groupBoxToggleHacks);
            this.tabPage1.Controls.Add(this.groupBoxTeleport);
            this.tabPage1.Location = new System.Drawing.Point(-3, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(544, 370);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            // 
            // groupBoxToggleHacks
            // 
            this.groupBoxToggleHacks.Controls.Add(this.checkBox7);
            this.groupBoxToggleHacks.Controls.Add(this.checkBox6);
            this.groupBoxToggleHacks.Controls.Add(this.checkBox5);
            this.groupBoxToggleHacks.Controls.Add(this.checkBox4);
            this.groupBoxToggleHacks.Controls.Add(this.checkBox3);
            this.groupBoxToggleHacks.Controls.Add(this.checkBox1);
            this.groupBoxToggleHacks.Controls.Add(this.checkBox2);
            this.groupBoxToggleHacks.Controls.Add(this.checkBoxWallHack);
            this.groupBoxToggleHacks.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxToggleHacks.ForeColor = System.Drawing.Color.White;
            this.groupBoxToggleHacks.Location = new System.Drawing.Point(279, 6);
            this.groupBoxToggleHacks.Name = "groupBoxToggleHacks";
            this.groupBoxToggleHacks.Size = new System.Drawing.Size(251, 254);
            this.groupBoxToggleHacks.TabIndex = 8;
            this.groupBoxToggleHacks.TabStop = false;
            this.groupBoxToggleHacks.Text = "Toggle Hacks";
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(23, 54);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(124, 19);
            this.checkBox2.TabIndex = 5;
            this.checkBox2.Text = "unlimited zoom";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // checkBoxWallHack
            // 
            this.checkBoxWallHack.AutoSize = true;
            this.checkBoxWallHack.Location = new System.Drawing.Point(23, 29);
            this.checkBoxWallHack.Name = "checkBoxWallHack";
            this.checkBoxWallHack.Size = new System.Drawing.Size(82, 19);
            this.checkBoxWallHack.TabIndex = 4;
            this.checkBoxWallHack.Text = "wallhack";
            this.checkBoxWallHack.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(-3, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(544, 370);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(23, 79);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(166, 19);
            this.checkBox1.TabIndex = 6;
            this.checkBox1.Text = "view every inventory";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // checkBox3
            // 
            this.checkBox3.AutoSize = true;
            this.checkBox3.Location = new System.Drawing.Point(23, 104);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(145, 19);
            this.checkBox3.TabIndex = 7;
            this.checkBox3.Text = "teleport on click";
            this.checkBox3.UseVisualStyleBackColor = true;
            // 
            // checkBox4
            // 
            this.checkBox4.AutoSize = true;
            this.checkBox4.Location = new System.Drawing.Point(23, 129);
            this.checkBox4.Name = "checkBox4";
            this.checkBox4.Size = new System.Drawing.Size(131, 19);
            this.checkBox4.TabIndex = 8;
            this.checkBox4.Text = "no casting time";
            this.checkBox4.UseVisualStyleBackColor = true;
            // 
            // checkBox5
            // 
            this.checkBox5.AutoSize = true;
            this.checkBox5.Location = new System.Drawing.Point(23, 154);
            this.checkBox5.Name = "checkBox5";
            this.checkBox5.Size = new System.Drawing.Size(124, 19);
            this.checkBox5.TabIndex = 9;
            this.checkBox5.Text = "no skill delay";
            this.checkBox5.UseVisualStyleBackColor = true;
            // 
            // checkBox6
            // 
            this.checkBox6.AutoSize = true;
            this.checkBox6.Location = new System.Drawing.Point(23, 178);
            this.checkBox6.Name = "checkBox6";
            this.checkBox6.Size = new System.Drawing.Size(124, 19);
            this.checkBox6.TabIndex = 10;
            this.checkBox6.Text = "perfect combos";
            this.checkBox6.UseVisualStyleBackColor = true;
            // 
            // checkBox7
            // 
            this.checkBox7.AutoSize = true;
            this.checkBox7.Location = new System.Drawing.Point(23, 203);
            this.checkBox7.Name = "checkBox7";
            this.checkBox7.Size = new System.Drawing.Size(166, 19);
            this.checkBox7.TabIndex = 11;
            this.checkBox7.Text = "increased loot range";
            this.checkBox7.UseVisualStyleBackColor = true;
            // 
            // Main2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(101)))), ((int)(((byte)(147)))));
            this.ClientSize = new System.Drawing.Size(556, 452);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.ButtonMinimize);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Main2";
            this.Text = "CabalBox";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Main2_FormClosed);
            this.groupBoxTeleport.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.groupBoxToggleHacks.ResumeLayout(false);
            this.groupBoxToggleHacks.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button ButtonMinimize;
        private System.Windows.Forms.Button buttonClose;
        private MyLabel label1;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.GroupBox groupBoxTeleport;
        private System.Windows.Forms.ListBox listBoxTeleport;
        private System.Windows.Forms.Button buttonTP;
        private System.Windows.Forms.Button buttonTPRemove;
        private System.Windows.Forms.Button buttonTPAdd;
        private TabControlWithoutHeader tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.GroupBox groupBoxToggleHacks;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.CheckBox checkBoxWallHack;
        private System.Windows.Forms.Panel panelBlock;
        private System.Windows.Forms.Label labelBlock;
        private System.Windows.Forms.CheckBox checkBox7;
        private System.Windows.Forms.CheckBox checkBox6;
        private System.Windows.Forms.CheckBox checkBox5;
        private System.Windows.Forms.CheckBox checkBox4;
        private System.Windows.Forms.CheckBox checkBox3;
        private System.Windows.Forms.CheckBox checkBox1;
    }
}