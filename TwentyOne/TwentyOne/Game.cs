using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TwentyOne
{
    public abstract class Game
    {
        List<Player> _players = new List<Player>();
        public List<Player> Players { get { return _players; } set { _players = value; } }

        public string Name { get; set; }
        public string Dealer { get; set; }

        Dictionary<Player, int> _bets = new Dictionary<Player, int>();
        public Dictionary<Player, int> Bets { get { return _bets; } set { _bets = value; } }

        public abstract void Play();

        public virtual void ListPlayers()
        {
            foreach(Player player in Players)
            {
                Console.WriteLine(player.Name);
            }
        }

        public void StartLog()
        {
            using (StreamWriter file = new StreamWriter(@"C:\Users\DwarfKhan\Documents\work\TechAcademyLocalRepos\C-Sharp-Coding-Projects\TwentyOne\logs\cardlog.txt", true))
            {
                file.WriteLine("New hand on {0}", DateTime.Now);
                file.WriteLine("Players:");
                foreach (Player player in Players)
                {
                    file.WriteLine("{0}, starting balance:{1}", player.Name, player.Balance);
                }
            }
        }

        public void EndLog()
        {
            using (StreamWriter file = new StreamWriter(@"C:\Users\DwarfKhan\Documents\work\TechAcademyLocalRepos\C-Sharp-Coding-Projects\TwentyOne\logs\cardlog.txt", true))
            {
                file.WriteLine("End hand on {0}", DateTime.Now);
                file.WriteLine("Players:");
                foreach (Player player in Players)
                {
                    file.WriteLine("{0}, ending balance:{1}", player.Name, player.Balance);
                }
            }
        }
    }
}
