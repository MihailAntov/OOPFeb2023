using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleSnake.GameObjects
{
    public abstract class Food : Point
    {
        private Random random;
        private Wall wall;
        private char foodSymbol;
        protected Food(Wall wall, char foodSymbol, int points)
            : base(wall.LeftX, wall.TopY)
        {
            random = new Random();
            FoodPoints = points;
            this.foodSymbol = foodSymbol;
            this.wall = wall;
        }

        public int FoodPoints { get; private set; }

        public void SetRandomPosition(Queue<Point> snakePoints)
        {
            LeftX = random.Next(2, wall.LeftX - 2);
            TopY = random.Next(2, wall.TopY - 2);
            bool isPartOfSnake = snakePoints.Any(p => p.TopY == this.TopY && p.LeftX == this.LeftX);
            while (isPartOfSnake)
            {
                LeftX = random.Next(2, wall.LeftX-2);
                TopY = random.Next(2, wall.TopY-2);

                isPartOfSnake = snakePoints.Any(p=>p.LeftX == this.LeftX && p.TopY == this.TopY);
            }
            Console.BackgroundColor = ConsoleColor.Red;
            Draw(foodSymbol);
            Console.BackgroundColor = ConsoleColor.White;
        }

        public bool IsFoodPoint(Point snake)
        {
            return snake.TopY == this.TopY && snake.LeftX == this.LeftX;
        }
    }


}
