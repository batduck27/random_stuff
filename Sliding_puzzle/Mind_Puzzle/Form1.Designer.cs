namespace WindowsFormsApplication1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.Timp = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.Mutari = new System.Windows.Forms.Label();
            this.treix = new System.Windows.Forms.Button();
            this.patrux = new System.Windows.Forms.Button();
            this.New_game = new System.Windows.Forms.Button();
            this.About = new System.Windows.Forms.Button();
            this.playground = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.Timp);
            this.groupBox1.Location = new System.Drawing.Point(274, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(98, 50);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Timp:";
            // 
            // Timp
            // 
            this.Timp.AutoSize = true;
            this.Timp.Location = new System.Drawing.Point(13, 21);
            this.Timp.Name = "Timp";
            this.Timp.Size = new System.Drawing.Size(0, 13);
            this.Timp.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.Mutari);
            this.groupBox2.Location = new System.Drawing.Point(12, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(90, 50);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Mutari:";
            // 
            // Mutari
            // 
            this.Mutari.AutoSize = true;
            this.Mutari.Location = new System.Drawing.Point(6, 21);
            this.Mutari.Name = "Mutari";
            this.Mutari.Size = new System.Drawing.Size(0, 13);
            this.Mutari.TabIndex = 6;
            this.Mutari.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // treix
            // 
            this.treix.Enabled = false;
            this.treix.Location = new System.Drawing.Point(108, 12);
            this.treix.Name = "treix";
            this.treix.Size = new System.Drawing.Size(77, 21);
            this.treix.TabIndex = 2;
            this.treix.Text = "3 x 3";
            this.treix.UseVisualStyleBackColor = true;
            this.treix.Click += new System.EventHandler(this.treix_Click);
            // 
            // patrux
            // 
            this.patrux.Location = new System.Drawing.Point(191, 12);
            this.patrux.Name = "patrux";
            this.patrux.Size = new System.Drawing.Size(77, 21);
            this.patrux.TabIndex = 3;
            this.patrux.Text = "4 x 4";
            this.patrux.UseVisualStyleBackColor = true;
            this.patrux.Click += new System.EventHandler(this.patrux_Click);
            // 
            // New_game
            // 
            this.New_game.Location = new System.Drawing.Point(108, 39);
            this.New_game.Name = "New_game";
            this.New_game.Size = new System.Drawing.Size(77, 21);
            this.New_game.TabIndex = 4;
            this.New_game.Text = "New Game";
            this.New_game.UseVisualStyleBackColor = true;
            this.New_game.Click += new System.EventHandler(this.New_game_Click);
            // 
            // About
            // 
            this.About.Location = new System.Drawing.Point(191, 39);
            this.About.Name = "About";
            this.About.Size = new System.Drawing.Size(77, 21);
            this.About.TabIndex = 5;
            this.About.Text = "About";
            this.About.UseVisualStyleBackColor = true;
            this.About.Click += new System.EventHandler(this.About_Click);
            // 
            // playground
            // 
            this.playground.Location = new System.Drawing.Point(12, 70);
            this.playground.Name = "playground";
            this.playground.Size = new System.Drawing.Size(360, 360);
            this.playground.TabIndex = 6;
            this.playground.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 442);
            this.Controls.Add(this.playground);
            this.Controls.Add(this.About);
            this.Controls.Add(this.New_game);
            this.Controls.Add(this.patrux);
            this.Controls.Add(this.treix);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(392, 476);
            this.MinimumSize = new System.Drawing.Size(392, 476);
            this.Name = "Form1";
            this.Text = "Sliding Puzzle";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button treix;
        private System.Windows.Forms.Button patrux;
        private System.Windows.Forms.Button New_game;
        private System.Windows.Forms.Button About;
        private System.Windows.Forms.Label Timp;
        private System.Windows.Forms.Label Mutari;
        private System.Windows.Forms.GroupBox playground;
    }
}

