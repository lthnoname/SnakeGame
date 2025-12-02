using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SnakeGame.Models;

namespace SnakeGame.Services
{
    internal class CollisionDetector
    {
        public enum CollisionType
        {
            None,
            Wall,
            Self,
            Food
        }

        public static CollisionType Detect(Position head, IReadOnlyList<Position> snakeBody, int columns, int rows, Food food = null)
        {
            if (IsWallCollision(head, columns, rows)) return CollisionType.Wall;
            if (IsSelfCollision(head, snakeBody)) return CollisionType.Self;
            if (IsFoodCollision(head, food)) return CollisionType.Food;
            return CollisionType.None;
        }

        public static bool IsWallCollision(Position p, int columns, int rows)
        {
            if (columns <= 0) throw new ArgumentOutOfRangeException(nameof(columns));
            if (rows <= 0) throw new ArgumentOutOfRangeException(nameof(rows));
            return p.X < 0 || p.X >= columns || p.Y < 0 || p.Y >= rows;
        }

        public static bool IsSelfCollision(Position head, IReadOnlyList<Position> snakeBody)
        {
            if(snakeBody == null) throw new ArgumentNullException(nameof(snakeBody));
            if (snakeBody.Count <= 1) return false;

            for(int i = 1; i < snakeBody.Count; i++)
            {
                if (head == snakeBody[i]) return true;
            }
            return false;
        }

        public static bool IsFoodCollision(Position head, Food food)
        {
            if(food == null) return false;
            return food.IsAt(head);
        }
    }
}
