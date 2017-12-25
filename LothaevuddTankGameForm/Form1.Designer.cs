namespace LothaevuddTankGameForm
{
    partial class LothauvuddTankGame
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
            this.tmrGameTimer = new System.Windows.Forms.Timer(this.components);
            this.pnlScreen = new System.Windows.Forms.Panel();
            this.txtLast = new System.Windows.Forms.TextBox();
            this.cboColour = new System.Windows.Forms.ComboBox();
            this.lblColour = new System.Windows.Forms.Label();
            this.lblInitial = new System.Windows.Forms.Label();
            this.txtFirst = new System.Windows.Forms.TextBox();
            this.btnHighScores = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnInstructions = new System.Windows.Forms.Button();
            this.btnLoad = new System.Windows.Forms.Button();
            this.pnlScreen.SuspendLayout();
            this.SuspendLayout();
            // 
            // tmrGameTimer
            // 
            this.tmrGameTimer.Interval = 16;
            this.tmrGameTimer.Tick += new System.EventHandler(this.tmrGameTimer_Tick);
            // 
            // pnlScreen
            // 
            this.pnlScreen.Controls.Add(this.btnLoad);
            this.pnlScreen.Controls.Add(this.txtLast);
            this.pnlScreen.Controls.Add(this.cboColour);
            this.pnlScreen.Controls.Add(this.lblColour);
            this.pnlScreen.Controls.Add(this.lblInitial);
            this.pnlScreen.Controls.Add(this.txtFirst);
            this.pnlScreen.Controls.Add(this.btnHighScores);
            this.pnlScreen.Controls.Add(this.btnStart);
            this.pnlScreen.Controls.Add(this.btnInstructions);
            this.pnlScreen.Location = new System.Drawing.Point(-2, 0);
            this.pnlScreen.Name = "pnlScreen";
            this.pnlScreen.Size = new System.Drawing.Size(786, 464);
            this.pnlScreen.TabIndex = 0;
            // 
            // txtLast
            // 
            this.txtLast.Location = new System.Drawing.Point(301, 63);
            this.txtLast.Name = "txtLast";
            this.txtLast.Size = new System.Drawing.Size(20, 20);
            this.txtLast.TabIndex = 8;
            // 
            // cboColour
            // 
            this.cboColour.FormattingEnabled = true;
            this.cboColour.Items.AddRange(new object[] {
            "Black",
            "White",
            "Blue",
            "Orange",
            "Red",
            "Yellow",
            "Green",
            "Purple"});
            this.cboColour.Location = new System.Drawing.Point(278, 107);
            this.cboColour.Name = "cboColour";
            this.cboColour.Size = new System.Drawing.Size(121, 21);
            this.cboColour.TabIndex = 7;
            // 
            // lblColour
            // 
            this.lblColour.AutoSize = true;
            this.lblColour.Location = new System.Drawing.Point(198, 107);
            this.lblColour.Name = "lblColour";
            this.lblColour.Size = new System.Drawing.Size(74, 13);
            this.lblColour.TabIndex = 6;
            this.lblColour.Text = "Tank Colo(u)r:";
            // 
            // lblInitial
            // 
            this.lblInitial.AutoSize = true;
            this.lblInitial.Location = new System.Drawing.Point(234, 66);
            this.lblInitial.Name = "lblInitial";
            this.lblInitial.Size = new System.Drawing.Size(39, 13);
            this.lblInitial.TabIndex = 5;
            this.lblInitial.Text = "Initials:";
            // 
            // txtFirst
            // 
            this.txtFirst.Location = new System.Drawing.Point(276, 63);
            this.txtFirst.Name = "txtFirst";
            this.txtFirst.Size = new System.Drawing.Size(20, 20);
            this.txtFirst.TabIndex = 3;
            // 
            // btnHighScores
            // 
            this.btnHighScores.Location = new System.Drawing.Point(347, 426);
            this.btnHighScores.Name = "btnHighScores";
            this.btnHighScores.Size = new System.Drawing.Size(75, 23);
            this.btnHighScores.TabIndex = 1;
            this.btnHighScores.Text = "High Scores";
            this.btnHighScores.UseVisualStyleBackColor = true;
            this.btnHighScores.Click += new System.EventHandler(this.btnHighScores_Click);
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(699, 426);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 2;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnInstructions
            // 
            this.btnInstructions.Location = new System.Drawing.Point(14, 426);
            this.btnInstructions.Name = "btnInstructions";
            this.btnInstructions.Size = new System.Drawing.Size(75, 23);
            this.btnInstructions.TabIndex = 0;
            this.btnInstructions.Text = "Instructions";
            this.btnInstructions.UseVisualStyleBackColor = true;
            this.btnInstructions.Click += new System.EventHandler(this.btnInstructions_Click);
            // 
            // btnLoad
            // 
            this.btnLoad.Location = new System.Drawing.Point(169, 426);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(75, 23);
            this.btnLoad.TabIndex = 9;
            this.btnLoad.Text = "Load Game";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // LothauvuddTankGame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(784, 461);
            this.Controls.Add(this.pnlScreen);
            this.Cursor = System.Windows.Forms.Cursors.Cross;
            this.DoubleBuffered = true;
            this.MaximizeBox = false;
            this.Name = "LothauvuddTankGame";
            this.Text = "Lothaevudd Tank Game v0.00";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.LothaevuddTankGame_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.LothaevuddTankGame_KeyUp);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.LothauvuddTankGame_MouseClick);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.LothaevuddTankGame_MouseMove);
            this.pnlScreen.ResumeLayout(false);
            this.pnlScreen.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer tmrGameTimer;
        private System.Windows.Forms.Panel pnlScreen;
        private System.Windows.Forms.ComboBox cboColour;
        private System.Windows.Forms.Label lblColour;
        private System.Windows.Forms.Label lblInitial;
        private System.Windows.Forms.TextBox txtFirst;
        private System.Windows.Forms.Button btnHighScores;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnInstructions;
        private System.Windows.Forms.TextBox txtLast;
        private System.Windows.Forms.Button btnLoad;
    }
}

