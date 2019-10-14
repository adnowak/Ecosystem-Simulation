namespace Grass_simulation
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
            this.components = new System.ComponentModel.Container();
            this.NextRoundButton = new System.Windows.Forms.Button();
            this.StartButton = new System.Windows.Forms.Button();
            this.Timer = new System.Windows.Forms.Timer(this.components);
            this.StopButton = new System.Windows.Forms.Button();
            this.SheepButon = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // NextRoundButton
            // 
            this.NextRoundButton.Location = new System.Drawing.Point(551, 293);
            this.NextRoundButton.Name = "NextRoundButton";
            this.NextRoundButton.Size = new System.Drawing.Size(75, 23);
            this.NextRoundButton.TabIndex = 0;
            this.NextRoundButton.Text = "Next";
            this.NextRoundButton.UseVisualStyleBackColor = true;
            this.NextRoundButton.Click += new System.EventHandler(this.NextRoundButton_Click);
            // 
            // StartButton
            // 
            this.StartButton.Location = new System.Drawing.Point(551, 322);
            this.StartButton.Name = "StartButton";
            this.StartButton.Size = new System.Drawing.Size(75, 23);
            this.StartButton.TabIndex = 1;
            this.StartButton.Text = "Start";
            this.StartButton.UseVisualStyleBackColor = true;
            this.StartButton.Click += new System.EventHandler(this.StartButton_Click);
            // 
            // Timer
            // 
            this.Timer.Interval = 500;
            this.Timer.Tick += new System.EventHandler(this.Timer_Tick);
            // 
            // StopButton
            // 
            this.StopButton.Location = new System.Drawing.Point(551, 351);
            this.StopButton.Name = "StopButton";
            this.StopButton.Size = new System.Drawing.Size(75, 23);
            this.StopButton.TabIndex = 2;
            this.StopButton.Text = "Stop";
            this.StopButton.UseVisualStyleBackColor = true;
            this.StopButton.Click += new System.EventHandler(this.StopButton_Click);
            // 
            // SheepButon
            // 
            this.SheepButon.Location = new System.Drawing.Point(551, 380);
            this.SheepButon.Name = "SheepButon";
            this.SheepButon.Size = new System.Drawing.Size(75, 23);
            this.SheepButon.TabIndex = 3;
            this.SheepButon.Text = "Add sheep";
            this.SheepButon.UseVisualStyleBackColor = true;
            this.SheepButon.Click += new System.EventHandler(this.SheepButon_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(668, 480);
            this.Controls.Add(this.SheepButon);
            this.Controls.Add(this.StopButton);
            this.Controls.Add(this.StartButton);
            this.Controls.Add(this.NextRoundButton);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button NextRoundButton;
        private System.Windows.Forms.Button StartButton;
        private System.Windows.Forms.Timer Timer;
        private System.Windows.Forms.Button StopButton;
        private System.Windows.Forms.Button SheepButon;
    }
}

