namespace Shapes;

public class StartUp
{
    static void Main(string[] args)
    {
        Shape rectangle = new Rectangle(5, 10);
        Shape circle = new Circle(6);

        Console.WriteLine(rectangle.CalculateArea());
        Console.WriteLine(rectangle.CalculatePerimeter());
        Console.WriteLine(rectangle.Draw());
        Console.WriteLine();

        Console.WriteLine(circle.CalculateArea());
        Console.WriteLine(circle.CalculatePerimeter());
        Console.WriteLine(circle.Draw());

    }
}
