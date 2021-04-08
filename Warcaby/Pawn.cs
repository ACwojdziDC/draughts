using System;

namespace Warcaby
{
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

        public Tuple<int, int> coordinates { get; set; }

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

}