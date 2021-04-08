using System;

namespace Warcaby
{
    public class Menu
    {
        public string GameMode { get; set; }

        public Menu()
        {
            string mode;
            bool validInput = true;
                
            do
            {
                if (!validInput)
                    Console.Out.WriteLine("Incorrect input. Enter one of the following digits: '1', '2' or '3'");
                
                Console.Out.WriteLine("Please choose one of the following modes:\n\t1. HUMAN-HUMAN\n\t2. HUMAN-COMPUTER\n\t3. COMPUTER-COMPUTER");
                mode = Console.ReadLine().Trim();
                validInput = mode == "1" || mode == "2" || mode == "3";
                
            } while (!validInput);
            
            switch (mode)
            {
                case "1":
                    GameMode = "HUMAN-HUMAN";
                    break;
                case "2":
                    GameMode = "HUMAN-COMPUTER";
                    break;
                case "3":
                    GameMode = "COMPUTER-COMPUTER";
                    break;
            }
        }
    }
}