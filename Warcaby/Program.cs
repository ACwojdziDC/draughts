using System;
using System.Collections.Generic;

namespace Warcaby
{

    /*
    class Node
    {
        Node next;
        public Node() { }
    }
    class SinglyLinkedList
    {
        Node root;

        metody na liscie dodawanie, usuwanie, edycja itd
    }
    */

    /* 
There is a class Board which represents square board for Polish draughts.
There is a parameter n in the constructor describing the length of the side of the square, size should be an integer between 10 and 20 provided from user input.
There is a 2D array Pawn[,] Fields which represents fields on a board. Each field can be null (empty) or a Pawn instance.
Pawns are created and placed only at every second field when the board is initialized. Their number is determined by board size as a 2 * n.
There is ToString() method that overrides built-in method. This method should mark rows as a numbers and columns as a letters.
There is RemovePawn() method that removes pawn with given position from.
There is MovePawn() method that moves pawn with given position from one field to another.
    */
    
    /*
    public class Pawn
    {

        private bool isWhite;
        public bool IsWhite
        {
            get { return isWhite; }
            set { isWhite = value; }
        }

        private bool isCrowned;
        public bool IsCrowned
        {
            get { return isCrowned; }
            set { isCrowned = value; }
        }

        Tuple<int, int> coordinates;

        public Pawn(bool _isWhite, int x, int y) 
        {
            coordinates = Tuple.Create(x, y);
            isWhite = _isWhite;
        }

        public bool ValidateMove()
        {
            // check if multiple jumps are available
            return true;
        }
    }


    public class Board
    {
        public Pawn[,] board;
        private int size;
        public Board ()
        {
            Console.WriteLine("Please input the size of the board (10-20): ");
            size = Convert.ToInt32(Console.ReadLine());
            board = new Pawn[size, size];
        }

        public void InitBoard()
        {
            for (int i = 0; i < size; i++)
            {   
                for (int j = 0; j < size; j++)
                {
                    if (i == 0 || i == 1 || i == size - 2 || i == size - 1)
                    {
                        //sprawdzanie czy wiersz (czyli rzad planszy) jest parzysty
                        if (i % 2 == 0) 
                        {
                            if (j % 2 == 0)
                            {
                                if (i > size / 2)
                                    board[i, j] = new Pawn(true, i, j);
                                else
                                    board[i, j] = new Pawn(false, i, j);
                            }
                            else
                                board[i, j] = null;
                        }
                        else
                        {
                            //sprawdzanie czy jest nieparzysty
                            if (j % 2 == 1)
                            {
                                if (i > size / 2)
                                    board[i, j] = new Pawn(true, i, j);
                                else
                                    board[i, j] = new Pawn(false, i, j);
                            }
                            else
                                board[i, j] = null;
                        }
                            
                    }
                }
            }
        }

        public override string ToString()
        {
            string b = "";
            for(int i = 0; i < size; i++)
            {
                for(int j = 0; j < size; j++)
                {
                    if (board[i, j] == null)
                        //Console.Write("O ");
                        b += "O ";
                    else
                    {
                        string pionek = board[i, j].IsWhite ? "W " : "B ";
                        //Console.Write(pionek + " ");
                        b += pionek;
                    }
                }
                b += "\n";
            }

            return b;
        }

        public void RemovePawn()
        {

        }

        public void MovePawn()
        {

        }

    }


    class Game
    {
        /*
There is a class Game which contains all game logic and actions.
There is a method Start() that starts whole game between players.
There is a method Round() that determines one round actions i.e. check who plays next or is there a winner yet.
There is a method that checks if starting position from user input is a valid pawn and if ending position is within board boundaries. If so, it calls TryToMakeMove() on pawn instance.
There is a method CheckForWinner() that checks after each round is there an a winner.
Method CheckForWinner() checks also for draws.
        */

/*
    public void Start()
    {
        
    }

    public void Round()
    {

    }

    public void TryToMakeMove()
    {

    }

    public void CheckForWinner()
    {

    }

    
}
*/

class Program
{
    static void Main(string[] args)
    {
        Game game = new Game();
        game.Start();
        //Board b = new Board();
        //b.InitBoard();
        //Console.Write(b.ToString());
        //Pawn[,] boardArrey = b.board;
        //AskForMove move = new AskForMove();
        
        //(int?, int?) inputCoordinates = move.AskUserForCoordinates();
        //Console.Out.WriteLine("The user entered the following coordinates: {0}", inputCoordinates);
        
        //move.MakeMove(boardArrey, 10);
    }
}

}
