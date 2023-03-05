namespace Animals;

public class Dog : Animal
{
    public Dog(string name, string favoriteFood) : base(name, favoriteFood)
    {

    }
    public override string ExplainSelf()
    {
        return $"I am {name} and my fovourite food is {favoriteFood} DJAAF";
    }
}
