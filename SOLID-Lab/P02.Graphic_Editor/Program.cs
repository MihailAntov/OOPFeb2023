using System.Collections.Generic;

namespace P02.Graphic_Editor
{
    class Program
    {
        static void Main()
        {
            List<IShape> shapes = new List<IShape>();
            IShape circle = new Circle();
            IShape square = new Square();
            IShape rectangle = new Rectangle();
            IShape triangle = new Triangle();

            shapes.Add(circle);
            shapes.Add(square);
            shapes.Add(rectangle);
            shapes.Add(triangle);



            foreach(IShape shape in shapes)
            {
                shape.Draw();
            }
        }
    }
}
