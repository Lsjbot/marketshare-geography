namespace marketshare_geography
{
    partial class Form1
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
            this.quitbutton = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.LB_uni = new System.Windows.Forms.CheckedListBox();
            this.LB_kommun = new System.Windows.Forms.CheckedListBox();
            this.LB_lan = new System.Windows.Forms.CheckedListBox();
            this.button2 = new System.Windows.Forms.Button();
            this.LB_year = new System.Windows.Forms.CheckedListBox();
            this.button3 = new System.Windows.Forms.Button();
            this.LB_subject = new System.Windows.Forms.CheckedListBox();
            this.button4 = new System.Windows.Forms.Button();
            this.CBmarketshare = new System.Windows.Forms.CheckBox();
            this.CBabsolute = new System.Windows.Forms.CheckBox();
            this.Clearbutton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.LB_subjectgroup = new System.Windows.Forms.CheckedListBox();
            this.LB_sector = new System.Windows.Forms.CheckedListBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.button5 = new System.Windows.Forms.Button();
            this.CBmk = new System.Windows.Forms.CheckBox();
            this.CBk = new System.Windows.Forms.CheckBox();
            this.CBage = new System.Windows.Forms.CheckBox();
            this.CB24 = new System.Windows.Forms.CheckBox();
            this.button6 = new System.Windows.Forms.Button();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.CB_reg = new System.Windows.Forms.CheckBox();
            this.CB_hst = new System.Windows.Forms.CheckBox();
            this.CB_hpr = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // quitbutton
            // 
            this.quitbutton.Location = new System.Drawing.Point(950, 742);
            this.quitbutton.Name = "quitbutton";
            this.quitbutton.Size = new System.Drawing.Size(156, 70);
            this.quitbutton.TabIndex = 0;
            this.quitbutton.Text = "Exit";
            this.quitbutton.UseVisualStyleBackColor = true;
            this.quitbutton.Click += new System.EventHandler(this.button1_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(951, 17);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(156, 82);
            this.button1.TabIndex = 1;
            this.button1.Text = "Read data";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(12, 37);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(453, 656);
            this.richTextBox1.TabIndex = 2;
            this.richTextBox1.Text = "";
            // 
            // LB_uni
            // 
            this.LB_uni.CheckOnClick = true;
            this.LB_uni.FormattingEnabled = true;
            this.LB_uni.Location = new System.Drawing.Point(471, 37);
            this.LB_uni.Name = "LB_uni";
            this.LB_uni.Size = new System.Drawing.Size(205, 208);
            this.LB_uni.Sorted = true;
            this.LB_uni.TabIndex = 4;
            // 
            // LB_kommun
            // 
            this.LB_kommun.CheckOnClick = true;
            this.LB_kommun.FormattingEnabled = true;
            this.LB_kommun.Location = new System.Drawing.Point(471, 485);
            this.LB_kommun.Name = "LB_kommun";
            this.LB_kommun.Size = new System.Drawing.Size(205, 208);
            this.LB_kommun.Sorted = true;
            this.LB_kommun.TabIndex = 5;
            // 
            // LB_lan
            // 
            this.LB_lan.CheckOnClick = true;
            this.LB_lan.FormattingEnabled = true;
            this.LB_lan.Location = new System.Drawing.Point(471, 270);
            this.LB_lan.Name = "LB_lan";
            this.LB_lan.Size = new System.Drawing.Size(204, 191);
            this.LB_lan.Sorted = true;
            this.LB_lan.TabIndex = 6;
            // 
            // button2
            // 
            this.button2.Enabled = false;
            this.button2.Location = new System.Drawing.Point(951, 105);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(156, 80);
            this.button2.TabIndex = 7;
            this.button2.Text = "Tidsserie stud från kommun";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // LB_year
            // 
            this.LB_year.CheckOnClick = true;
            this.LB_year.FormattingEnabled = true;
            this.LB_year.Location = new System.Drawing.Point(695, 37);
            this.LB_year.Name = "LB_year";
            this.LB_year.Size = new System.Drawing.Size(197, 208);
            this.LB_year.Sorted = true;
            this.LB_year.TabIndex = 8;
            // 
            // button3
            // 
            this.button3.Enabled = false;
            this.button3.Location = new System.Drawing.Point(951, 191);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(156, 82);
            this.button3.TabIndex = 9;
            this.button3.Text = "Alla lärosäten stud från kommun";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // LB_subject
            // 
            this.LB_subject.CheckOnClick = true;
            this.LB_subject.FormattingEnabled = true;
            this.LB_subject.Location = new System.Drawing.Point(695, 270);
            this.LB_subject.Name = "LB_subject";
            this.LB_subject.Size = new System.Drawing.Size(197, 191);
            this.LB_subject.Sorted = true;
            this.LB_subject.TabIndex = 10;
            // 
            // button4
            // 
            this.button4.Enabled = false;
            this.button4.Location = new System.Drawing.Point(952, 279);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(155, 80);
            this.button4.TabIndex = 11;
            this.button4.Text = "Tidsserier stud i ämne";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // CBmarketshare
            // 
            this.CBmarketshare.AutoSize = true;
            this.CBmarketshare.Checked = true;
            this.CBmarketshare.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CBmarketshare.Location = new System.Drawing.Point(486, 726);
            this.CBmarketshare.Name = "CBmarketshare";
            this.CBmarketshare.Size = new System.Drawing.Size(162, 21);
            this.CBmarketshare.TabIndex = 12;
            this.CBmarketshare.Text = "Visa andel av totalen";
            this.CBmarketshare.UseVisualStyleBackColor = true;
            // 
            // CBabsolute
            // 
            this.CBabsolute.AutoSize = true;
            this.CBabsolute.Location = new System.Drawing.Point(485, 754);
            this.CBabsolute.Name = "CBabsolute";
            this.CBabsolute.Size = new System.Drawing.Size(134, 21);
            this.CBabsolute.TabIndex = 13;
            this.CBabsolute.Text = "Visa absoluta tal";
            this.CBabsolute.UseVisualStyleBackColor = true;
            // 
            // Clearbutton
            // 
            this.Clearbutton.Location = new System.Drawing.Point(474, 789);
            this.Clearbutton.Name = "Clearbutton";
            this.Clearbutton.Size = new System.Drawing.Size(202, 22);
            this.Clearbutton.TabIndex = 14;
            this.Clearbutton.Text = "Rensa urval";
            this.Clearbutton.UseVisualStyleBackColor = true;
            this.Clearbutton.Click += new System.EventHandler(this.Clearbutton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(471, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 17);
            this.label1.TabIndex = 15;
            this.label1.Text = "Lärosäten";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(471, 252);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 17);
            this.label2.TabIndex = 16;
            this.label2.Text = "Län";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(471, 468);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 17);
            this.label3.TabIndex = 17;
            this.label3.Text = "Kommuner";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(692, 17);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(22, 17);
            this.label4.TabIndex = 18;
            this.label4.Text = "År";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(695, 251);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(52, 17);
            this.label5.TabIndex = 19;
            this.label5.Text = "Ämnen";
            // 
            // LB_subjectgroup
            // 
            this.LB_subjectgroup.CheckOnClick = true;
            this.LB_subjectgroup.FormattingEnabled = true;
            this.LB_subjectgroup.Location = new System.Drawing.Point(695, 485);
            this.LB_subjectgroup.Name = "LB_subjectgroup";
            this.LB_subjectgroup.Size = new System.Drawing.Size(197, 208);
            this.LB_subjectgroup.Sorted = true;
            this.LB_subjectgroup.TabIndex = 20;
            // 
            // LB_sector
            // 
            this.LB_sector.CheckOnClick = true;
            this.LB_sector.FormattingEnabled = true;
            this.LB_sector.Location = new System.Drawing.Point(697, 715);
            this.LB_sector.Name = "LB_sector";
            this.LB_sector.Size = new System.Drawing.Size(195, 106);
            this.LB_sector.Sorted = true;
            this.LB_sector.TabIndex = 21;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(696, 468);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(101, 17);
            this.label6.TabIndex = 22;
            this.label6.Text = "Ämnesgrupper";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(695, 700);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(62, 17);
            this.label7.TabIndex = 23;
            this.label7.Text = "Sektorer";
            // 
            // button5
            // 
            this.button5.Enabled = false;
            this.button5.Location = new System.Drawing.Point(952, 365);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(154, 75);
            this.button5.TabIndex = 24;
            this.button5.Text = "Antal lärosäten med ämne";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // CBmk
            // 
            this.CBmk.AutoSize = true;
            this.CBmk.Checked = true;
            this.CBmk.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CBmk.Location = new System.Drawing.Point(12, 715);
            this.CBmk.Name = "CBmk";
            this.CBmk.Size = new System.Drawing.Size(106, 21);
            this.CBmk.TabIndex = 25;
            this.CBmk.Text = "Oavsett kön";
            this.CBmk.UseVisualStyleBackColor = true;
            // 
            // CBk
            // 
            this.CBk.AutoSize = true;
            this.CBk.Location = new System.Drawing.Point(12, 742);
            this.CBk.Name = "CBk";
            this.CBk.Size = new System.Drawing.Size(117, 21);
            this.CBk.TabIndex = 26;
            this.CBk.Text = "Könsuppdelat";
            this.CBk.UseVisualStyleBackColor = true;
            // 
            // CBage
            // 
            this.CBage.AutoSize = true;
            this.CBage.Checked = true;
            this.CBage.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CBage.Location = new System.Drawing.Point(144, 715);
            this.CBage.Name = "CBage";
            this.CBage.Size = new System.Drawing.Size(115, 21);
            this.CBage.TabIndex = 28;
            this.CBage.Text = "Oavsett ålder";
            this.CBage.UseVisualStyleBackColor = true;
            // 
            // CB24
            // 
            this.CB24.AutoSize = true;
            this.CB24.Location = new System.Drawing.Point(144, 742);
            this.CB24.Name = "CB24";
            this.CB24.Size = new System.Drawing.Size(125, 21);
            this.CB24.TabIndex = 29;
            this.CB24.Text = "Åldersuppdelat";
            this.CB24.UseVisualStyleBackColor = true;
            this.CB24.CheckedChanged += new System.EventHandler(this.CB24_CheckedChanged);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(951, 446);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(155, 75);
            this.button6.TabIndex = 30;
            this.button6.Text = "Avstånd student-lärosäte";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(0, 0);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(98, 21);
            this.checkBox1.TabIndex = 31;
            this.checkBox1.Text = "checkBox1";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // CB_reg
            // 
            this.CB_reg.AutoSize = true;
            this.CB_reg.Checked = true;
            this.CB_reg.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CB_reg.Location = new System.Drawing.Point(289, 715);
            this.CB_reg.Name = "CB_reg";
            this.CB_reg.Size = new System.Drawing.Size(176, 21);
            this.CB_reg.TabIndex = 32;
            this.CB_reg.Text = "Registrerade studenter";
            this.CB_reg.UseVisualStyleBackColor = true;
            this.CB_reg.CheckedChanged += new System.EventHandler(this.CB_reg_CheckedChanged);
            // 
            // CB_hst
            // 
            this.CB_hst.AutoSize = true;
            this.CB_hst.Location = new System.Drawing.Point(289, 742);
            this.CB_hst.Name = "CB_hst";
            this.CB_hst.Size = new System.Drawing.Size(58, 21);
            this.CB_hst.TabIndex = 33;
            this.CB_hst.Text = "HST";
            this.CB_hst.UseVisualStyleBackColor = true;
            // 
            // CB_hpr
            // 
            this.CB_hpr.AutoSize = true;
            this.CB_hpr.Location = new System.Drawing.Point(289, 769);
            this.CB_hpr.Name = "CB_hpr";
            this.CB_hpr.Size = new System.Drawing.Size(59, 21);
            this.CB_hpr.TabIndex = 34;
            this.CB_hpr.Text = "HPR";
            this.CB_hpr.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1118, 824);
            this.Controls.Add(this.CB_hpr);
            this.Controls.Add(this.CB_hst);
            this.Controls.Add(this.CB_reg);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.CB24);
            this.Controls.Add(this.CBage);
            this.Controls.Add(this.CBk);
            this.Controls.Add(this.CBmk);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.LB_sector);
            this.Controls.Add(this.LB_subjectgroup);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Clearbutton);
            this.Controls.Add(this.CBabsolute);
            this.Controls.Add(this.CBmarketshare);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.LB_subject);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.LB_year);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.LB_lan);
            this.Controls.Add(this.LB_kommun);
            this.Controls.Add(this.LB_uni);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.quitbutton);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button quitbutton;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.CheckedListBox LB_uni;
        private System.Windows.Forms.CheckedListBox LB_kommun;
        private System.Windows.Forms.CheckedListBox LB_lan;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.CheckedListBox LB_year;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.CheckedListBox LB_subject;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.CheckBox CBmarketshare;
        private System.Windows.Forms.CheckBox CBabsolute;
        private System.Windows.Forms.Button Clearbutton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckedListBox LB_subjectgroup;
        private System.Windows.Forms.CheckedListBox LB_sector;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.CheckBox CBmk;
        private System.Windows.Forms.CheckBox CBk;
        private System.Windows.Forms.CheckBox CBage;
        private System.Windows.Forms.CheckBox CB24;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.CheckBox CB_reg;
        private System.Windows.Forms.CheckBox CB_hst;
        private System.Windows.Forms.CheckBox CB_hpr;
    }
}

