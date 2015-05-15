namespace Tic_Tac_Chess
{
    partial class Form2
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnPutRook = new System.Windows.Forms.Button();
            this.btnPutBishop = new System.Windows.Forms.Button();
            this.btnPutKnight = new System.Windows.Forms.Button();
            this.btnPutQueen = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnPutQueen);
            this.groupBox1.Controls.Add(this.btnPutKnight);
            this.groupBox1.Controls.Add(this.btnPutBishop);
            this.groupBox1.Controls.Add(this.btnPutRook);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(206, 115);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Which piece would you like to add?";
            // 
            // btnPutRook
            // 
            this.btnPutRook.Location = new System.Drawing.Point(21, 33);
            this.btnPutRook.Name = "btnPutRook";
            this.btnPutRook.Size = new System.Drawing.Size(75, 23);
            this.btnPutRook.TabIndex = 0;
            this.btnPutRook.Text = "Rook";
            this.btnPutRook.UseVisualStyleBackColor = true;
            this.btnPutRook.Click += new System.EventHandler(this.btnPutRook_Click);
            // 
            // btnPutBishop
            // 
            this.btnPutBishop.Location = new System.Drawing.Point(109, 33);
            this.btnPutBishop.Name = "btnPutBishop";
            this.btnPutBishop.Size = new System.Drawing.Size(75, 23);
            this.btnPutBishop.TabIndex = 1;
            this.btnPutBishop.Text = "Bishop";
            this.btnPutBishop.UseVisualStyleBackColor = true;
            this.btnPutBishop.Click += new System.EventHandler(this.btnPutBishop_Click);
            // 
            // btnPutKnight
            // 
            this.btnPutKnight.Location = new System.Drawing.Point(21, 77);
            this.btnPutKnight.Name = "btnPutKnight";
            this.btnPutKnight.Size = new System.Drawing.Size(75, 23);
            this.btnPutKnight.TabIndex = 2;
            this.btnPutKnight.Text = "Knight";
            this.btnPutKnight.UseVisualStyleBackColor = true;
            this.btnPutKnight.Click += new System.EventHandler(this.btnPutKnight_Click);
            // 
            // btnPutQueen
            // 
            this.btnPutQueen.Location = new System.Drawing.Point(109, 77);
            this.btnPutQueen.Name = "btnPutQueen";
            this.btnPutQueen.Size = new System.Drawing.Size(75, 23);
            this.btnPutQueen.TabIndex = 3;
            this.btnPutQueen.Text = "Queen";
            this.btnPutQueen.UseVisualStyleBackColor = true;
            this.btnPutQueen.Click += new System.EventHandler(this.btnPutQueen_Click);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(234, 148);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form2";
            this.Text = "Choose A Piece";
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnPutQueen;
        private System.Windows.Forms.Button btnPutKnight;
        private System.Windows.Forms.Button btnPutBishop;
        private System.Windows.Forms.Button btnPutRook;
    }
}