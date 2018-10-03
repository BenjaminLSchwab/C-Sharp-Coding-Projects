﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwentyOne
{
    public class Player
    {
        public Player(string name, int balance)
        {
            Hand = new List<Card>();
            Name = name;
            Balance = balance;
        }

        public List<Card> Hand { get; set; }
        public int Balance { get; set; }
        public string Name { get; set; }
        public bool IsActivleyPlaying { get; set; }
        public bool Stay { get; set; }

        public static Game operator + (Game game, Player player)
        {
            game.Players.Add(player);
            return game;
        }

        public static Game operator - (Game game, Player player)
        {
            game.Players.Remove(player);
            return game;
        }
    }
}
