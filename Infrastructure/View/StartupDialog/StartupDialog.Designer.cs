namespace A320_Cockpit.Infrastructure.View.StartupDialog
{
    partial class StartupDialog
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
            combobox_port = new ComboBox();
            combobox_aircraft = new ComboBox();
            label1 = new Label();
            label2 = new Label();
            button_start = new Button();
            button_cancel = new Button();
            label3 = new Label();
            checkBox_monitoring = new CheckBox();
            SuspendLayout();
            // 
            // combobox_port
            // 
            combobox_port.DropDownStyle = ComboBoxStyle.DropDownList;
            combobox_port.FormattingEnabled = true;
            combobox_port.Location = new Point(78, 53);
            combobox_port.Name = "combobox_port";
            combobox_port.Size = new Size(196, 23);
            combobox_port.TabIndex = 0;
            // 
            // combobox_aircraft
            // 
            combobox_aircraft.DropDownStyle = ComboBoxStyle.DropDownList;
            combobox_aircraft.FormattingEnabled = true;
            combobox_aircraft.Location = new Point(78, 82);
            combobox_aircraft.Name = "combobox_aircraft";
            combobox_aircraft.Size = new Size(196, 23);
            combobox_aircraft.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 56);
            label1.Name = "label1";
            label1.Size = new Size(60, 15);
            label1.TabIndex = 2;
            label1.Text = "COM Port";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(34, 85);
            label2.Name = "label2";
            label2.Size = new Size(38, 15);
            label2.TabIndex = 3;
            label2.Text = "Avion";
            // 
            // button_start
            // 
            button_start.Location = new Point(199, 140);
            button_start.Name = "button_start";
            button_start.Size = new Size(75, 23);
            button_start.TabIndex = 4;
            button_start.Text = "Demarrer";
            button_start.UseVisualStyleBackColor = true;
            button_start.Click += Button_start_Click;
            // 
            // button_cancel
            // 
            button_cancel.Location = new Point(118, 140);
            button_cancel.Name = "button_cancel";
            button_cancel.Size = new Size(75, 23);
            button_cancel.TabIndex = 5;
            button_cancel.Text = "Annuler";
            button_cancel.UseVisualStyleBackColor = true;
            button_cancel.Click += Button_cancel_Click;
            // 
            // label3
            // 
            label3.Location = new Point(12, 9);
            label3.Name = "label3";
            label3.Size = new Size(262, 41);
            label3.TabIndex = 6;
            label3.Text = "Avant de démarrer, assurez-vous que le cockpit est allimenté et que le MSFS est lancé.";
            // 
            // checkBox_monitoring
            // 
            checkBox_monitoring.AutoSize = true;
            checkBox_monitoring.Location = new Point(78, 111);
            checkBox_monitoring.Name = "checkBox_monitoring";
            checkBox_monitoring.Size = new Size(134, 19);
            checkBox_monitoring.TabIndex = 7;
            checkBox_monitoring.Text = "Ouvrir le monitoring";
            checkBox_monitoring.UseVisualStyleBackColor = true;
            // 
            // StartupDialog
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(286, 175);
            ControlBox = false;
            Controls.Add(checkBox_monitoring);
            Controls.Add(label3);
            Controls.Add(button_cancel);
            Controls.Add(button_start);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(combobox_aircraft);
            Controls.Add(combobox_port);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "StartupDialog";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "A320 Home Cockpit";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox combobox_port;
        private ComboBox combobox_aircraft;
        private Label label1;
        private Label label2;
        private Button button_start;
        private Button button_cancel;
        private Label label3;
        private CheckBox checkBox_monitoring;
    }
}