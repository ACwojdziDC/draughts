namespace Warcaby
{
    public class Player
    {
        public string ColorPawns { get; set; }
        public int Id { get; set; }
        public bool Human { get; set; }

        public Player(string colorPawns, int id, bool human)
        {
            colorPawns = ColorPawns;
            id = Id;
            Human = human;
        }
    }
}