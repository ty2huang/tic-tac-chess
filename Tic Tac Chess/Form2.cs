using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Tic_Tac_Chess
{
    public partial class Form2 : Form
    {
        public string ChosenChessPiece;
        public Form2()
        {
            InitializeComponent();
        }

        //turns the pawn into a rook
        private void btnPutRook_Click(object sender, EventArgs e)
        {
            ChosenChessPiece = "Rook";
            this.Close();
        }

        //turns the pawn into a bishop
        private void btnPutBishop_Click(object sender, EventArgs e)
        {
            ChosenChessPiece = "Bishop";
            this.Close();
        }

        //turns the pawn into a knight
        private void btnPutKnight_Click(object sender, EventArgs e)
        {
            ChosenChessPiece = "Knight";
            this.Close();
        }

        //turns the pawn into a queen
        private void btnPutQueen_Click(object sender, EventArgs e)
        {
            ChosenChessPiece = "Queen";
            this.Close();
        }
    }
}
