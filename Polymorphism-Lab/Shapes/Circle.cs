namespace Shapes;

public class Circle : Shape
{
    private double radius;


    public Circle(double radius)
    {
        Radius = radius;
    }
    public double Radius
    {
        get { return radius; }
        private set { radius = value; }
    }






    public override double CalculateArea()
    {
        return Math.PI * Math.Pow(radius, 2);
    }

    public override double CalculatePerimeter()
    {
        return 2 * Math.PI * radius;
    }

    public override string Draw()
    {
        return base.Draw() + this.GetType().Name;
    }


}