using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace Warcaby
{

    /*
There is a class Game which contains all game logic and actions.
There is a method Start() that starts whole game between players.
There is a method Round() that determines one round actions i.e. check who plays next or is there a winner yet.
There is a method that checks if starting position from user input is a valid pawn and if ending position is within board boundaries. If so, it calls TryToMakeMove() on pawn instance.
There is a method CheckForWinner() that checks after each round is there an a winner.
Method CheckForWinner() checks also for draws.
    */
    class Game
    {
        private Board board;
        private bool isFinished = false;
        private int player = 2;
        private Player curentPlayer;
        
        AskForMove move = new AskForMove();
        AI ai = new AI();
        Menu menu = new Menu();
        Player player1;
        Player player2;


        public void Start()
        {
            string gameMode = menu.GameMode;
            player1 = new Player("white", 1, gameMode.Contains("HUMAN") ? true : false);
            player2 = new Player("black", 2, gameMode.Contains("COMPUTER") ? false : true);
            curentPlayer = player2;
            
            board = new Board();
            while (!isFinished)
            {
                Round();
                //Console.Clear();
                Console.Out.WriteLine(board.ToString());
            }
        }

        public void Round()
        {
            curentPlayer = curentPlayer.Equals(player2) ? player1 : player2;
            player = player == 2 ? 1 : 2;
            TryToMakeMove();
            CheckForWinner();
            int sleepTime = !curentPlayer.Human ? 1000 : 0;
            Thread.Sleep(sleepTime);
            //Console.ReadKey();
        }

        public void TryToMakeMove()
        {
            (int row, int col)[] moveCoordinates = !curentPlayer.Human ? ai.aiMove(player, board) : move.MakeMove(board, player);


            //(int row, int col)[] moveCoordinates = move.MakeMove(board.board, board.Size, player);

            if (Math.Abs(moveCoordinates[0].row - moveCoordinates[1].row) == 2 && Math.Abs(moveCoordinates[0].col - moveCoordinates[1].col) == 2)
            {
                (int, int) pawnToCapture;
                if (board.board[moveCoordinates[0].row, moveCoordinates[0].col].IsWhite && (moveCoordinates[0].col - moveCoordinates[1].col == -2))
                {
                    pawnToCapture = (moveCoordinates[0].row - 1, moveCoordinates[0].col + 1);
                }
                else if (board.board[moveCoordinates[0].row, moveCoordinates[0].col].IsWhite && (moveCoordinates[0].col - moveCoordinates[1].col == 2))
                {
                    pawnToCapture = (moveCoordinates[0].row - 1, moveCoordinates[0].col - 1);
                }
                else if (!board.board[moveCoordinates[0].row, moveCoordinates[0].col].IsWhite && (moveCoordinates[0].col - moveCoordinates[1].col == -2))
                {
                    pawnToCapture = (moveCoordinates[0].row + 1, moveCoordinates[0].col + 1);
                }
                else
                {
                    pawnToCapture = (moveCoordinates[0].row + 1, moveCoordinates[0].col - 1);
                }
                board.RemovePawn(pawnToCapture);
            }
            board.MovePawn(moveCoordinates[0], moveCoordinates[1]);
            
        }

        public void MakeMoveOrCapture((int row, int col) pawnCoordinate, (int row, int col) nextPawnCoordinate,
            bool capture = false)
        {
            if (capture)
            {
                (int row, int col) pawnToCapture = (pawnCoordinate.row + 1, pawnCoordinate.col + 1);
                board.RemovePawn(pawnToCapture);
            }
           
            board.MovePawn(pawnCoordinate, nextPawnCoordinate);
        }

        public void CheckForWinner()
        {
            int W = 0, B = 0;

            for (int row = 0; row < board.Size; row++)
            {
                for (int col = 0; col < board.Size; col++)
                {
                    Pawn p = board.board[row, col];
                    if (board.board[row,col] != null)
                    {
                        if (board.board[row, col].IsWhite)
                            W++;
                        else
                            B++;
                    }
                }
            }

            ValidateResult(W, B);
        }

        public void ValidateResult(int white, int black)
        {
            if (white == 1 && black == 1)
            {
                printEndScreen(3);
                isFinished = true;
            }
            else if (white == 0)
            {
                printEndScreen(2);
                isFinished = true;
            }
            else if (black == 0)
            {
                printEndScreen(1);
                isFinished = true;
            }

            isFinished = false;
        }

        private void printEndScreen(int ending)
        {
            if (ending == 1)
            {
                Console.WriteLine("**************White has won**************");
            }
            else if (ending == 2)
            {
                Console.WriteLine("**************Black has won**************");
            }
            else
            {
                Console.WriteLine("************** Tied **************"); 
            }
        }
    }
}
