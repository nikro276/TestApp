
namespace TestApp
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.ChooseFileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.BusCountLabel = new System.Windows.Forms.Label();
            this.StationsCountLabel = new System.Windows.Forms.Label();
            this.Panel = new System.Windows.Forms.Panel();
            this.StartStationTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.EndStationTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.StartTimeTextBox = new System.Windows.Forms.TextBox();
            this.CalculateButton = new System.Windows.Forms.Button();
            this.ResultPanel = new System.Windows.Forms.Panel();
            this.ResultLabel = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.ResultPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ChooseFileMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // ChooseFileMenuItem
            // 
            this.ChooseFileMenuItem.Name = "ChooseFileMenuItem";
            this.ChooseFileMenuItem.Size = new System.Drawing.Size(98, 20);
            this.ChooseFileMenuItem.Text = "Выбрать файл";
            this.ChooseFileMenuItem.Click += new System.EventHandler(this.ChooseFile);
            // 
            // BusCountLabel
            // 
            this.BusCountLabel.AutoSize = true;
            this.BusCountLabel.Location = new System.Drawing.Point(12, 60);
            this.BusCountLabel.Name = "BusCountLabel";
            this.BusCountLabel.Size = new System.Drawing.Size(137, 15);
            this.BusCountLabel.TabIndex = 1;
            this.BusCountLabel.Text = "Количество автобусов: ";
            // 
            // StationsCountLabel
            // 
            this.StationsCountLabel.AutoSize = true;
            this.StationsCountLabel.Location = new System.Drawing.Point(12, 84);
            this.StationsCountLabel.Name = "StationsCountLabel";
            this.StationsCountLabel.Size = new System.Drawing.Size(138, 15);
            this.StationsCountLabel.TabIndex = 2;
            this.StationsCountLabel.Text = "Количество остановок: ";
            // 
            // Panel
            // 
            this.Panel.AutoScroll = true;
            this.Panel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Panel.Location = new System.Drawing.Point(12, 113);
            this.Panel.Name = "Panel";
            this.Panel.Size = new System.Drawing.Size(600, 100);
            this.Panel.TabIndex = 3;
            // 
            // StartStationTextBox
            // 
            this.StartStationTextBox.Location = new System.Drawing.Point(19, 257);
            this.StartStationTextBox.Name = "StartStationTextBox";
            this.StartStationTextBox.Size = new System.Drawing.Size(86, 23);
            this.StartStationTextBox.TabIndex = 4;
            this.StartStationTextBox.TextChanged += new System.EventHandler(this.StationChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 239);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(126, 15);
            this.label1.TabIndex = 5;
            this.label1.Text = "Начальная остановка";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 283);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(119, 15);
            this.label2.TabIndex = 6;
            this.label2.Text = "Конечная остановка";
            // 
            // EndStationTextBox
            // 
            this.EndStationTextBox.Location = new System.Drawing.Point(19, 301);
            this.EndStationTextBox.Name = "EndStationTextBox";
            this.EndStationTextBox.Size = new System.Drawing.Size(86, 23);
            this.EndStationTextBox.TabIndex = 7;
            this.EndStationTextBox.TextChanged += new System.EventHandler(this.StationChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(203, 239);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(116, 15);
            this.label3.TabIndex = 8;
            this.label3.Text = "Время отправления";
            // 
            // StartTimeTextBox
            // 
            this.StartTimeTextBox.Location = new System.Drawing.Point(214, 257);
            this.StartTimeTextBox.Name = "StartTimeTextBox";
            this.StartTimeTextBox.Size = new System.Drawing.Size(88, 23);
            this.StartTimeTextBox.TabIndex = 9;
            this.StartTimeTextBox.TextChanged += new System.EventHandler(this.StartTimeChanged);
            // 
            // CalculateButton
            // 
            this.CalculateButton.Location = new System.Drawing.Point(7, 13);
            this.CalculateButton.Name = "CalculateButton";
            this.CalculateButton.Size = new System.Drawing.Size(86, 28);
            this.CalculateButton.TabIndex = 10;
            this.CalculateButton.Text = "Рассчитать";
            this.CalculateButton.UseVisualStyleBackColor = true;
            this.CalculateButton.Click += new System.EventHandler(this.Calculate);
            // 
            // ResultPanel
            // 
            this.ResultPanel.Controls.Add(this.ResultLabel);
            this.ResultPanel.Controls.Add(this.CalculateButton);
            this.ResultPanel.Location = new System.Drawing.Point(12, 347);
            this.ResultPanel.Name = "ResultPanel";
            this.ResultPanel.Size = new System.Drawing.Size(600, 121);
            this.ResultPanel.TabIndex = 11;
            // 
            // ResultLabel
            // 
            this.ResultLabel.AutoSize = true;
            this.ResultLabel.Location = new System.Drawing.Point(7, 48);
            this.ResultLabel.MaximumSize = new System.Drawing.Size(550, 1000);
            this.ResultLabel.Name = "ResultLabel";
            this.ResultLabel.Size = new System.Drawing.Size(0, 15);
            this.ResultLabel.TabIndex = 11;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 499);
            this.Controls.Add(this.ResultPanel);
            this.Controls.Add(this.StartTimeTextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.EndStationTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.StartStationTextBox);
            this.Controls.Add(this.Panel);
            this.Controls.Add(this.StationsCountLabel);
            this.Controls.Add(this.BusCountLabel);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResultPanel.ResumeLayout(false);
            this.ResultPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem ChooseFileMenuItem;
        private System.Windows.Forms.Label BusCountLabel;
        private System.Windows.Forms.Label StationsCountLabel;
        private System.Windows.Forms.Panel Panel;
        private System.Windows.Forms.TextBox StartStationTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox EndStationTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox StartTimeTextBox;
        private System.Windows.Forms.Button CalculateButton;
        private System.Windows.Forms.Panel ResultPanel;
        private System.Windows.Forms.Label ResultLabel;
    }
}

