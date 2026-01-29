using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    public struct Position
    {
        public int x;
        public int y;

        public Position(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }
    public class Player
    {
        public Position Position;
        public ConsoleColor Color;

        public Player(Position position, ConsoleColor color)
        {
            Position = position;
            Color = color;
        }
    }

    public class Enemy
    {
        public Position Position;
        public ConsoleColor Color;

        public Enemy(Position position, ConsoleColor color)
        {
            Position = position;
            Color = color;
        }

    }


    internal class Program
    {
        static void Main(string[] args)
        {
        }
    }
}
