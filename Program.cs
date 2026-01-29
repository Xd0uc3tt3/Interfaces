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
        Position Move(Position enemyPosition, Position playerPosition);
    }

    public class AggressiveMoveStrategy : IMoveStrategy
    {
        public Position Move(Position enemyPosition, Position playerPosition)
        {
            int x = playerPosition.x - enemyPosition.x;
            int y = playerPosition.y - enemyPosition.y;

            int stepX = x == 0 ? 0 : x / Math.Abs(x);
            int stepY = y == 0 ? 0 : y / Math.Abs(y);

            return new Position(enemyPosition.x + stepX, enemyPosition.y + stepY);
        }
    }

    public class PassiveMoveStrategy : IMoveStrategy
    {
        public Position Move(Position enemyPosition, Position playerPosition)
        {
            int newX = enemyPosition.x;
            int newY = enemyPosition.y;

            if (enemyPosition.x < playerPosition.x)
            {
                newX++;
            }
                
            else if (enemyPosition.x > playerPosition.x)
            {
                newX--;
            }

            if (newX == playerPosition.x)
            {
                if (enemyPosition.y < playerPosition.y) newY++;
                else if (enemyPosition.y > playerPosition.y) newY--;
            }

            return new Position(newX, newY);
        }
    }

    public class RandomMoveStrategy : IMoveStrategy
    {
        private static Random _random = new Random();

        public Position Move(Position enemyPosition, Position playerPosition)
        {
            int x = _random.Next(-1, 2);
            int y = _random.Next(-1, 2);
            return new Position(enemyPosition.x + x, enemyPosition.y + y);
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

        public void Move(Position playerPosition)
        {
            Position = _moveStrategy.Move(Position, playerPosition);
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
            IMoveStrategy random = new RandomMoveStrategy();

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
                {
                    enemy.Move(player.Position);
                }
                else if (key == ConsoleKey.I)
                {
                    enemy._moveStrategy = aggressive;
                }
                else if (key == ConsoleKey.O)
                {
                    enemy._moveStrategy = passive;
                }
                else if (key == ConsoleKey.P)
                {
                    enemy._moveStrategy = random;
                }
            }
        }
    }
}
