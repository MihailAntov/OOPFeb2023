using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Prototype
{
    [Serializable]
    public class Sandwich : SandwichPrototype
    {
        private string bread;
        private string meat;
        private string cheese;
        private string veggies;
        public Sandwich(string bread, string meat, string cheese, string veggies)
        {
            this.bread = bread;
            this.meat = meat;
            this.cheese = cheese;
            this.veggies = veggies;
        }

        public override SandwichPrototype Clone()
        {
            string ingredientList = GetIngredientList();
            Console.WriteLine($"Cloning sandwich with {ingredientList}");
            
            //shallow clone:
            //return this.MemberwiseClone() as SandwichPrototype;


            //deep clone:
            IFormatter formatter = new BinaryFormatter();
            using var stream = new MemoryStream();
            formatter.Serialize(stream, this);
            stream.Position = 0;
            return formatter.Deserialize(stream) as Sandwich;

            
            
        }

        private string GetIngredientList()
        {
            return $"{bread}, {meat}, {cheese}, {veggies}";
        }


    }
}
