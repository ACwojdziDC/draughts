using System;
using System.Linq;

namespace Warcaby
{
    public class AskForMove
    {
        private readonly Messages _message = new Messages();
        
        
        public (int, int)[] MakeMove(Board board, int player = 1)
        {
            Pawn[,] fieldsArray = board.board;
            int boardSize = board.Size;
            (int row, int col) pawnToMove;
            (int, int) fieldToMoveTo;
            (int, int)[] availableMoves = new (int, int)[] {};
            (int, int)[] avilableCaptures = new (int, int)[] {};
            (int, int)[] result;
            string messageText;
            bool isValidChoice;
            bool isEmptyList = true;

            //Player selects pawn
            _message.WriteMessage("choosePawn");
            do
            {
                pawnToMove = AskUserForCoordinates(boardSize, player);
                isValidChoice = ValidPawn(fieldsArray, pawnToMove, player);
                messageText = "wrongPawn";
                if (isValidChoice)
                {
                    availableMoves = GetPosibleMoves(pawnToMove, player, board);
                    //if (pawnToMove.row > 1 && pawnToMove.row < boardSize - 2)
                    avilableCaptures = GetPosibleCaptures(pawnToMove, player, board);
                    isEmptyList = availableMoves.Length == 0 && avilableCaptures.Length == 0;
                    messageText = isEmptyList ? "noMove" : "whereToPlace";
                }

                _message.WriteMessage(messageText);
            } while (!isValidChoice || isEmptyList);


            //Player decides where to move his pawn
            do
            {
                fieldToMoveTo = AskUserForCoordinates(boardSize, player);
                isValidChoice = availableMoves.Contains(fieldToMoveTo) || avilableCaptures.Contains(fieldToMoveTo);
                if (!isValidChoice)
                    _message.WriteMessage("unavailableMove");
            } while (!isValidChoice);

            //Console.Out.WriteLine("Your pawn will move to {0}", fieldToMoveTo);

            // move or capture + move in Game class method MakeMoveOrCapture
            return new (int, int)[2] {pawnToMove, fieldToMoveTo};
        }
        
        
        private (int, int) AskUserForCoordinates (int boardSize = 10, int player = 1)
        {
            (int?, int?) pawnCoordinates;

            do
            {
                string userInput = Console.ReadLine();
                pawnCoordinates = ValidateInput(userInput, boardSize);
            } while (pawnCoordinates == (null, null));

            return  ConverrtNullableInts(pawnCoordinates);
        }

        
        private (int?, int?) ValidateInput(string userInput, int boardSize)
        {
            int firstLetterAsciiCode = 65;
            int numberZeroAciiCode = 49;
            //int alphabetLength = 25;
            int? col, row;


            if (userInput.Length > 1 && userInput.Length < 4)
            {
                char[] charArr = userInput.ToUpper().ToCharArray(0, userInput.Length);
                col = (int) charArr[0] - firstLetterAsciiCode;
                row = userInput.Length == 2
                    ? charArr[1] - numberZeroAciiCode
                    : (((charArr[1] - numberZeroAciiCode + 1) * 10) + (charArr[2] - numberZeroAciiCode));

                if (col >= boardSize || col < 0 || row >= boardSize || row < 0)
                {
                    _message.WriteMessage("outOfRange");
                    (row, col) = (null, null);
                }
            }
            else
            {
                _message.WriteMessage("invalidInput");
                (row, col) = (null, null);
            }

            return (row, col);
        }

        /*
        private (int?, int?) nullCoordinates()
        {
            Console.Out.WriteLine("Invalid input! Please enter correct coordinates");
            return (null, null);
        }
        */
        
        
        private (int, int) ConverrtNullableInts((int? rowNull, int? colNull) coordinates)
        {
            var colConverted = coordinates.colNull ?? default(int); //colConverted = colNull is not null ? colNum : default(int);
            var rowConverted = coordinates.rowNull ?? default(int);

            return (rowConverted, colConverted);
        }
        
     
        private bool ValidPawn(Pawn[,] board, (int row, int col) coordinates, int player = 1)
        {
            Pawn pawn = board[coordinates.row, coordinates.col];

            if (pawn is null)
                return false;
            else 
                switch (player)
                {
                    case 1 when pawn.IsWhite:
                    case 2 when !pawn.IsWhite:
                        return true;
                    default:
                        return false;
                };
        }
        
        
        public (int, int)[] GetPosibleMoves((int pawnRow, int pawnCol) pawnLocation, int player, Board board)
        {
            (int, int)[] possibleCoordinates;

            int newRow = player == 1 ? pawnLocation.pawnRow - 1 : pawnLocation.pawnRow + 1;
            (int, int) rightMoveField = (newRow, pawnLocation.pawnCol + 1);
            (int, int) leftMoveField = (newRow, pawnLocation.pawnCol - 1);

            bool rMoveAvailable = pawnLocation.pawnCol < board.Size - 1 && isAnEmptyField(rightMoveField, board);
            bool lMoveAvailable = pawnLocation.pawnCol > 0 && isAnEmptyField(leftMoveField, board);

            if (newRow >= board.Size || newRow < 0 || (!rMoveAvailable && !lMoveAvailable))
                return possibleCoordinates = new (int, int)[] { };
            else if (rMoveAvailable && !lMoveAvailable)
                return possibleCoordinates = new (int, int)[] {rightMoveField};
            else if (!rMoveAvailable)
                return possibleCoordinates = new (int, int)[] {leftMoveField};
            else
                return possibleCoordinates = new (int, int)[] {leftMoveField, rightMoveField};
        }

        
        private bool isAnEmptyField((int row, int col) fieldCoordinates, Board board)
        {
            
            if (fieldCoordinates.row < 0 || fieldCoordinates.row >= board.Size)
                return false;
            return (board.board[fieldCoordinates.row, fieldCoordinates.col] is null) ? true : false;
        }

        public (int, int)[] GetPosibleCaptures((int pawnRow, int pawnCol) pawnLocation, int player, Board board)
        {
            (int, int)[] possibleCoordinatesCapture;
            
            int newRow = player == 1 ? pawnLocation.pawnRow - 2 : pawnLocation.pawnRow + 2;
            bool rowIsValid = newRow <= board.Size - 1 && newRow >= 1;
            (int newRow, int newCol) rightMoveField = (newRow, pawnLocation.pawnCol + 2);
            (int newRow, int newCol) leftMoveField = (newRow, pawnLocation.pawnCol - 2);
            
            int rowBetween = player == 1 ? pawnLocation.pawnRow - 1 : pawnLocation.pawnRow + 1;
            (int newRow, int newCol) rightFieldBetween = (rowBetween, pawnLocation.pawnCol + 1);
            (int newRow, int newCol) leftFieldBetween = (rowBetween, pawnLocation.pawnCol - 1);
            
            bool rMoveAvailable = rowIsValid && rightMoveField.newCol <= board.Size - 1 && isAnEmptyField(rightMoveField, board) && isFieldWithOppositePawn(rightFieldBetween, board, player);
            bool lMoveAvailable = rowIsValid && leftMoveField.newCol >= 0 && isAnEmptyField(leftMoveField, board) && isFieldWithOppositePawn(leftFieldBetween, board, player);

            if (newRow >= board.Size || newRow < 0 || (!rMoveAvailable && !lMoveAvailable))
            {
                return possibleCoordinatesCapture = new (int, int)[] { };
            }
            else if(rMoveAvailable && !lMoveAvailable)
            {
                return possibleCoordinatesCapture = new (int, int)[] {rightMoveField};
            }
            else if(!rMoveAvailable && lMoveAvailable)
            {
                return possibleCoordinatesCapture = new (int, int)[] {leftMoveField};
            }
            else
            {
                return possibleCoordinatesCapture = new (int, int)[] {leftMoveField, rightMoveField};
            }
        }

        private bool isFieldWithOppositePawn((int row, int col) fieldCoordinate, Board board, int player)
        {
            if (!isAnEmptyField(fieldCoordinate, board))
            {
                if (player == 1)
                {
                    return (!board.board[fieldCoordinate.row, fieldCoordinate.col].IsWhite) ? true : false;
                }
                else
                {
                    return (board.board[fieldCoordinate.row, fieldCoordinate.col].IsWhite) ? true : false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}