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
    public partial class Form1 : Form
    {
        //declare all public variables
        public bool WhiteTurn = true;
        public string[,] chessPieceLocation = new string[3, 3];
        public string chessPieceToAdd = "";
        public bool[,] White = new bool[3, 3];
        public bool[,] Black = new bool[3, 3];
        public bool[,] PossiblePlacesToMove = new bool[3, 3];
        public bool KingUnderCheck = false;
        public bool ThreeIsThere = false;
        public int[] ChessPieceCount = { 8, 2, 2, 2, 1, 1, 8, 2, 2, 2, 1, 1 };
        public int xPieceToMove = 0;
        public int yPieceToMove = 0;
        public int WhiteCount = 0;
        public int BlackCount = 0;
        Bitmap bitmapImage;
        Color[,] ImageArray = new Color[240, 240];
        Color[,] ChessPieceArray = new Color[60, 60];

        public Form1()
        {
            InitializeComponent();
        }

        public void StartNewGame()
        {
            //The board is disabled by default, and when new game starts, the board is enabled
            TicTacToeBoard.Enabled = true;

            //Sets all the array of location of black and white pieces to false. Also, white goes first
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                {
                    White[i, j] = false;
                    Black[i, j] = false;
                }
            WhiteTurn = true;

            //Gets the tic tac toe image saved in the debugging folder
            Image TicTacToe = Image.FromFile(@"TicTacToeBoard.png");
            bitmapImage = new Bitmap(TicTacToe, 240, 240);
            TicTacToeBoard.Image = bitmapImage;

            //Saves the pixels of the image into a picture array
            for (int row = 0; row < 240; row++)
                for (int col = 0; col < 240; col++)
                {
                    ImageArray[row, col] = bitmapImage.GetPixel(row, col);
                }

            //Clears all the location of chess piece names
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                {
                    chessPieceLocation[i, j] = "";
                }

            //Sets the text to white's turn, the three in a row and checking is false
            lblTurn.Text = "White's Turn";
            KingUnderCheck = false;
            ThreeIsThere = false;
            //set all the number of black and white pieces on the board to 0
            WhiteCount = 0;
            BlackCount = 0;
            //Reset the number of each pieces back to their original amount
            int[] OriginalCount = { 8, 2, 2, 2, 1, 1, 8, 2, 2, 2, 1, 1 };
            for (int i = 0; i < 12; i++)
                ChessPieceCount[i] = OriginalCount[i];
            UpdatePieceCountTextBoxes();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            //Restarts the game
            StartNewGame();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        public void ThreeIsStillThere()
        {

        }

        public void ClearPlacesToMove()
        {
            //turns all the green squares back into white using image processing and saves it onto the image
            for (int row = 0; row < 240; row++)
                for (int col = 0; col < 240; col++)
                {
                    if (ImageArray[row, col] == Color.PaleGreen)
                        ImageArray[row, col] = Color.FromArgb(255, 255, 255, 255);
                }

            SetBitmapFromArray();
            TicTacToeBoard.Image = bitmapImage;
        }

        public void ClearAvailableSpaceToAdd()
        {
            //turns all brown squares back into white using graphics and saves it onto the image
            int Width = TicTacToeBoard.Width;
            int Length = TicTacToeBoard.Height;
            Graphics g = Graphics.FromHwnd(TicTacToeBoard.Handle);
            SolidBrush whiteBrush = new SolidBrush(Color.White);

            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                {
                    if (chessPieceLocation[i, j] == "")
                        g.FillRectangle(whiteBrush, i * Length / 3 + 1, j * Width / 3 + 1, 77, 77);
                }
        }

        public void SetPossiblePlacesToMoveToFalse()
        {
            //Clears the possible places a piece can move, usually for another piece later on
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    PossiblePlacesToMove[i, j] = false;
        }

        public void ChessPiecesForChecking(int row, int col)
        {
            //White pawns can check a piece one square diagonally from bottom up
            if (chessPieceLocation[row, col] == "Pawn" && White[row, col] == true)
            {
                if (row - 1 > -1 && col - 1 > -1)
                        PossiblePlacesToMove[row - 1, col - 1] = true;
                if (row + 1 < 3 && col - 1 > -1)
                        PossiblePlacesToMove[row + 1, col - 1] = true;
            }

            //Black pawns can move check a piece one square diagonally from top down
            if (chessPieceLocation[row, col] == "Pawn" && Black[row, col] == true)
            {
                if (row - 1 > -1 && col + 1 < 3)
                        PossiblePlacesToMove[row - 1, col + 1] = true;
                if (row + 1 < 3 && col + 1 < 3)
                        PossiblePlacesToMove[row + 1, col + 1] = true;
            }

            //Rooks can move vertically or horizontally until they meet another piece or reach the end of the board
            if (chessPieceLocation[row, col] == "Rook" && White[row, col] == true)
            {
                for (int i = row + 1; i < 3; i++)
                {
                    if (White[i, col] == true)
                    {
                        PossiblePlacesToMove[i, col] = true;
                        break;
                    }
                    else if (Black[i, col] == true)
                    {
                        PossiblePlacesToMove[i, col] = true;
                        break;
                    }
                    else
                        PossiblePlacesToMove[i, col] = true;
                }
                for (int i = row - 1; i >= 0; i--)
                {
                    if (White[i, col] == true)
                    {
                        PossiblePlacesToMove[i, col] = true;
                        break;
                    }
                    else if (Black[i, col] == true)
                    {
                        PossiblePlacesToMove[i, col] = true;
                        break;
                    }
                    else
                        PossiblePlacesToMove[i, col] = true;
                }
                for (int i = col + 1; i < 3; i++)
                {
                    if (White[row, i] == true)
                    {
                        PossiblePlacesToMove[row, i] = true;
                        break;
                    }
                    else if (Black[row, i] == true)
                    {
                        PossiblePlacesToMove[row, i] = true;
                        break;
                    }
                    else
                        PossiblePlacesToMove[row, i] = true;
                }
                for (int i = col - 1; i >= 0; i--)
                {
                    if (White[row, i] == true)
                    {
                        PossiblePlacesToMove[row, i] = true;
                        break;
                    }
                    else if (Black[row, i] == true)
                    {
                        PossiblePlacesToMove[row, i] = true;
                        break;
                    }
                    else
                        PossiblePlacesToMove[row, i] = true;
                }
            }

            if (chessPieceLocation[row, col] == "Rook" && Black[row, col] == true)
            {
                for (int i = row + 1; i < 3; i++)
                {
                    if (Black[i, col] == true)
                    {
                        PossiblePlacesToMove[i, col] = true;
                        break;
                    }
                    else if (White[i, col] == true)
                    {
                        PossiblePlacesToMove[i, col] = true;
                        break;
                    }
                    else
                        PossiblePlacesToMove[i, col] = true;
                }
                for (int i = row - 1; i >= 0; i--)
                {
                    if (Black[i, col] == true)
                    {
                        PossiblePlacesToMove[i, col] = true;
                        break;
                    }
                    else if (White[i, col] == true)
                    {
                        PossiblePlacesToMove[i, col] = true;
                        break;
                    }
                    else
                        PossiblePlacesToMove[i, col] = true;
                }
                for (int i = col + 1; i < 3; i++)
                {
                    if (Black[row, i] == true)
                    {
                        PossiblePlacesToMove[row, i] = true;
                        break;
                    }
                    else if (White[row, i] == true)
                    {
                        PossiblePlacesToMove[row, i] = true;
                        break;
                    }
                    else
                        PossiblePlacesToMove[row, i] = true;
                }
                for (int i = col - 1; i >= 0; i--)
                {
                    if (Black[row, i] == true)
                    {
                        PossiblePlacesToMove[row, i] = true;
                        break;
                    }
                    else if (White[row, i] == true)
                    {
                        PossiblePlacesToMove[row, i] = true;
                        break;
                    }
                    else
                        PossiblePlacesToMove[row, i] = true;
                }
            }

            //Bishops can move diagonally until they reach another piece or the end of the board
            if (chessPieceLocation[row, col] == "Bishop" && White[row, col] == true)
            {
                int j = col + 1;
                for (int i = row + 1; i < 3 && j < 3; i++)
                {
                    if (White[i, j] == true)
                    {
                        PossiblePlacesToMove[i, j] = true;
                        break;
                    }
                    else if (Black[i, j] == true)
                    {
                        PossiblePlacesToMove[i, j] = true;
                        break;
                    }
                    else
                        PossiblePlacesToMove[i, j] = true;
                    j++;
                }
                j = col + 1;
                for (int i = row - 1; i >= 0 && j < 3; i--)
                {
                    if (White[i, j] == true)
                    {
                        PossiblePlacesToMove[i, j] = true;
                        break;
                    }
                    else if (Black[i, j] == true)
                    {
                        PossiblePlacesToMove[i, j] = true;
                        break;
                    }
                    else
                        PossiblePlacesToMove[i, j] = true;
                    j++;
                }
                j = col - 1;
                for (int i = row + 1; i < 3 && j >= 0; i++)
                {
                    if (White[i, j] == true)
                    {
                        PossiblePlacesToMove[i, j] = true;
                        break;
                    }
                    else if (Black[i, j] == true)
                    {
                        PossiblePlacesToMove[i, j] = true;
                        break;
                    }
                    else
                        PossiblePlacesToMove[i, j] = true;
                    j--;
                }
                j = col - 1;
                for (int i = row - 1; i >= 0 && j >= 0; i--)
                {
                    if (White[i, j] == true)
                    {
                        PossiblePlacesToMove[i, j] = true;
                        break;
                    }
                    else if (Black[i, j] == true)
                    {
                        PossiblePlacesToMove[i, j] = true;
                        break;
                    }
                    else
                        PossiblePlacesToMove[i, j] = true;
                    j--;
                }
            }

            if (chessPieceLocation[row, col] == "Bishop" && Black[row, col] == true)
            {
                int j = col + 1;
                for (int i = row + 1; i < 3 && j < 3; i++)
                {
                    if (Black[i, j] == true)
                    {
                        PossiblePlacesToMove[i, j] = true;
                        break;
                    }
                    else if (White[i, j] == true)
                    {
                        PossiblePlacesToMove[i, j] = true;
                        break;
                    }
                    else
                        PossiblePlacesToMove[i, j] = true;
                    j++;
                }
                j = col + 1;
                for (int i = row - 1; i >= 0 && j < 3; i--)
                {
                    if (Black[i, j] == true)
                    {
                        PossiblePlacesToMove[i, j] = true;
                        break;
                    }
                    else if (White[i, j] == true)
                    {
                        PossiblePlacesToMove[i, j] = true;
                        break;
                    }
                    else
                        PossiblePlacesToMove[i, j] = true;
                    j++;
                }
                j = col - 1;
                for (int i = row + 1; i < 3 && j >= 0; i++)
                {
                    if (Black[i, j] == true)
                    {
                        PossiblePlacesToMove[i, j] = true;
                        break;
                    }
                    else if (White[i, j] == true)
                    {
                        PossiblePlacesToMove[i, j] = true;
                        break;
                    }
                    else
                        PossiblePlacesToMove[i, j] = true;
                    j--;
                }
                j = col - 1;
                for (int i = row - 1; i >= 0 && j >= 0; i--)
                {
                    if (Black[i, j] == true)
                    {
                        PossiblePlacesToMove[i, j] = true;
                        break;
                    }
                    else if (White[i, j] == true)
                    {
                        PossiblePlacesToMove[i, j] = true;
                        break;
                    }
                    else
                        PossiblePlacesToMove[i, j] = true;
                    j--;
                }
            }

            //Knights can move to the spot 2 squares in one direction vertically or horizontally and 1 square perpendicular to the direction
            if (chessPieceLocation[row, col] == "Knight")
                for (int i = 0; i < 3; i++)
                    for (int j = 0; j < 3; j++)
                    {
                        if (White[row, col] == true)
                            if (((Math.Abs(i - row) == 2 && Math.Abs(j - col) == 1) || (Math.Abs(i - row) == 1 && Math.Abs(j - col) == 2)))
                                PossiblePlacesToMove[i, j] = true;
                        if (Black[row, col] == true)
                            if (((Math.Abs(i - row) == 2 && Math.Abs(j - col) == 1) || (Math.Abs(i - row) == 1 && Math.Abs(j - col) == 2)))
                                PossiblePlacesToMove[i, j] = true;
                    }

            //Queens can move vertically, horizontally, or diagonally until they reach another piece or the end of the board
            if (chessPieceLocation[row, col] == "Queen" && White[row, col] == true)
            {
                int j = col + 1;
                for (int i = row + 1; i < 3 && j < 3; i++)
                {
                    if (White[i, j] == true)
                    {
                        PossiblePlacesToMove[i, j] = true;
                        break;
                    }
                    else if (Black[i, j] == true)
                    {
                        PossiblePlacesToMove[i, j] = true;
                        break;
                    }
                    else
                        PossiblePlacesToMove[i, j] = true;
                    j++;
                }
                j = col + 1;
                for (int i = row - 1; i >= 0 && j < 3; i--)
                {
                    if (White[i, j] == true)
                    {
                        PossiblePlacesToMove[i, j] = true;
                        break;
                    }
                    else if (Black[i, j] == true)
                    {
                        PossiblePlacesToMove[i, j] = true;
                        break;
                    }
                    else
                        PossiblePlacesToMove[i, j] = true;
                    j++;
                }
                j = col - 1;
                for (int i = row + 1; i < 3 && j >= 0; i++)
                {
                    if (White[i, j] == true)
                    {
                        PossiblePlacesToMove[i, j] = true;
                        break;
                    }
                    else if (Black[i, j] == true)
                    {
                        PossiblePlacesToMove[i, j] = true;
                        break;
                    }
                    else
                        PossiblePlacesToMove[i, j] = true;
                    j--;
                }
                j = col - 1;
                for (int i = row - 1; i >= 0 && j >= 0; i--)
                {
                    if (White[i, j] == true)
                    {
                        PossiblePlacesToMove[i, j] = true;
                        break;
                    }
                    else if (Black[i, j] == true)
                    {
                        PossiblePlacesToMove[i, j] = true;
                        break;
                    }
                    else
                        PossiblePlacesToMove[i, j] = true;
                    j--;
                }
                for (int i = row + 1; i < 3; i++)
                {
                    if (White[i, col] == true)
                    {
                        PossiblePlacesToMove[i, col] = true;
                        break;
                    }
                    else if (Black[i, col] == true)
                    {
                        PossiblePlacesToMove[i, col] = true;
                        break;
                    }
                    else
                        PossiblePlacesToMove[i, col] = true;
                }
                for (int i = row - 1; i >= 0; i--)
                {
                    if (White[i, col] == true)
                    {
                        PossiblePlacesToMove[i, col] = true;
                        break;
                    }
                    else if (Black[i, col] == true)
                    {
                        PossiblePlacesToMove[i, col] = true;
                        break;
                    }
                    else
                        PossiblePlacesToMove[i, col] = true;
                }
                for (int i = col + 1; i < 3; i++)
                {
                    if (White[row, i] == true)
                    {
                        PossiblePlacesToMove[row,i] = true;
                        break;
                    }
                    else if (Black[row, i] == true)
                    {
                        PossiblePlacesToMove[row, i] = true;
                        break;
                    }
                    else
                        PossiblePlacesToMove[row, i] = true;
                }
                for (int i = col - 1; i >= 0; i--)
                {
                    if (White[row, i] == true)
                    {
                        PossiblePlacesToMove[row, i] = true;
                        break;
                    }
                    else if (Black[row, i] == true)
                    {
                        PossiblePlacesToMove[row, i] = true;
                        break;
                    }
                    else
                        PossiblePlacesToMove[row, i] = true;
                }
            }

            if (chessPieceLocation[row, col] == "Queen" && Black[row, col] == true)
            {
                int j = col + 1;
                for (int i = row + 1; i < 3 && j < 3; i++)
                {
                    if (Black[i, j] == true)
                    {
                        PossiblePlacesToMove[i, j] = true;
                        break;
                    }
                    else if (White[i, j] == true)
                    {
                        PossiblePlacesToMove[i, j] = true;
                        break;
                    }
                    else
                        PossiblePlacesToMove[i, j] = true;
                    j++;
                }
                j = col + 1;
                for (int i = row - 1; i >= 0 && j < 3; i--)
                {
                    if (Black[i, j] == true)
                    {
                        PossiblePlacesToMove[i, j] = true;
                        break;
                    }
                    else if (White[i, j] == true)
                    {
                        PossiblePlacesToMove[i, j] = true;
                        break;
                    }
                    else
                        PossiblePlacesToMove[i, j] = true;
                    j++;
                }
                j = col - 1;
                for (int i = row + 1; i < 3 && j >= 0; i++)
                {
                    if (Black[i, j] == true)
                    {
                        PossiblePlacesToMove[i, j] = true;
                        break;
                    }
                    else if (White[i, j] == true)
                    {
                        PossiblePlacesToMove[i, j] = true;
                        break;
                    }
                    else
                        PossiblePlacesToMove[i, j] = true;
                    j--;
                }
                j = col - 1;
                for (int i = row - 1; i >= 0 && j >= 0; i--)
                {
                    if (Black[i, j] == true)
                    {
                        PossiblePlacesToMove[i, j] = true;
                        break;
                    }
                    else if (White[i, j] == true)
                    {
                        PossiblePlacesToMove[i, j] = true;
                        break;
                    }
                    else
                        PossiblePlacesToMove[i, j] = true;
                    j--;
                }
                for (int i = row + 1; i < 3; i++)
                {
                    if (Black[i, col] == true)
                    {
                        PossiblePlacesToMove[i, col] = true;
                        break;
                    }
                    else if (White[i, col] == true)
                    {
                        PossiblePlacesToMove[i, col] = true;
                        break;
                    }
                    else
                        PossiblePlacesToMove[i, col] = true;
                }
                for (int i = row - 1; i >= 0; i--)
                {
                    if (Black[i, col] == true)
                    {
                        PossiblePlacesToMove[i, col] = true;
                        break;
                    }
                    else if (White[i, col] == true)
                    {
                        PossiblePlacesToMove[i, col] = true;
                        break;
                    }
                    else
                        PossiblePlacesToMove[i, col] = true;
                }
                for (int i = col + 1; i < 3; i++)
                {
                    if (Black[row, i] == true)
                    {
                        PossiblePlacesToMove[row, i] = true;
                        break;
                    }
                    else if (White[row, i] == true)
                    {
                        PossiblePlacesToMove[row, i] = true;
                        break;
                    }
                    else
                        PossiblePlacesToMove[row, i] = true;
                }
                for (int i = col - 1; i >= 0; i--)
                {
                    if (Black[row, i] == true)
                    {
                        PossiblePlacesToMove[row, i] = true;
                        break;
                    }
                    else if (White[row, i] == true)
                    {
                        PossiblePlacesToMove[row, i] = true;
                        break;
                    }
                    else
                        PossiblePlacesToMove[row, i] = true;
                }
            }

            //Kings can check anywhere one square around
            if (chessPieceLocation[row, col] == "King")
            {
                for (int i = 0; i < 3; i++)
                    for (int j = 0; j < 3; j++)
                    {
                        if (White[row, col] == true)
                            if (IsWhitePieceChecked(i, j) == true)
                                PossiblePlacesToMove[i, j] = true;
                        if (Black[row, col] == true)
                            if (IsBlackPieceChecked(i, j) == true)
                                PossiblePlacesToMove[i, j] = true;
                    }

                for (int i = 0; i < 3; i++)
                    for (int j = 0; j < 3; j++)
                    {
                        if (PossiblePlacesToMove[i, j] == false) { PossiblePlacesToMove[i, j] = true; }
                        else if (PossiblePlacesToMove[i, j] == true) { PossiblePlacesToMove[i, j] = false; }
                        if (Math.Abs(i - row) > 1 || Math.Abs(j - col) > 1)
                            PossiblePlacesToMove[i, j] = false;
                        if (White[row, col] == true && White[i, j] == true)
                            PossiblePlacesToMove[i, j] = false;
                        if (Black[row, col] == true && Black[i, j] == true)
                            PossiblePlacesToMove[i, j] = false;
                        PossiblePlacesToMove[row, col] = false;
                    }
            }
        }

        public void SetPossiblePlacesToMove(int row, int col)
        {
            //Pawns can move forward one square and can only take pieces diagonally one square
            if (chessPieceLocation[row, col] == "Pawn" && White[row, col] == true)
            {
                if (chessPieceLocation[row, col - 1] == "")
                    PossiblePlacesToMove[row, col - 1] = true;
                if (row - 1 > -1 && col - 1 > -1)
                    if (Black[row - 1, col - 1] == true)
                        PossiblePlacesToMove[row - 1, col - 1] = true;
                if (row + 1 < 3 && col - 1 > -1)
                    if (Black[row + 1, col - 1] == true)
                        PossiblePlacesToMove[row + 1, col - 1] = true;
            }

            if (chessPieceLocation[row, col] == "Pawn" && Black[row, col] == true)
            {
                if (chessPieceLocation[row, col + 1] == "")
                    PossiblePlacesToMove[row, col + 1] = true;
                if (row - 1 > -1 && col + 1 < 3)
                    if (White[row - 1, col + 1] == true)
                        PossiblePlacesToMove[row - 1, col + 1] = true;
                if (row + 1 < 3 && col + 1 < 3)
                    if (White[row + 1, col + 1] == true)
                        PossiblePlacesToMove[row + 1, col + 1] = true;
            }

            //Rooks move horizontally or vertically
            if (chessPieceLocation[row, col] == "Rook" && White[row, col] == true)
            {
                for (int i = row + 1; i < 3; i++)
                {
                    if (White[i, col] == true)
                        break;
                    else if (Black[i, col] == true)
                    {
                        PossiblePlacesToMove[i, col] = true;
                        break;
                    }
                    else
                        PossiblePlacesToMove[i, col] = true;
                }
                for (int i = row - 1; i >= 0; i--)
                {
                    if (White[i, col] == true)
                        break;
                    else if (Black[i, col] == true)
                    {
                        PossiblePlacesToMove[i, col] = true;
                        break;
                    }
                    else
                        PossiblePlacesToMove[i, col] = true;
                }
                for (int i = col + 1; i < 3; i++)
                {
                    if (White[row, i] == true)
                        break;
                    else if (Black[row, i] == true)
                    {
                        PossiblePlacesToMove[row, i] = true;
                        break;
                    }
                    else
                        PossiblePlacesToMove[row, i] = true;
                }
                for (int i = col - 1; i >= 0; i--)
                {
                    if (White[row, i] == true)
                        break;
                    else if (Black[row, i] == true)
                    {
                        PossiblePlacesToMove[row, i] = true;
                        break;
                    }
                    else
                        PossiblePlacesToMove[row, i] = true;
                }
            }

            if (chessPieceLocation[row, col] == "Rook" && Black[row, col] == true)
            {
                for (int i = row + 1; i < 3; i++)
                {
                    if (Black[i, col] == true)
                        break;
                    else if (White[i, col] == true)
                    {
                        PossiblePlacesToMove[i, col] = true;
                        break;
                    }
                    else
                        PossiblePlacesToMove[i, col] = true;
                }
                for (int i = row - 1; i >= 0; i--)
                {
                    if (Black[i, col] == true)
                        break;
                    else if (White[i, col] == true)
                    {
                        PossiblePlacesToMove[i, col] = true;
                        break;
                    }
                    else
                        PossiblePlacesToMove[i, col] = true;
                }
                for (int i = col + 1; i < 3; i++)
                {
                    if (Black[row, i] == true)
                        break;
                    else if (White[row, i] == true)
                    {
                        PossiblePlacesToMove[row, i] = true;
                        break;
                    }
                    else
                        PossiblePlacesToMove[row, i] = true;
                }
                for (int i = col - 1; i >= 0; i--)
                {
                    if (Black[row, i] == true)
                        break;
                    else if (White[row, i] == true)
                    {
                        PossiblePlacesToMove[row, i] = true;
                        break;
                    }
                    else
                        PossiblePlacesToMove[row, i] = true;
                }
            }

            //Bishops move diagonally
            if (chessPieceLocation[row, col] == "Bishop" && White[row, col] == true)
            {
                int j = col + 1;
                for (int i = row + 1; i < 3 && j < 3; i++)
                {
                    if (White[i, j] == true)
                        break;
                    else if (Black[i, j] == true)
                    {
                        PossiblePlacesToMove[i, j] = true;
                        break;
                    }
                    else
                        PossiblePlacesToMove[i, j] = true;
                    j++;
                }
                j = col + 1;
                for (int i = row - 1; i >= 0 && j < 3; i--)
                {
                    if (White[i, j] == true)
                        break;
                    else if (Black[i, j] == true)
                    {
                        PossiblePlacesToMove[i, j] = true;
                        break;
                    }
                    else
                        PossiblePlacesToMove[i, j] = true;
                    j++;
                }
                j = col - 1;
                for (int i = row + 1; i < 3 && j >= 0; i++)
                {
                    if (White[i, j] == true)
                        break;
                    else if (Black[i, j] == true)
                    {
                        PossiblePlacesToMove[i, j] = true;
                        break;
                    }
                    else
                        PossiblePlacesToMove[i, j] = true;
                    j--;
                }
                j = col - 1;
                for (int i = row - 1; i >= 0 && j >= 0; i--)
                {
                    if (White[i, j] == true)
                        break;
                    else if (Black[i, j] == true)
                    {
                        PossiblePlacesToMove[i, j] = true;
                        break;
                    }
                    else
                        PossiblePlacesToMove[i, j] = true;
                    j--;
                }
            }

            if (chessPieceLocation[row, col] == "Bishop" && Black[row, col] == true)
            {
                int j = col + 1;
                for (int i = row + 1; i < 3 && j < 3; i++)
                {
                    if (Black[i, j] == true)
                        break;
                    else if (White[i, j] == true)
                    {
                        PossiblePlacesToMove[i, j] = true;
                        break;
                    }
                    else
                        PossiblePlacesToMove[i, j] = true;
                    j++;
                }
                j = col + 1;
                for (int i = row - 1; i >= 0 && j < 3; i--)
                {
                    if (Black[i, j] == true)
                        break;
                    else if (White[i, j] == true)
                    {
                        PossiblePlacesToMove[i, j] = true;
                        break;
                    }
                    else
                        PossiblePlacesToMove[i, j] = true;
                    j++;
                }
                j = col - 1;
                for (int i = row + 1; i < 3 && j >= 0; i++)
                {
                    if (Black[i, j] == true)
                        break;
                    else if (White[i, j] == true)
                    {
                        PossiblePlacesToMove[i, j] = true;
                        break;
                    }
                    else
                        PossiblePlacesToMove[i, j] = true;
                    j--;
                }
                j = col - 1;
                for (int i = row - 1; i >= 0 && j >= 0; i--)
                {
                    if (Black[i, j] == true)
                        break;
                    else if (White[i, j] == true)
                    {
                        PossiblePlacesToMove[i, j] = true;
                        break;
                    }
                    else
                        PossiblePlacesToMove[i, j] = true;
                    j--;
                }
            }

            //Knights move in a L shape
            if (chessPieceLocation[row, col] == "Knight")
                for (int i = 0; i < 3; i++)
                    for (int j = 0; j < 3; j++)
                    {
                        if (White[row, col] == true)
                            if (((Math.Abs(i - row) == 2 && Math.Abs(j - col) == 1) || (Math.Abs(i - row) == 1 && Math.Abs(j - col) == 2)) && (White[i, j] != true))
                                PossiblePlacesToMove[i, j] = true;
                        if (Black[row, col] == true)
                            if (((Math.Abs(i - row) == 2 && Math.Abs(j - col) == 1) || (Math.Abs(i - row) == 1 && Math.Abs(j - col) == 2)) && (Black[i, j] != true))
                                PossiblePlacesToMove[i, j] = true;
                    }

            //Queens can move anywhere horizontally, vertically, or diagonally
            if (chessPieceLocation[row, col] == "Queen" && White[row, col] == true)
            {
                int j = col + 1;
                for (int i = row + 1; i < 3 && j < 3; i++)
                {
                    if (White[i, j] == true)
                        break;
                    else if (Black[i, j] == true)
                    {
                        PossiblePlacesToMove[i, j] = true;
                        break;
                    }
                    else
                        PossiblePlacesToMove[i, j] = true;
                    j++;
                }
                j = col + 1;
                for (int i = row - 1; i >= 0 && j < 3; i--)
                {
                    if (White[i, j] == true)
                        break;
                    else if (Black[i, j] == true)
                    {
                        PossiblePlacesToMove[i, j] = true;
                        break;
                    }
                    else
                        PossiblePlacesToMove[i, j] = true;
                    j++;
                }
                j = col - 1;
                for (int i = row + 1; i < 3 && j >= 0; i++)
                {
                    if (White[i, j] == true)
                        break;
                    else if (Black[i, j] == true)
                    {
                        PossiblePlacesToMove[i, j] = true;
                        break;
                    }
                    else
                        PossiblePlacesToMove[i, j] = true;
                    j--;
                }
                j = col - 1;
                for (int i = row - 1; i >= 0 && j >= 0; i--)
                {
                    if (White[i, j] == true)
                        break;
                    else if (Black[i, j] == true)
                    {
                        PossiblePlacesToMove[i, j] = true;
                        break;
                    }
                    else
                        PossiblePlacesToMove[i, j] = true;
                    j--;
                }
                for (int i = row + 1; i < 3; i++)
                {
                    if (White[i, col] == true)
                        break;
                    else if (Black[i, col] == true)
                    {
                        PossiblePlacesToMove[i, col] = true;
                        break;
                    }
                    else
                        PossiblePlacesToMove[i, col] = true;
                }
                for (int i = row - 1; i >= 0; i--)
                {
                    if (White[i, col] == true)
                        break;
                    else if (Black[i, col] == true)
                    {
                        PossiblePlacesToMove[i, col] = true;
                        break;
                    }
                    else
                        PossiblePlacesToMove[i, col] = true;
                }
                for (int i = col + 1; i < 3; i++)
                {
                    if (White[row, i] == true)
                        break;
                    else if (Black[row, i] == true)
                    {
                        PossiblePlacesToMove[row, i] = true;
                        break;
                    }
                    else
                        PossiblePlacesToMove[row, i] = true;
                }
                for (int i = col - 1; i >= 0; i--)
                {
                    if (White[row, i] == true)
                        break;
                    else if (Black[row, i] == true)
                    {
                        PossiblePlacesToMove[row, i] = true;
                        break;
                    }
                    else
                        PossiblePlacesToMove[row, i] = true;
                }
            }

            if (chessPieceLocation[row, col] == "Queen" && Black[row, col] == true)
            {
                int j = col + 1;
                for (int i = row + 1; i < 3 && j < 3; i++)
                {
                    if (Black[i, j] == true)
                        break;
                    else if (White[i, j] == true)
                    {
                        PossiblePlacesToMove[i, j] = true;
                        break;
                    }
                    else
                        PossiblePlacesToMove[i, j] = true;
                    j++;
                }
                j = col + 1;
                for (int i = row - 1; i >= 0 && j < 3; i--)
                {
                    if (Black[i, j] == true)
                        break;
                    else if (White[i, j] == true)
                    {
                        PossiblePlacesToMove[i, j] = true;
                        break;
                    }
                    else
                        PossiblePlacesToMove[i, j] = true;
                    j++;
                }
                j = col - 1;
                for (int i = row + 1; i < 3 && j >= 0; i++)
                {
                    if (Black[i, j] == true)
                        break;
                    else if (White[i, j] == true)
                    {
                        PossiblePlacesToMove[i, j] = true;
                        break;
                    }
                    else
                        PossiblePlacesToMove[i, j] = true;
                    j--;
                }
                j = col - 1;
                for (int i = row - 1; i >= 0 && j >= 0; i--)
                {
                    if (Black[i, j] == true)
                        break;
                    else if (White[i, j] == true)
                    {
                        PossiblePlacesToMove[i, j] = true;
                        break;
                    }
                    else
                        PossiblePlacesToMove[i, j] = true;
                    j--;
                }
                for (int i = row + 1; i < 3; i++)
                {
                    if (Black[i, col] == true)
                        break;
                    else if (White[i, col] == true)
                    {
                        PossiblePlacesToMove[i, col] = true;
                        break;
                    }
                    else
                        PossiblePlacesToMove[i, col] = true;
                }
                for (int i = row - 1; i >= 0; i--)
                {
                    if (Black[i, col] == true)
                        break;
                    else if (White[i, col] == true)
                    {
                        PossiblePlacesToMove[i, col] = true;
                        break;
                    }
                    else
                        PossiblePlacesToMove[i, col] = true;
                }
                for (int i = col + 1; i < 3; i++)
                {
                    if (Black[row, i] == true)
                        break;
                    else if (White[row, i] == true)
                    {
                        PossiblePlacesToMove[row, i] = true;
                        break;
                    }
                    else
                        PossiblePlacesToMove[row, i] = true;
                }
                for (int i = col - 1; i >= 0; i--)
                {
                    if (Black[row, i] == true)
                        break;
                    else if (White[row, i] == true)
                    {
                        PossiblePlacesToMove[row, i] = true;
                        break;
                    }
                    else
                        PossiblePlacesToMove[row, i] = true;
                }
            }

            //Kings can check anywhere one square around
            if (chessPieceLocation[row, col] == "King")
            {
                for (int i = 0; i < 3; i++)
                    for (int j = 0; j < 3; j++)
                    {
                        if (White[row, col] == true)
                            if (IsWhitePieceChecked(i, j) == true)
                                PossiblePlacesToMove[i, j] = true;
                        if (Black[row, col] == true)
                            if (IsBlackPieceChecked(i, j) == true)
                                PossiblePlacesToMove[i, j] = true;
                    }

                for (int i = 0; i < 3; i++)
                    for (int j = 0; j < 3; j++)
                    {
                        if (PossiblePlacesToMove[i, j] == false) { PossiblePlacesToMove[i, j] = true; }
                        else if (PossiblePlacesToMove[i, j] == true) { PossiblePlacesToMove[i, j] = false; }
                        if (Math.Abs(i - row) > 1 || Math.Abs(j - col) > 1)
                            PossiblePlacesToMove[i, j] = false;
                        if (White[row, col] == true && White[i, j] == true)
                            PossiblePlacesToMove[i, j] = false;
                        if (Black[row, col] == true && Black[i, j] == true)
                            PossiblePlacesToMove[i, j] = false;
                        PossiblePlacesToMove[row, col] = false;
                    }
            }
        }

        public void MakePossibleMoveImage(int row, int col)
        {
            //Clears any existing possible places to move and turns all green into white
            SetPossiblePlacesToMoveToFalse();
            ClearPlacesToMove();
            //Finds all the possible place to move for the given piece, and turns those squares green
            SetPossiblePlacesToMove(row, col);

            int Width = TicTacToeBoard.Width;
            int Length = TicTacToeBoard.Height;
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    if (PossiblePlacesToMove[i, j] == true)
                        for (int k = i * Length / 3 + 1; k < i * Length / 3 + 78; k++)
                            for (int l = j * Width / 3 + 1; l < j * Width / 3 + 78; l++)
                                if (ImageArray[k, l] == Color.FromArgb(255, 255, 255, 255))
                                    ImageArray[k, l] = Color.PaleGreen;

            SetBitmapFromArray();
            TicTacToeBoard.Image = bitmapImage;
        }

        public void SetBitmapFromArray()
        {
            //gets the bitmap image from the array
            for (int y = 0; y < 240; y++)
            {
                for (int x = 0; x < 240; x++)
                {
                    bitmapImage.SetPixel(y, x, ImageArray[y, x]);
                }
            }
        }

        public void UpdatePieceCountTextBoxes()
        {
            //shows the textbox of the amount of chess pieces left whenever the turn switches
            if (WhiteTurn == true)
            {
                txtPawn.Text = ChessPieceCount[0].ToString();
                txtRook.Text = ChessPieceCount[1].ToString();
                txtKnight.Text = ChessPieceCount[2].ToString();
                txtBishop.Text = ChessPieceCount[3].ToString();
                txtQueen.Text = ChessPieceCount[4].ToString();
                txtKing.Text = ChessPieceCount[5].ToString();
            }
            else
            {
                txtPawn.Text = ChessPieceCount[6].ToString();
                txtRook.Text = ChessPieceCount[7].ToString();
                txtKnight.Text = ChessPieceCount[8].ToString();
                txtBishop.Text = ChessPieceCount[9].ToString();
                txtQueen.Text = ChessPieceCount[10].ToString();
                txtKing.Text = ChessPieceCount[11].ToString();
            }
        }

        public void LowerChessPieceCount()
        {
            //everytime a chess piece is added, it reduces one from its corresponding piece count
            if (chessPieceToAdd == "White Pawn")
                ChessPieceCount[0]--;
            else if (chessPieceToAdd == "White Rook")
                ChessPieceCount[1]--;
            else if (chessPieceToAdd == "White Knight")
                ChessPieceCount[2]--;
            else if (chessPieceToAdd == "White Bishop")
                ChessPieceCount[3]--;
            else if (chessPieceToAdd == "White Queen")
                ChessPieceCount[4]--;
            else if (chessPieceToAdd == "White King")
                ChessPieceCount[5]--;
            else if (chessPieceToAdd == "Black Pawn")
                ChessPieceCount[6]--;
            else if (chessPieceToAdd == "Black Rook")
                ChessPieceCount[7]--;
            else if (chessPieceToAdd == "Black Knight")
                ChessPieceCount[8]--;
            else if (chessPieceToAdd == "Black Bishop")
                ChessPieceCount[9]--;
            else if (chessPieceToAdd == "Black Queen")
                ChessPieceCount[10]--;
            else if (chessPieceToAdd == "Black King")
                ChessPieceCount[11]--;
        }

        public void AddChessPiece(int yLocation, int xLocation, string ChessPiece)
        {
            //Takes file of the image of the chess piece
            Image ChessPieceImage = Image.FromFile(@ChessPiece + ".bmp");
            Bitmap chessPiecebitmap = new Bitmap(ChessPieceImage, 60, 60);

            //saves the pixels into an array
            for (int row = 0; row < 60; row++)
                for (int col = 0; col < 60; col++)
                {
                    ChessPieceArray[row, col] = chessPiecebitmap.GetPixel(row, col);
                }

            //replaces the corresponding location in the whole image with the image array of the chess piece
            int i = 0;
            for (int row = yLocation; row < yLocation + 60; row++)
            {
                int j = 0;
                for (int col = xLocation; col < xLocation + 60; col++)
                {
                    ImageArray[row, col] = ChessPieceArray[i, j];
                    j++;
                }
                i++;
            }
        }

        public void GameOverMessage(string strWhoWins)
        {
            //when game is over turns all green squares white and shows message box of whether they want to play again 
            ClearPlacesToMove();
            DialogResult dialogResult = MessageBox.Show(strWhoWins + "\r\n Would you like to start a new game?", "Good Game!", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                //a new game starts
                StartNewGame();
            }
            else
                //actions towards the board is disabled
                TicTacToeBoard.Enabled = false;
        }

        public bool IsWhitePieceChecked(int xWhiteKing, int yWhiteKing)
        {
            //checks if a white piece is under attack by checking all the black pieces
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    if (Black[i, j] == true)
                    {
                        ChessPiecesForChecking(i, j);
                        if (PossiblePlacesToMove[xWhiteKing, yWhiteKing] == true)
                            return true;
                    }
            return false;
        }

        public bool IsBlackPieceChecked(int yBlackKing, int xBlackKing)
        {
            //checks if a black piece is under attack by checking all the white pieces
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    if (White[i, j] == true)
                    {
                        ChessPiecesForChecking(i, j);
                        if (PossiblePlacesToMove[yBlackKing, xBlackKing] == true)
                            return true;
                    }
            return false;
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            int mouseX = e.X;
            int mouseY = e.Y;

            int WidthSquare = TicTacToeBoard.Width / 3;
            int LengthSquare = TicTacToeBoard.Height / 3;

            //When it's white's turn
            if (WhiteTurn == true)
            {
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {   
                        //Checks what the row and column of the array the mouseclick corresponds to
                        if ((mouseX <= (i + 1) * LengthSquare) && (mouseX >= i * LengthSquare) && (mouseY < (j + 1) * WidthSquare) && (mouseY >= j * WidthSquare))
                        {   
                            //Moves the piece from it's original spot to the new spot
                            if (PossiblePlacesToMove[i, j] == true)
                            {
                                SetPossiblePlacesToMoveToFalse();
                                string temporary = chessPieceLocation[i, j];
                                chessPieceLocation[i, j] = chessPieceLocation[yPieceToMove, xPieceToMove];
                                chessPieceLocation[yPieceToMove, xPieceToMove] = "";

                                White[i, j] = true;
                                bool tempBool = Black[i, j];
                                Black[i, j] = false;
                                White[yPieceToMove, xPieceToMove] = false;

                                //When the king is checked after the move, a warning message box appears and the move is undone
                                for (int m = 0; m < 3; m++)
                                    for (int n = 0; n < 3; n++)
                                        if (chessPieceLocation[m, n] == "King" && White[m, n] == true)
                                        {
                                            if (IsWhitePieceChecked(m, n) == true)
                                            {
                                                MessageBox.Show("Your king would be in check, please try another move!");
                                                chessPieceLocation[yPieceToMove, xPieceToMove] = chessPieceLocation[i, j];
                                                chessPieceLocation[i, j] = temporary;
                                                SetPossiblePlacesToMoveToFalse();
                                                ClearPlacesToMove();
                                                White[i, j] = false;
                                                Black[i, j] = tempBool;
                                                White[yPieceToMove, xPieceToMove] = true;
                                                return;
                                            }
                                        }

                                //when the opponent still has a three in a row, a warning message box appears and the move is undone
                                if (ThreeIsThere == true)
                                {
                                    int iXThreeColumn = 0, iXThreeRow = 0, x = 0;
                                    int iXForwardSlash = 0, iXBackwardSlash = 0;

                                    for (int m = 0; m < 3; m++)
                                    {
                                        for (int n = 0; n < 3; n++)
                                        {
                                            if (Black[m, n] == true)
                                                iXThreeColumn++;
                                            if (Black[n, m] == true)
                                                iXThreeRow++;
                                        }

                                        if (iXThreeColumn == 3 || iXThreeRow == 3)
                                        {
                                            MessageBox.Show("Your opponent has a three in a row! Please try another move!");
                                            chessPieceLocation[yPieceToMove, xPieceToMove] = chessPieceLocation[i, j];
                                            chessPieceLocation[i, j] = temporary;
                                            SetPossiblePlacesToMoveToFalse();
                                            ClearPlacesToMove();
                                            White[i, j] = false;
                                            Black[i, j] = tempBool;
                                            White[yPieceToMove, xPieceToMove] = true;
                                            return;
                                        }
                                        iXThreeColumn = 0;
                                        iXThreeRow = 0;
                                    }

                                    for (int m = 0; m < 3; m++)
                                    {
                                        if (Black[m, x] == true)
                                            iXForwardSlash++;
                                        if (Black[m, 2 - x] == true)
                                            iXBackwardSlash++;
                                        x++;
                                    }

                                    if (iXForwardSlash == 3 || iXBackwardSlash == 3)
                                    {
                                        MessageBox.Show("Your opponent has a three in a row! Please try another move!");
                                        chessPieceLocation[yPieceToMove, xPieceToMove] = chessPieceLocation[i, j];
                                        chessPieceLocation[i, j] = temporary;
                                        SetPossiblePlacesToMoveToFalse();
                                        ClearPlacesToMove();
                                        White[i, j] = false;
                                        Black[i, j] = tempBool;
                                        White[yPieceToMove, xPieceToMove] = true;
                                        return;
                                    }
                                }
                                ThreeIsThere = false;
                                KingUnderCheck = false;

                                int yOriginal = yPieceToMove * LengthSquare + 1;
                                for (int m = i * LengthSquare + 1; m < i * LengthSquare + 78; m++)
                                {
                                    int xOriginal = xPieceToMove * WidthSquare + 1;
                                    for (int l = j * WidthSquare + 1; l < j * WidthSquare + 78; l++)
                                    {
                                        ImageArray[m, l] = ImageArray[yOriginal, xOriginal];
                                        xOriginal++;
                                    }
                                    yOriginal++;
                                }

                                for (yOriginal = yPieceToMove * LengthSquare + 1; yOriginal < yPieceToMove * LengthSquare + 78; yOriginal++)
                                    for (int xOriginal = xPieceToMove * WidthSquare + 1; xOriginal < xPieceToMove * WidthSquare + 78; xOriginal++)
                                        ImageArray[yOriginal, xOriginal] = Color.FromArgb(255, 255, 255, 255);

                                //when the white pawn reaches the top row, it turns into another piece
                                if (j == 0 && chessPieceLocation[i, j] == "Pawn")
                                {
                                    Form2 f2 = new Form2();
                                    f2.ShowDialog();
                                    string ReplacingChessPiece = f2.ChosenChessPiece;
                                    AddChessPiece(i * LengthSquare + 10, j * WidthSquare + 10, "White " + ReplacingChessPiece);
                                    chessPieceLocation[i, j] = ReplacingChessPiece;
                                }

                                WhiteTurn = false;
                                if (tempBool == true)
                                    BlackCount--;
                                break;
                            }
                            //if a white piece is clicked, all possible moves are identified
                            else if (White[i, j] == true)
                            {
                                MakePossibleMoveImage(i, j);
                                chessPieceToAdd = "";
                                xPieceToMove = j;
                                yPieceToMove = i;
                                return;
                            }
                            //if an "add chess piece" button has been clicked and there is nothing on that square, the chess piece is added
                            else if (chessPieceToAdd != "" && chessPieceLocation[i, j] == "")
                            {
                                int yPosition = i * LengthSquare + 10, xPosition = j * WidthSquare + 10;
                                if (chessPieceToAdd == "White Pawn" && xPosition < 160)
                                    return;

                                if (chessPieceToAdd == "White King" && IsWhitePieceChecked(i, j) == true)
                                    return;

                                AddChessPiece(yPosition, xPosition, chessPieceToAdd);
                                chessPieceLocation[i, j] = chessPieceToAdd.Remove(0, 6);
                                WhiteCount++;
                                WhiteTurn = false;
                                White[i, j] = true;
                                break;
                            }
                            //when none of above conditions apply, nothing happens
                            else
                                return;
                        }
                    }
                    if (WhiteTurn == false)
                        break;
                }
                SetBitmapFromArray();
                TicTacToeBoard.Image = bitmapImage;

                //Checks if the black king is under checkmate, if it is, white wins
                for (int i = 0; i < 3; i++)
                    for (int j = 0; j < 3; j++)
                        if (chessPieceLocation[i, j] == "King" && Black[i, j] == true)
                        {
                            if (IsBlackPieceChecked(i, j) == true)
                                KingUnderCheck = true;

                            SetPossiblePlacesToMoveToFalse();
                            bool test = true;
                            for (int m = i - 1; m < i + 2; m++)
                                for (int l = j - 1; l < j + 2; l++)
                                    if (m > -1 && l > -1 && m < 3 && l < 3 && IsBlackPieceChecked(m, l) == false && Black[m,l] == false)
                                        test = false;

                            if (test == true)
                            {
                                for (int x = 0; x < 3; x++)
                                    for (int y = 0; y < 3; y++)
                                        if (White[x, y] == true)
                                        {
                                            SetPossiblePlacesToMoveToFalse();
                                            SetPossiblePlacesToMove(x, y);
                                            if (PossiblePlacesToMove[i, j] == true)
                                            {
                                                SetPossiblePlacesToMoveToFalse();
                                                if (IsWhitePieceChecked(x, y) == false)
                                                {
                                                    string strWhoWins = "Congratulations! White Wins!";
                                                    GameOverMessage(strWhoWins);
                                                    return;
                                                }
                                            }
                                            SetPossiblePlacesToMoveToFalse();
                                        }
                            }
                        }
            }
            //black's turn
            else
            {
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        //Checks what the row and column of the array the mouseclick corresponds to
                        if ((mouseX <= (i + 1) * LengthSquare) && (mouseX >= i * LengthSquare) && (mouseY <= (j + 1) * WidthSquare) && (mouseY >= j * WidthSquare))
                        {
                            //Moves the piece from it's original spot to the new spot
                            if (PossiblePlacesToMove[i, j] == true)
                            {
                                SetPossiblePlacesToMoveToFalse();
                                string temporary = chessPieceLocation[i, j];
                                chessPieceLocation[i, j] = chessPieceLocation[yPieceToMove, xPieceToMove];
                                chessPieceLocation[yPieceToMove, xPieceToMove] = "";

                                Black[i, j] = true;
                                bool tempBool = White[i, j];
                                White[i, j] = false;
                                Black[yPieceToMove, xPieceToMove] = false;

                                //When the king is checked after the move, a warning message box appears and the move is undone
                                for (int m = 0; m < 3; m++)
                                    for (int n = 0; n < 3; n++)
                                        if (chessPieceLocation[m, n] == "King" && Black[m, n] == true)
                                        {
                                            if (IsBlackPieceChecked(m, n) == true)
                                            {
                                                MessageBox.Show("Your king would be in check, please try another move!");
                                                chessPieceLocation[yPieceToMove, xPieceToMove] = chessPieceLocation[i, j];
                                                chessPieceLocation[i, j] = temporary;
                                                SetPossiblePlacesToMoveToFalse();
                                                ClearPlacesToMove();
                                                Black[i, j] = false;
                                                White[i, j] = tempBool;
                                                Black[yPieceToMove, xPieceToMove] = true;
                                                return;
                                            }
                                        }

                                //when the opponent has three in a row, one of the three must be taken
                                if (ThreeIsThere == true)
                                {
                                    int iOThreeColumn = 0, iOThreeRow = 0, x = 0;
                                    int iOForwardSlash = 0, iOBackwardSlash = 0;

                                    for (int m = 0; m < 3; m++)
                                    {
                                        for (int n = 0; n < 3; n++)
                                        {
                                            if (White[m, n] == true)
                                                iOThreeColumn++;
                                            if (White[n, m] == true)
                                                iOThreeRow++;
                                        }

                                        if (iOThreeColumn == 3 || iOThreeRow == 3)
                                        {
                                            MessageBox.Show("Your opponent has a three in a row! Please try another move!");
                                            chessPieceLocation[yPieceToMove, xPieceToMove] = chessPieceLocation[i, j];
                                            chessPieceLocation[i, j] = temporary;
                                            SetPossiblePlacesToMoveToFalse();
                                            ClearPlacesToMove();
                                            Black[i, j] = false;
                                            White[i, j] = tempBool;
                                            Black[yPieceToMove, xPieceToMove] = true;
                                            return;
                                        }
                                        iOThreeColumn = 0;
                                        iOThreeRow = 0;
                                    }

                                    for (int m = 0; m < 3; m++)
                                    {
                                        if (White[m, x] == true)
                                            iOForwardSlash++;
                                        if (White[m, 2 - x] == true)
                                            iOBackwardSlash++;
                                        x++;
                                    }

                                    if (iOForwardSlash == 3 || iOBackwardSlash == 3)
                                    {
                                        MessageBox.Show("Your opponent has a three in a row! Please try another move!");
                                        chessPieceLocation[yPieceToMove, xPieceToMove] = chessPieceLocation[i, j];
                                        chessPieceLocation[i, j] = temporary;
                                        SetPossiblePlacesToMoveToFalse();
                                        ClearPlacesToMove();
                                        Black[i, j] = false;
                                        White[i, j] = tempBool;
                                        Black[yPieceToMove, xPieceToMove] = true;
                                        return;
                                    }
                                }
                                ThreeIsThere = false;
                                KingUnderCheck = false;

                                int yOriginal = yPieceToMove * LengthSquare + 1;
                                for (int m = i * LengthSquare + 1; m < i * LengthSquare + 78; m++)
                                {
                                    int xOriginal = xPieceToMove * WidthSquare + 1;
                                    for (int l = j * WidthSquare + 1; l < j * WidthSquare + 78; l++)
                                    {
                                        ImageArray[m, l] = ImageArray[yOriginal, xOriginal];
                                        xOriginal++;
                                    }
                                    yOriginal++;
                                }

                                for (yOriginal = yPieceToMove * LengthSquare + 1; yOriginal < yPieceToMove * LengthSquare + 78; yOriginal++)
                                    for (int xOriginal = xPieceToMove * WidthSquare + 1; xOriginal < xPieceToMove * WidthSquare + 78; xOriginal++)
                                        ImageArray[yOriginal, xOriginal] = Color.FromArgb(255, 255, 255, 255);

                                //when the black pawn reaches the top row, it turns into another piece
                                if (j == 2 && chessPieceLocation[i, j] == "Pawn")
                                {
                                    Form2 f2 = new Form2();
                                    f2.ShowDialog();
                                    string ReplacingChessPiece = f2.ChosenChessPiece;
                                    AddChessPiece(i * LengthSquare + 10, j * WidthSquare + 10, "Black " + ReplacingChessPiece);
                                    chessPieceLocation[i, j] = ReplacingChessPiece;
                                }

                                WhiteTurn = true;
                                if (tempBool == true)
                                    WhiteCount--;
                                break;
                            }
                            //if a black piece is clicked, finds the possible places to move
                            else if (Black[i, j] == true)
                            {
                                MakePossibleMoveImage(i, j);
                                chessPieceToAdd = "";
                                xPieceToMove = j;
                                yPieceToMove = i;
                                return;
                            }
                            //if an add piece button was clicked, and then an empty square, the piece is added to the square
                            else if (chessPieceToAdd != "" && chessPieceLocation[i, j] == "")
                            {
                                int yPosition = i * LengthSquare + 10, xPosition = j * WidthSquare + 10;
                                if (chessPieceToAdd == "Black Pawn" && xPosition > 80)
                                    return;
                                if (chessPieceToAdd == "Black King" && IsBlackPieceChecked(i, j) == true)
                                    return;
                                AddChessPiece(yPosition, xPosition, chessPieceToAdd);
                                chessPieceLocation[i, j] = chessPieceToAdd.Remove(0, 6);
                                BlackCount++;
                                WhiteTurn = true;
                                Black[i, j] = true;
                                break;
                            }
                            else
                                return;
                        }
                    }
                    if (WhiteTurn == true)
                        break;
                }
                SetBitmapFromArray();
                TicTacToeBoard.Image = bitmapImage;

                //checks if the white king is under checkmate, if it is, black wins
                for (int i = 0; i < 3; i++)
                    for (int j = 0; j < 3; j++)
                        if (chessPieceLocation[i, j] == "King" && White[i, j] == true)
                        {
                            if (IsWhitePieceChecked(i, j) == true)
                                KingUnderCheck = true;

                            SetPossiblePlacesToMoveToFalse();
                            bool test = true;
                            for (int m = i - 1; m < i + 2; m++)
                                for (int l = j - 1; l < j + 2; l++)
                                    if (m > -1 && l > -1 && m < 3 && l < 3 && IsWhitePieceChecked(m, l) == false && White[m,l] == false)
                                        test = false;

                            if (test == true)
                                for (int x = 0; x < 3; x++)
                                    for (int y = 0; y < 3; y++)
                                        if (Black[x, y] == true)
                                        {
                                            SetPossiblePlacesToMoveToFalse();
                                            SetPossiblePlacesToMove(x, y);
                                            if (PossiblePlacesToMove[i, j] == true)
                                            {
                                                SetPossiblePlacesToMoveToFalse();
                                                if (IsBlackPieceChecked(x, y) == false)
                                                {
                                                    string strWhoWins = "Congratulations! Black Wins!";
                                                    GameOverMessage(strWhoWins);
                                                    return;
                                                }
                                            }
                                            SetPossiblePlacesToMoveToFalse();
                                        }
                        }
            }

            //a label shows whose turn it is
            if (WhiteTurn == true)
                lblTurn.Text = "White's Turn";
            else
                lblTurn.Text = "Black's Turn";

            int WhiteThreeColumn = 0, WhiteThreeRow = 0, BlackThreeColumn = 0, BlackThreeRow = 0;
            int WhiteForwardSlash = 0, WhiteBackwardSlash = 0, BlackForwardSlash = 0, BlackBackwardSlash = 0;
            int[] xValues = new int[3];
            int[] yValues = new int[3];

            //identifies three sequences in rows or columns
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (White[i, j] == true)
                        WhiteThreeColumn++;
                    if (White[j, i] == true)
                        WhiteThreeRow++;
                    if (Black[i, j] == true)
                        BlackThreeColumn++;
                    if (Black[j, i] == true)
                        BlackThreeRow++;

                    if (WhiteThreeColumn == 3)
                    {
                        for (int a = 0; a < 3; a++)
                        {
                            xValues[a] = i;
                            yValues[a] = j - a;
                        }
                        ThreeIsThere = true;
                    }
                    else if (BlackThreeColumn == 3)
                    {
                        for (int a = 0; a < 3; a++)
                        {
                            xValues[a] = i;
                            yValues[a] = j - a;
                        }
                        ThreeIsThere = true;
                    }

                    if (WhiteThreeRow == 3)
                    {
                        for (int a = 0; a < 3; a++)
                        {
                            xValues[a] = j - a;
                            yValues[a] = i;
                        }
                        ThreeIsThere = true;
                    }
                    else if (BlackThreeRow == 3)
                    {
                        for (int a = 0; a < 3; a++)
                        {
                            xValues[a] = j - a;
                            yValues[a] = i;
                        }
                        ThreeIsThere = true;
                    }
                }
                WhiteThreeColumn = 0;
                WhiteThreeRow = 0;
                BlackThreeColumn = 0;
                BlackThreeRow = 0;
            }

            //identifies a sequence of three in a diagonal
            int k = 0;
            for (int i = 0; i < 3; i++)
            {
                if (White[i, k] == true)
                    WhiteForwardSlash++;
                if (White[i, 2 - k] == true)
                    WhiteBackwardSlash++;
                if (Black[i, k] == true)
                    BlackForwardSlash++;
                if (Black[i, 2 - k] == true)
                    BlackBackwardSlash++;

                if (WhiteForwardSlash == 3)
                {
                    for (int a = 0; a < 3; a++)
                    {
                        xValues[a] = a;
                        yValues[a] = a;
                    }
                    ThreeIsThere = true;
                }
                else if (BlackForwardSlash == 3)
                {
                    for (int a = 0; a < 3; a++)
                    {
                        xValues[a] = a;
                        yValues[a] = a;
                    }
                    ThreeIsThere = true;
                }

                if (WhiteBackwardSlash == 3)
                {
                    for (int a = 0; a < 3; a++)
                    {
                        xValues[a] = 2 - a;
                        yValues[a] = a;
                    }
                    ThreeIsThere = true;
                }
                else if (BlackBackwardSlash == 3)
                {
                    for (int a = 0; a < 3; a++)
                    {
                        xValues[a] = 2 - a;
                        yValues[a] = a;
                    }
                    ThreeIsThere = true;
                }
                k++;
            }

            //if all three in the row can't be taken by the next turn, that player wins
            int HowManySafe = 0;
            if (ThreeIsThere == true)
                for (int i = 0; i < 3; i++)
                    if (WhiteTurn == true && IsBlackPieceChecked(xValues[i],yValues[i]) == false)
                    {
                        HowManySafe++;
                        if (HowManySafe == 3)
                        {
                            string strWhoWins = "Congratulations! Black Wins!";
                            GameOverMessage(strWhoWins);
                            return;
                        }
                    }
                    else if (WhiteTurn == false && IsWhitePieceChecked(xValues[i], yValues[i]) == false)
                    {
                        HowManySafe++;
                        if (HowManySafe == 3)
                        {
                            string strWhoWins = "Congratulations! White Wins!";
                            GameOverMessage(strWhoWins);
                            return;
                        }
                    }

            LowerChessPieceCount();
            UpdatePieceCountTextBoxes();
            chessPieceToAdd = "";
            SetPossiblePlacesToMoveToFalse();
            ClearPlacesToMove();
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {

        }

        public void FindAvailableSpaces(string WhitePiece, string BlackPiece)
        {
            if (TicTacToeBoard.Enabled == true)
            {
                //if the king is under check, or the opponent has three in row, or you have four pieces on board
                //you can not add a piece
                if (KingUnderCheck == true)
                {
                    MessageBox.Show("Your king is under check! You can not add any pieces.");
                    return;
                }

                if(ThreeIsThere == true)
                {
                    MessageBox.Show("The opponent has a three in a row! You can not add any pieces.");
                    return;
                }

                if ((WhiteTurn == true && WhiteCount == 4) || (WhiteTurn == false && BlackCount == 4))
                {
                    MessageBox.Show("Sorry, you can only have 4 pieces on the board at once!");
                    return;
                }

                //clears all places to move
                SetPossiblePlacesToMoveToFalse();
                ClearPlacesToMove();

                //identifies whether to add a white piece or black piece
                if (WhiteTurn == true)
                    chessPieceToAdd = WhitePiece;
                else
                    chessPieceToAdd = BlackPiece;

                //finds all the available spaces of adding the piece and makes the square yellow-brown
                int Width = TicTacToeBoard.Width;
                int Length = TicTacToeBoard.Height;
                Graphics g = Graphics.FromImage(TicTacToeBoard.Image);
                SolidBrush yellowBrush = new SolidBrush(Color.Linen);

                //white pawns can only be added to the bottom row
                //black pawns can only be added to the top row
                //kings can not be added where they will be checked
                for (int i = 0; i < 3; i++)
                    for (int j = 0; j < 3; j++)
                    {
                        if (chessPieceToAdd == "White Pawn")
                        {
                            if (chessPieceLocation[i, 2] == "")
                                g.FillRectangle(yellowBrush, i * Length / 3 + 1, 2 * Width / 3 + 1, 77, 77);
                        }
                        else if (chessPieceToAdd == "Black Pawn")
                        {
                            if (chessPieceLocation[i, 0] == "")
                                g.FillRectangle(yellowBrush, i * Length / 3 + 1, 1, 77, 77);
                        }
                        else if (chessPieceToAdd == "White King")
                        {
                            if (chessPieceLocation[i, j] == "" && IsWhitePieceChecked(i, j) == false)
                                g.FillRectangle(yellowBrush, i * Length / 3 + 1, j * Width / 3 + 1, 77, 77);
                            SetPossiblePlacesToMoveToFalse();
                        }
                        else if (chessPieceToAdd == "Black King")
                        {
                            if (chessPieceLocation[i, j] == "" && IsBlackPieceChecked(i, j) == false)
                                g.FillRectangle(yellowBrush, i * Length / 3 + 1, j * Width / 3 + 1, 77, 77);
                            SetPossiblePlacesToMoveToFalse();
                        }
                        else
                        {
                            if (chessPieceLocation[i, j] == "")
                                g.FillRectangle(yellowBrush, i * Length / 3 + 1, j * Width / 3 + 1, 77, 77);
                        }
                    }
            }
        }

        //when the number of that piece left reaches zero
        public void NoMorePieceMessageBox()
        {
            MessageBox.Show("Sorry, you do not have that piece anymore to place on the board.");
        }

        //prepares to add a pawn to the board
        private void btnPawn_Click(object sender, EventArgs e)
        {
            if (WhiteTurn == true && ChessPieceCount[0] == 0)
                NoMorePieceMessageBox();
            else if (WhiteTurn == false && ChessPieceCount[6] == 0)
                NoMorePieceMessageBox();
            else
                FindAvailableSpaces("White Pawn", "Black Pawn");
        }

        //prepares to add a rook to the board
        private void btnRook_Click(object sender, EventArgs e)
        {
            if (WhiteTurn == true && ChessPieceCount[1] == 0)
                NoMorePieceMessageBox();
            else if (WhiteTurn == false && ChessPieceCount[7] == 0)
                NoMorePieceMessageBox();
            else
                FindAvailableSpaces("White Rook", "Black Rook");
        }

        //prepares to add a knight to the board
        private void btnKnight_Click(object sender, EventArgs e)
        {
            if (WhiteTurn == true && ChessPieceCount[2] == 0)
                NoMorePieceMessageBox();
            else if (WhiteTurn == false && ChessPieceCount[8] == 0)
                NoMorePieceMessageBox();
            else
                FindAvailableSpaces("White Knight", "Black Knight");
        }

        //prepares to add a bishop to the board
        private void btnBishop_Click(object sender, EventArgs e)
        {
            if (WhiteTurn == true && ChessPieceCount[3] == 0)
                NoMorePieceMessageBox();
            else if (WhiteTurn == false && ChessPieceCount[9] == 0)
                NoMorePieceMessageBox();
            else
                FindAvailableSpaces("White Bishop", "Black Bishop");
        }

        //prepares to add a queen to the board
        private void btnQueen_Click(object sender, EventArgs e)
        {
            if (WhiteTurn == true && ChessPieceCount[4] == 0)
                NoMorePieceMessageBox();
            else if (WhiteTurn == false && ChessPieceCount[10] == 0)
                NoMorePieceMessageBox();
            else
                FindAvailableSpaces("White Queen", "Black Queen");
        }

        //prepares to add a king to the board
        private void btnKing_Click(object sender, EventArgs e)
        {
            if (WhiteTurn == true && ChessPieceCount[5] == 0)
                NoMorePieceMessageBox();
            else if (WhiteTurn == false && ChessPieceCount[11] == 0)
                NoMorePieceMessageBox();
            else
                FindAvailableSpaces("White King", "Black King");
        }

        //whitens the board and cancels all potential actions
        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (TicTacToeBoard.Image != null)
            {
                ClearPlacesToMove();
                SetPossiblePlacesToMoveToFalse();
                chessPieceToAdd = "";
            }
        }
    }
}
