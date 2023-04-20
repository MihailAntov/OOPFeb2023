using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleSnake.GameObjects
{
    public class Snake
    {
        private const char SnakeSymbol = '\u25cf';
        private Queue<Point> points;
        private Food[] foods;
        private Wall wall;
        private int nextLeftX;
        private int nextTopY;
        private int foodIndex;
        private Random random;
        private const char emptySpace = ' ';
        public Snake(Wall wall)
        {
            points = new Queue<Point>();
            foods = new Food[3];
            random = new Random();
            foodIndex = RandomFoodNumber;
            this.wall = wall;
            GetFoods();
            CreateSnake();
            CreateFood();
            

        }

        private int RandomFoodNumber => random.Next(0, 3);

        public void CreateSnake()
        {
            for (int topY = 1; topY <= 6; topY++)
            {
                points.Enqueue(new Point(2, topY));
            }
            //Console.SetCursorPosition(2, 4);
        }
        public void CreateFood()
        {
            foodIndex = RandomFoodNumber;
            foods[foodIndex].SetRandomPosition(points);
        }

        private void GetFoods()
        {
            this.foods[0] = new DollarFood(this.wall);
            this.foods[1] = new AsteriskFood(this.wall);
            this.foods[2] = new HashFood(this.wall);
        }

        public bool IsMoving(Point direction)
        {
            Point currentSnakeHead = this.points.Last();
            GetNextElement(direction, currentSnakeHead);

            bool isPointOfSnake = points.Any(p => p.LeftX == nextLeftX && p.TopY == nextTopY);
            if (isPointOfSnake)
            {
                return false;
            }
            Point snakeNewHead = new Point(nextLeftX, nextTopY);
            if (wall.IsPointOfWall(snakeNewHead))
            {
                return false;
            }

            this.points.Enqueue(snakeNewHead);
            snakeNewHead.Draw(SnakeSymbol);

            if (foods[foodIndex].IsFoodPoint(snakeNewHead))
            {
                this.Eat(direction, currentSnakeHead);
            }


            Point snakeTail = points.Dequeue();
            snakeTail.Draw(emptySpace);
            return true;



        }

        private void Eat(Point direction, Point currentSnakeHead)
        {
            int length = foods[foodIndex].FoodPoints;
            for (int i = 0; i < length; i++) 
            {
                points.Enqueue(new Point(nextLeftX, nextTopY));
                GetNextElement(direction, currentSnakeHead);
            }

            foodIndex = RandomFoodNumber;
            foods[foodIndex].SetRandomPosition(points);

        }

        private void GetNextElement(Point direction, Point currentSnakeHead)
        {
            this.nextLeftX = currentSnakeHead.LeftX + direction.LeftX;
            this.nextTopY = currentSnakeHead.TopY + direction.TopY;
        }

    }
}
