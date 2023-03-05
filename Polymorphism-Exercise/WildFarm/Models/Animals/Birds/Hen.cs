using WildFarm.Models.Interfaces;

namespace WildFarm.Models
{
    public class Hen : Bird
    {
        private const double HenWeightGainOnFeed = 0.35;
        public Hen(string name, double weight, double wingSize)
            : base(name, weight, 0, wingSize)
        {
            WeightGainOnFeed = HenWeightGainOnFeed;
        }
        public override string AskForFood()
        {
            return "Cluck";
        }

        
    }
}
