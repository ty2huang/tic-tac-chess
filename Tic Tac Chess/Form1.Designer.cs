namespace Tic_Tac_Chess
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
            this.btnStart = new System.Windows.Forms.Button();
            this.gbAddPiece = new System.Windows.Forms.GroupBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnKing = new System.Windows.Forms.Button();
            this.btnQueen = new System.Windows.Forms.Button();
            this.btnBishop = new System.Windows.Forms.Button();
            this.btnKnight = new System.Windows.Forms.Button();
            this.btnRook = new System.Windows.Forms.Button();
            this.btnPawn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtPawn = new System.Windows.Forms.TextBox();
            this.txtRook = new System.Windows.Forms.TextBox();
            this.txtKnight = new System.Windows.Forms.TextBox();
            this.txtBishop = new System.Windows.Forms.TextBox();
            this.txtQueen = new System.Windows.Forms.TextBox();
            this.txtKing = new System.Windows.Forms.TextBox();
            this.lblPieces = new System.Windows.Forms.Label();
            this.lblTurn = new System.Windows.Forms.Label();
            this.TicTacToeBoard = new System.Windows.Forms.PictureBox();
            this.gbAddPiece.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TicTacToeBoard)).BeginInit();
            this.SuspendLayout();
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(84, 258);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 1;
            this.btnStart.Text = "New Game";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // gbAddPiece
            // 
            this.gbAddPiece.Controls.Add(this.btnCancel);
            this.gbAddPiece.Controls.Add(this.btnKing);
            this.gbAddPiece.Controls.Add(this.btnQueen);
            this.gbAddPiece.Controls.Add(this.btnBishop);
            this.gbAddPiece.Controls.Add(this.btnKnight);
            this.gbAddPiece.Controls.Add(this.btnRook);
            this.gbAddPiece.Controls.Add(this.btnPawn);
            this.gbAddPiece.Location = new System.Drawing.Point(246, 38);
            this.gbAddPiece.Name = "gbAddPiece";
            this.gbAddPiece.Size = new System.Drawing.Size(172, 138);
            this.gbAddPiece.TabIndex = 2;
            this.gbAddPiece.TabStop = false;
            this.gbAddPiece.Text = "Add a Piece";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(6, 106);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(156, 23);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnKing
            // 
            this.btnKing.Location = new System.Drawing.Point(87, 77);
            this.btnKing.Name = "btnKing";
            this.btnKing.Size = new System.Drawing.Size(75, 23);
            this.btnKing.TabIndex = 5;
            this.btnKing.Text = "King";
            this.btnKing.UseVisualStyleBackColor = true;
            this.btnKing.Click += new System.EventHandler(this.btnKing_Click);
            // 
            // btnQueen
            // 
            this.btnQueen.Location = new System.Drawing.Point(6, 77);
            this.btnQueen.Name = "btnQueen";
            this.btnQueen.Size = new System.Drawing.Size(75, 23);
            this.btnQueen.TabIndex = 4;
            this.btnQueen.Text = "Queen";
            this.btnQueen.UseVisualStyleBackColor = true;
            this.btnQueen.Click += new System.EventHandler(this.btnQueen_Click);
            // 
            // btnBishop
            // 
            this.btnBishop.Location = new System.Drawing.Point(87, 48);
            this.btnBishop.Name = "btnBishop";
            this.btnBishop.Size = new System.Drawing.Size(75, 23);
            this.btnBishop.TabIndex = 3;
            this.btnBishop.Text = "Bishop";
            this.btnBishop.UseVisualStyleBackColor = true;
            this.btnBishop.Click += new System.EventHandler(this.btnBishop_Click);
            // 
            // btnKnight
            // 
            this.btnKnight.Location = new System.Drawing.Point(6, 48);
            this.btnKnight.Name = "btnKnight";
            this.btnKnight.Size = new System.Drawing.Size(75, 23);
            this.btnKnight.TabIndex = 2;
            this.btnKnight.Text = "Knight";
            this.btnKnight.UseVisualStyleBackColor = true;
            this.btnKnight.Click += new System.EventHandler(this.btnKnight_Click);
            // 
            // btnRook
            // 
            this.btnRook.Location = new System.Drawing.Point(87, 19);
            this.btnRook.Name = "btnRook";
            this.btnRook.Size = new System.Drawing.Size(75, 23);
            this.btnRook.TabIndex = 1;
            this.btnRook.Text = "Rook";
            this.btnRook.UseVisualStyleBackColor = true;
            this.btnRook.Click += new System.EventHandler(this.btnRook_Click);
            // 
            // btnPawn
            // 
            this.btnPawn.Location = new System.Drawing.Point(6, 19);
            this.btnPawn.Name = "btnPawn";
            this.btnPawn.Size = new System.Drawing.Size(75, 23);
            this.btnPawn.TabIndex = 0;
            this.btnPawn.Text = "Pawn";
            this.btnPawn.UseVisualStyleBackColor = true;
            this.btnPawn.Click += new System.EventHandler(this.btnPawn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(249, 210);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Pawn";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(249, 232);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(33, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Rook";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(249, 254);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Knight";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(336, 210);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(39, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Bishop";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(336, 232);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(39, 13);
            this.label5.TabIndex = 7;
            this.label5.Text = "Queen";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(336, 254);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(28, 13);
            this.label6.TabIndex = 8;
            this.label6.Text = "King";
            // 
            // txtPawn
            // 
            this.txtPawn.Enabled = false;
            this.txtPawn.Location = new System.Drawing.Point(290, 207);
            this.txtPawn.Name = "txtPawn";
            this.txtPawn.Size = new System.Drawing.Size(36, 20);
            this.txtPawn.TabIndex = 9;
            // 
            // txtRook
            // 
            this.txtRook.Enabled = false;
            this.txtRook.Location = new System.Drawing.Point(290, 229);
            this.txtRook.Name = "txtRook";
            this.txtRook.Size = new System.Drawing.Size(36, 20);
            this.txtRook.TabIndex = 10;
            // 
            // txtKnight
            // 
            this.txtKnight.Enabled = false;
            this.txtKnight.Location = new System.Drawing.Point(290, 251);
            this.txtKnight.Name = "txtKnight";
            this.txtKnight.Size = new System.Drawing.Size(36, 20);
            this.txtKnight.TabIndex = 11;
            // 
            // txtBishop
            // 
            this.txtBishop.Enabled = false;
            this.txtBishop.Location = new System.Drawing.Point(377, 207);
            this.txtBishop.Name = "txtBishop";
            this.txtBishop.Size = new System.Drawing.Size(36, 20);
            this.txtBishop.TabIndex = 12;
            // 
            // txtQueen
            // 
            this.txtQueen.Enabled = false;
            this.txtQueen.Location = new System.Drawing.Point(377, 229);
            this.txtQueen.Name = "txtQueen";
            this.txtQueen.Size = new System.Drawing.Size(36, 20);
            this.txtQueen.TabIndex = 13;
            // 
            // txtKing
            // 
            this.txtKing.Enabled = false;
            this.txtKing.Location = new System.Drawing.Point(377, 251);
            this.txtKing.Name = "txtKing";
            this.txtKing.Size = new System.Drawing.Size(36, 20);
            this.txtKing.TabIndex = 14;
            // 
            // lblPieces
            // 
            this.lblPieces.AutoSize = true;
            this.lblPieces.Location = new System.Drawing.Point(249, 189);
            this.lblPieces.Name = "lblPieces";
            this.lblPieces.Size = new System.Drawing.Size(60, 13);
            this.lblPieces.TabIndex = 15;
            this.lblPieces.Text = "Pieces Left";
            // 
            // lblTurn
            // 
            this.lblTurn.AutoSize = true;
            this.lblTurn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTurn.Location = new System.Drawing.Point(286, 11);
            this.lblTurn.Name = "lblTurn";
            this.lblTurn.Size = new System.Drawing.Size(0, 16);
            this.lblTurn.TabIndex = 16;
            // 
            // TicTacToeBoard
            // 
            this.TicTacToeBoard.BackColor = System.Drawing.Color.White;
            this.TicTacToeBoard.Enabled = false;
            this.TicTacToeBoard.Location = new System.Drawing.Point(0, 0);
            this.TicTacToeBoard.Name = "TicTacToeBoard";
            this.TicTacToeBoard.Size = new System.Drawing.Size(240, 240);
            this.TicTacToeBoard.TabIndex = 0;
            this.TicTacToeBoard.TabStop = false;
            this.TicTacToeBoard.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseClick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(435, 299);
            this.Controls.Add(this.lblTurn);
            this.Controls.Add(this.lblPieces);
            this.Controls.Add(this.txtKing);
            this.Controls.Add(this.txtQueen);
            this.Controls.Add(this.txtBishop);
            this.Controls.Add(this.txtKnight);
            this.Controls.Add(this.txtRook);
            this.Controls.Add(this.txtPawn);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.gbAddPiece);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.TicTacToeBoard);
            this.Name = "Form1";
            this.Text = "Tic Tac Chess";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseMove);
            this.gbAddPiece.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.TicTacToeBoard)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox TicTacToeBoard;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.GroupBox gbAddPiece;
        private System.Windows.Forms.Button btnKing;
        private System.Windows.Forms.Button btnQueen;
        private System.Windows.Forms.Button btnBishop;
        private System.Windows.Forms.Button btnKnight;
        private System.Windows.Forms.Button btnRook;
        private System.Windows.Forms.Button btnPawn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtPawn;
        private System.Windows.Forms.TextBox txtRook;
        private System.Windows.Forms.TextBox txtKnight;
        private System.Windows.Forms.TextBox txtBishop;
        private System.Windows.Forms.TextBox txtQueen;
        private System.Windows.Forms.TextBox txtKing;
        private System.Windows.Forms.Label lblPieces;
        private System.Windows.Forms.Label lblTurn;
        private System.Windows.Forms.Button btnCancel;
    }
}

