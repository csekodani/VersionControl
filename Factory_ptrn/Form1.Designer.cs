namespace Factory_ptrn
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
            this.fileSystemWatcher1 = new System.IO.FileSystemWatcher();
            this.mainPanel = new System.Windows.Forms.Panel();
            this.conveyorTimer = new System.Windows.Forms.Timer(this.components);
            this.createTimer = new System.Windows.Forms.Timer(this.components);
            this.carBtn = new System.Windows.Forms.Button();
            this.ballBtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).BeginInit();
            this.SuspendLayout();
            // 
            // fileSystemWatcher1
            // 
            this.fileSystemWatcher1.EnableRaisingEvents = true;
            this.fileSystemWatcher1.SynchronizingObject = this;
            this.fileSystemWatcher1.Changed += new System.IO.FileSystemEventHandler(this.fileSystemWatcher1_Changed);
            // 
            // mainPanel
            // 
            this.mainPanel.Location = new System.Drawing.Point(0, 119);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(752, 335);
            this.mainPanel.TabIndex = 0;
            // 
            // conveyorTimer
            // 
            this.conveyorTimer.Enabled = true;
            this.conveyorTimer.Interval = 10;
            this.conveyorTimer.Tick += new System.EventHandler(this.conveyorTimer_Tick);
            // 
            // createTimer
            // 
            this.createTimer.Enabled = true;
            this.createTimer.Interval = 3000;
            this.createTimer.Tick += new System.EventHandler(this.createTimer_Tick);
            // 
            // carBtn
            // 
            this.carBtn.Location = new System.Drawing.Point(111, 24);
            this.carBtn.Name = "carBtn";
            this.carBtn.Size = new System.Drawing.Size(75, 23);
            this.carBtn.TabIndex = 1;
            this.carBtn.Text = "Car";
            this.carBtn.UseVisualStyleBackColor = true;
            this.carBtn.Click += new System.EventHandler(this.carBtn_Click);
            // 
            // ballBtn
            // 
            this.ballBtn.Location = new System.Drawing.Point(220, 24);
            this.ballBtn.Name = "ballBtn";
            this.ballBtn.Size = new System.Drawing.Size(75, 23);
            this.ballBtn.TabIndex = 2;
            this.ballBtn.Text = "Ball";
            this.ballBtn.UseVisualStyleBackColor = true;
            this.ballBtn.Click += new System.EventHandler(this.ballBtn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Coming Next:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ballBtn);
            this.Controls.Add(this.carBtn);
            this.Controls.Add(this.mainPanel);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.IO.FileSystemWatcher fileSystemWatcher1;
        private System.Windows.Forms.Panel mainPanel;
        private System.Windows.Forms.Timer conveyorTimer;
        private System.Windows.Forms.Timer createTimer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button ballBtn;
        private System.Windows.Forms.Button carBtn;
    }
}

