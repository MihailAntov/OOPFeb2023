using WildFarm.Models.Interfaces;

namespace WildFarm.Models
{
    public abstract class Mammal : Animal, IMammal
    {
        public Mammal(string name, double weight, int foodEaten, string livingRegion) 
            :base(name, weight, foodEaten)
        {
            LivingRegion = livingRegion;
        }
        public string LivingRegion { get; private set; }
        public override string ToString()
        {
            return $"{this.GetType().Name} [{Name}, {Weight}, {LivingRegion}, {FoodEaten}]";

        }
    }
}
