using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warcaby
{
    public class Board
    {
        private Pawn[,] _board;
        private int size;
        public Pawn[,] board
        {
            get { return _board; }
        }
        public int Size
        {
            get { return size; }
        }
        public Board()
        {
            Console.WriteLine("Please input the size of the board (10-20): ");
            size = Convert.ToInt32(Console.ReadLine());
            _board = new Pawn[size, size];
            InitBoard();
            Console.WriteLine(this.ToString());
        }

        public void InitBoard()
        {
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    List<int> allowedRows = new List<int>() { 0, 1, 2, 3, size - 1, size - 2, size - 3, size - 4 };
                    if (allowedRows.Contains(i))
                    {
                        /* sprawdzanie czy wiersz (czyli rzad planszy) jest parzysty*/
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
                            /* sprawdzanie czy jest nieparzysty*/
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
            int n = ((size + 1) * 6 - 2);
            char horizontalLine = '-';

            for (int k = 0; k <= n; k++)
            {
                Console.Write(horizontalLine);
            }
            Console.WriteLine("-");

            Console.Write("|    ");
            for (int j = 0; j <= (size - 1); j++)
            {
                Console.Write("|  " + (char)(j + 65) + "  ");
            }
            Console.WriteLine("|");

            for (int k = 0; k <= n; k++)
            {
                Console.Write(horizontalLine);
            }
            Console.WriteLine("-");

            for (int i = 0; i <= (size - 1); i++)
            {
                for (int l = 0; l <= 2; l++)
                {
                    if (l % 2 == 1)
                    {
                        //row number
                        if (i <= 8)
                        {
                            Console.Write("| " + (i + 1) + "  ");
                        }
                        else
                        {
                            Console.Write("| " + (i + 1) + " ");
                        }
                        //row content
                        for (int j = 0; j <= (size - 1); j++)
                        {
                            if (board[i, j] == null)
                            {
                                if ((i % 2 == 1 & j % 2 == 0) | (i % 2 == 0 & j % 2 == 1))
                                {
                                    Console.Write("|  -  ");
                                }
                                else
                                {
                                    Console.Write("|     ");
                                }

                            }
                            else
                            {
                                if (board[i, j].IsWhite == false)
                                {
                                    if (board[i, j].IsCrowned == false)
                                    {
                                        Console.Write("|  x  ");
                                    }
                                    else
                                    {
                                        Console.Write("| xxx ");
                                    }
                                }
                                else
                                {
                                    if (board[i, j].IsCrowned == false)
                                    {
                                        Console.Write("|");
                                        Console.ForegroundColor = ConsoleColor.White;
                                        Console.Write(" o o ");
                                        Console.ResetColor();
                                    }
                                    else
                                    {
                                        Console.Write("|");
                                        Console.ForegroundColor = ConsoleColor.White;
                                        Console.Write(" ooo ");
                                        Console.ResetColor();
                                    }
                                       
                                }

                            }

                        }
                        Console.WriteLine("|");
                    }
                    else
                    {
                        //row start                    
                        Console.Write("|    ");
                        //row content
                        for (int j = 0; j <= (size - 1); j++)
                        {
                            if (board[i, j] == null)
                            {
                                Console.Write("|     ");
                            }
                            else
                            {
                                if (board[i, j].IsWhite == false)
                                {
                                    Console.Write("| x x ");
                                }
                                else
                                {
                                    Console.Write("|");
                                    Console.ForegroundColor = ConsoleColor.White;
                                    Console.Write(" ooo ");
                                    Console.ResetColor();
                                }
                            }
                        }
                        Console.WriteLine("|");
                    }

                }
                // line dividing the rows
                for (int k = 0; k <= n; k++)
                {
                    Console.Write(horizontalLine);
                }
                Console.WriteLine('-');
            }
            return "-";
        }

        public void RemovePawn((int row, int col) pawnCoordinate)
        {
            board[pawnCoordinate.row, pawnCoordinate.col] = null;
        }

        public void MovePawn((int row, int col) pawnCoordinate, (int row, int col) nextCoordinate)
        {

            board[nextCoordinate.row, nextCoordinate.col] = board[pawnCoordinate.row, pawnCoordinate.col];
            board[nextCoordinate.row, nextCoordinate.col].coordinates = Tuple.Create(nextCoordinate.row, nextCoordinate.col);
            board[pawnCoordinate.row, pawnCoordinate.col] = null;
            if ((board[nextCoordinate.row, nextCoordinate.col].IsWhite == true & nextCoordinate.row == 0) | 
                    (board[nextCoordinate.row, nextCoordinate.col].IsWhite == false & nextCoordinate.row == (size-1)))
            {
                board[nextCoordinate.row, nextCoordinate.col].IsCrowned = true;
            }
            

        }
    }

}
