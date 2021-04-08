using System;

namespace Warcaby
{
    public class Messages
    {
        private string[] messagesList = {
            "Please enter your pawn coordinates",
            "Incorrect pawn coordinates. Try again.",
            "This is not your pawn. Try again.",
            "This pawn cannot be moved. Choose another one.",
            "This move is unavailable. Choose a different field.",
            "Where to place your pawn?",
            "The board is too small. Coordinates out of range. Try again",
            "Invalid input! Please enter correct coordinates (e.g. a1, b2).",
        };

        public void WriteMessage(string messageType, int player = 1)
        {
            string message = null;
            switch (messageType)
            {
                case "choosePawn":
                    message = $"Player's {player} move.\n" + messagesList[0];
                    break;
                case "incorrectCoord":
                    message = messagesList[1];
                    break;
                case "wrongPawn":
                    message = messagesList[2];
                    break;
                case "noMove":
                    message = messagesList[3];
                    break;
                case "unavailableMove":
                    message = messagesList[4];
                    break;
                case "whereToPlace":
                    message = messagesList[5];
                    break;
                case "outOfRange":
                    message = messagesList[6];
                    break;
                case "invalidInput":
                    message = messagesList[7];
                    break;
                default:
                    message = messageType;
                    break;
            }

            Console.Out.WriteLine(message);
        }

    }
}