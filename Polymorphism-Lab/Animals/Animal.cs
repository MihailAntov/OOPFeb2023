namespace Animals;

public abstract class Animal
{
    protected string name;
    protected string favoriteFood;

    protected Animal(string name, string favoriteFood)
    {
        this.name = name;
        this.favoriteFood = favoriteFood;
    }

    public abstract string ExplainSelf();
    
}
