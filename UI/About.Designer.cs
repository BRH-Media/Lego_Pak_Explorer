using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Lego_Pak_Explorer.Properties;

namespace Lego_Pak_Explorer.UI
{
    public partial class About
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
                components.Dispose();
            base.Dispose(disposing);
        }
        private void InitializeComponent()
        {
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            button1 = new Button();
            label6 = new Label();
            label7 = new Label();
            label8 = new Label();
            label9 = new Label();
            pictureBox1 = new PictureBox();
            label10 = new Label();
            ((ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            label1.AutoSize = true;
            label1.Font = new Font("Microsoft Sans Serif", 9.75f, FontStyle.Bold | FontStyle.Underline, GraphicsUnit.Point, 0);
            label1.Location = new Point(72, 21);
            label1.Name = "label1";
            label1.Size = new Size(163, 16);
            label1.TabIndex = 1;
            label1.Text = "LEGO Games Explorer";
            label2.AutoSize = true;
            label2.Location = new Point(188, 42);
            label2.Name = "label2";
            label2.Size = new Size(47, 13);
            label2.TabIndex = 2;
            label2.Text = "by Ac_K";
            label3.AutoSize = true;
            label3.Location = new Point(72, 42);
            label3.Name = "label3";
            label3.Size = new Size(28, 13);
            label3.TabIndex = 3;
            label3.Text = "v0.1";
            label4.AutoSize = true;
            label4.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold | FontStyle.Underline, GraphicsUnit.Point, 0);
            label4.Location = new Point(19, 95);
            label4.Name = "label4";
            label4.Size = new Size(50, 13);
            label4.TabIndex = 4;
            label4.Text = "Credits:";
            label5.AutoSize = true;
            label5.Location = new Point(22, 111);
            label5.MaximumSize = new Size(220, 0);
            label5.Name = "label5";
            label5.Size = new Size(218, 26);
            label5.TabIndex = 5;
            label5.Text = "● Luigi Auriemma ( BMS Script of TT Games, LZ2K Compression and DFLT Compression )";
            button1.Location = new Point(165, 238);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 6;
            button1.Text = "OK";
            button1.UseVisualStyleBackColor = true;
            button1.Click += Button1_Click;
            label6.AutoSize = true;
            label6.Location = new Point(74, 73);
            label6.Name = "label6";
            label6.Size = new Size(120, 13);
            label6.TabIndex = 7;
            label6.Text = "pakexplorer@gmail.com";
            label7.AutoSize = true;
            label7.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold | FontStyle.Underline, GraphicsUnit.Point, 0);
            label7.Location = new Point(19, 73);
            label7.Name = "label7";
            label7.Size = new Size(55, 13);
            label7.TabIndex = 8;
            label7.Text = "Contact:";
            label8.AutoSize = true;
            label8.Location = new Point(21, 140);
            label8.MaximumSize = new Size(220, 0);
            label8.Name = "label8";
            label8.Size = new Size(211, 26);
            label8.TabIndex = 9;
            label8.Text = "● Pavel Torgashov ( Fast Colored TextBox for Syntax Highlighting )";
            label9.AutoSize = true;
            label9.Location = new Point(20, 170);
            label9.MaximumSize = new Size(220, 0);
            label9.Name = "label9";
            label9.Size = new Size(142, 13);
            label9.TabIndex = 10;
            label9.Text = "● Kons ( Basic DDS Library )";
            pictureBox1.Image = Resources.BlackFigure;
            pictureBox1.Location = new Point(22, 21);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(35, 34);
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            label10.AutoSize = true;
            label10.Location = new Point(20, 188);
            label10.MaximumSize = new Size(220, 0);
            label10.Name = "label10";
            label10.Size = new Size(211, 26);
            label10.TabIndex = 11;
            label10.Text = "● FamFamFam and Dave Brasgalla ( Icons Pack )";
            AutoScaleDimensions = new SizeF(6f, 13f);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(256, 277);
            Controls.Add(label10);
            Controls.Add(label9);
            Controls.Add(label8);
            Controls.Add(label7);
            Controls.Add(label6);
            Controls.Add(button1);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(pictureBox1);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = @"About";
            ShowIcon = false;
            ShowInTaskbar = false;
            SizeGripStyle = SizeGripStyle.Hide;
            StartPosition = FormStartPosition.CenterParent;
            Text = "About";
            ((ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private PictureBox pictureBox1;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Button button1;
        private Label label6;
        private Label label7;
        private Label label8;
        private Label label9;
        private Label label10;
    }
}
