using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleSnake.GameObjects
{
    public class Wall : Point
    {
        private const char WallSymbol = '\u25A0';
        public Wall(int leftX, int topY) : base(leftX, topY)
        {
            DrawHorizontalLine(0);
            DrawHorizontalLine(TopY);
            DrawVerticalLine(0);
            DrawVerticalLine(LeftX-1);
        }

        private void DrawHorizontalLine(int topY)
        {
            for (int leftX = 0; leftX < LeftX; leftX++)
            {
                Draw(leftX, topY, WallSymbol);
            }
        }

        private void DrawVerticalLine(int leftX)
        {
            for (int topY = 0; topY < TopY; topY++)
            {
                Draw(leftX, topY, WallSymbol);
            }
        }

        public bool IsPointOfWall(Point snake)
        {
            return snake.LeftX == 0
                || snake.TopY == 0
                || snake.LeftX == this.LeftX - 1
                || snake.TopY == this.TopY;
        }
    }
}
