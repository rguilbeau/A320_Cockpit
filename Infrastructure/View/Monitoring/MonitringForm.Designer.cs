namespace A320_Cockpit.Infrastructure.View.Monitoring
{
    partial class MonitringForm
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
            panel1 = new Panel();
            label_timing = new Label();
            progressBar = new ProgressBar();
            label_sim = new Label();
            label3 = new Label();
            label_canbus = new Label();
            label1 = new Label();
            panel2 = new Panel();
            splitContainer1 = new SplitContainer();
            richTextBox_frames = new RichTextBox();
            richTextBox_errors = new RichTextBox();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            panel1.BackColor = SystemColors.Control;
            panel1.Controls.Add(label_timing);
            panel1.Controls.Add(progressBar);
            panel1.Controls.Add(label_sim);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(label_canbus);
            panel1.Controls.Add(label1);
            panel1.Location = new Point(12, 12);
            panel1.Name = "panel1";
            panel1.Size = new Size(552, 35);
            panel1.TabIndex = 0;
            // 
            // label_timing
            // 
            label_timing.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            label_timing.Location = new Point(492, 9);
            label_timing.Name = "label_timing";
            label_timing.Size = new Size(57, 19);
            label_timing.TabIndex = 5;
            label_timing.Text = "0ms";
            label_timing.TextAlign = ContentAlignment.MiddleRight;
            // 
            // progressBar
            // 
            progressBar.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            progressBar.Location = new Point(185, 7);
            progressBar.MarqueeAnimationSpeed = 0;
            progressBar.Name = "progressBar";
            progressBar.Size = new Size(301, 23);
            progressBar.Style = ProgressBarStyle.Continuous;
            progressBar.TabIndex = 4;
            // 
            // label_sim
            // 
            label_sim.AutoSize = true;
            label_sim.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label_sim.ForeColor = Color.Red;
            label_sim.Location = new Point(76, 15);
            label_sim.Name = "label_sim";
            label_sim.Size = new Size(75, 15);
            label_sim.TabIndex = 3;
            label_sim.Text = "Déconnecté";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(3, 15);
            label3.Name = "label3";
            label3.Size = new Size(64, 15);
            label3.TabIndex = 2;
            label3.Text = "Simulateur";
            // 
            // label_canbus
            // 
            label_canbus.AutoSize = true;
            label_canbus.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label_canbus.ForeColor = Color.Red;
            label_canbus.Location = new Point(76, 0);
            label_canbus.Name = "label_canbus";
            label_canbus.Size = new Size(75, 15);
            label_canbus.TabIndex = 1;
            label_canbus.Text = "Déconnecté";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(3, 0);
            label1.Name = "label1";
            label1.Size = new Size(54, 15);
            label1.TabIndex = 0;
            label1.Text = "CAN bus";
            // 
            // panel2
            // 
            panel2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panel2.BackColor = SystemColors.Control;
            panel2.Controls.Add(splitContainer1);
            panel2.Location = new Point(12, 53);
            panel2.Name = "panel2";
            panel2.Size = new Size(552, 542);
            panel2.TabIndex = 1;
            // 
            // splitContainer1
            // 
            splitContainer1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            splitContainer1.Location = new Point(0, 0);
            splitContainer1.Name = "splitContainer1";
            splitContainer1.Orientation = Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.BackColor = SystemColors.Control;
            splitContainer1.Panel1.Controls.Add(richTextBox_frames);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.BackColor = SystemColors.Control;
            splitContainer1.Panel2.Controls.Add(richTextBox_errors);
            splitContainer1.Size = new Size(549, 542);
            splitContainer1.SplitterDistance = 332;
            splitContainer1.TabIndex = 0;
            // 
            // richTextBox_frames
            // 
            richTextBox_frames.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            richTextBox_frames.BackColor = SystemColors.Window;
            richTextBox_frames.Font = new Font("Consolas", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            richTextBox_frames.Location = new Point(3, 3);
            richTextBox_frames.Name = "richTextBox_frames";
            richTextBox_frames.ReadOnly = true;
            richTextBox_frames.Size = new Size(543, 326);
            richTextBox_frames.TabIndex = 0;
            richTextBox_frames.Text = "";
            richTextBox_frames.WordWrap = false;
            // 
            // richTextBox_errors
            // 
            richTextBox_errors.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            richTextBox_errors.BackColor = SystemColors.Window;
            richTextBox_errors.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point);
            richTextBox_errors.ForeColor = Color.Red;
            richTextBox_errors.Location = new Point(3, 3);
            richTextBox_errors.Name = "richTextBox_errors";
            richTextBox_errors.ReadOnly = true;
            richTextBox_errors.Size = new Size(546, 200);
            richTextBox_errors.TabIndex = 0;
            richTextBox_errors.Text = "";
            richTextBox_errors.WordWrap = false;
            // 
            // MonitringForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(576, 607);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Name = "MonitringForm";
            Text = "MonitringForm";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Panel panel2;
        private SplitContainer splitContainer1;
        private RichTextBox richTextBox_frames;
        private RichTextBox richTextBox_errors;
        private Label label_sim;
        private Label label3;
        private Label label_canbus;
        private Label label1;
        private ProgressBar progressBar;
        private Label label_timing;
    }
}