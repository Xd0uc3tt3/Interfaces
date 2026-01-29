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

    public interface IMoveStrategy
    {
        Position Move(Position currentPosition);
    }

    public class AggressiveMoveStrategy : IMoveStrategy
    {
        public Position Move(Position currentPosition)
        {
            return new Position(currentPosition.x + 1, currentPosition.y);
        }
    }

    public class PassiveMoveStrategy : IMoveStrategy
    {
        public Position Move(Position currentPosition)
        {
            return new Position(currentPosition.x - 1, currentPosition.y);
        }
    }

    public class Enemy
    {
        public Position Position;
        public ConsoleColor Color;
        public IMoveStrategy _moveStrategy;

        public Enemy(Position position, ConsoleColor color, IMoveStrategy moveStrategy)
        {
            Position = position;
            Color = color;
            _moveStrategy = moveStrategy;
        }

        public void Move()
        {
            Position = _moveStrategy.Move(Position);
        }
    }


    internal class Program
    {
        public static Player player;
        static void Main(string[] args)
        {
            player = new Player(new Position(10, 10), ConsoleColor.Green);

            IMoveStrategy aggressive = new AggressiveMoveStrategy();
            IMoveStrategy passive = new PassiveMoveStrategy();

            Enemy enemy = new Enemy(new Position(5, 5), ConsoleColor.Red, aggressive);

            while (true)
            {
                Console.Clear();

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.SetCursorPosition(0, 0);
                Console.WriteLine("(M) Move Enemy");
                Console.WriteLine("(I) Aggressive");
                Console.WriteLine("(O) Passive");
                Console.WriteLine("(P) Random");
                Console.WriteLine(enemy._moveStrategy.GetType().Name);
                Console.WriteLine();

                Console.ForegroundColor = player.Color;
                Console.SetCursorPosition(player.Position.x, player.Position.y);
                Console.Write("P");

                Console.ForegroundColor = enemy.Color;
                Console.SetCursorPosition(enemy.Position.x, enemy.Position.y);
                Console.Write("E");

                Console.ResetColor();

                ConsoleKey key = Console.ReadKey(true).Key;

                if (key == ConsoleKey.M)
                    enemy.Move();
                else if (key == ConsoleKey.I)
                    enemy._moveStrategy = aggressive;
                else if (key == ConsoleKey.O)
                    enemy._moveStrategy = passive;
            }
        }
    }
}
