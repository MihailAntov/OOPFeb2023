using WildFarm.Models.Interfaces;

namespace WildFarm.Models
{
    public abstract class Animal : IAnimal
    {
        protected  double WeightGainOnFeed;
        public Animal(string name, double weight, int foodEaten)
        {
            Name = name;
            Weight = weight;
            FoodEaten = foodEaten;
        }
        public string Name { get; private set; }

        public double Weight { get; private set; }

        public int FoodEaten { get; protected set; }
        
        public virtual void Feed(IFood food)
        {
            Weight += WeightGainOnFeed * food.Quantity;
            FoodEaten += food.Quantity;
        }

        public abstract string AskForFood();
        protected string CantEat(IFood food)
        {
            return $"{this.GetType().Name} does not eat {food.GetType().Name}!";

        }
    }
}
