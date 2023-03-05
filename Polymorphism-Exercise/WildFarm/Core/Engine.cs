using System;
using System.Collections.Generic;
using System.Text;
using WildFarm.Models;
using WildFarm.Models.Interfaces;

namespace WildFarm.Core
{
    public class Engine
    {
        private List<IAnimal> animals;
        public Engine()
        {
            animals = new List<IAnimal>();
        }
        
        public void Run()
        {
            HandleInput();
            PrintAnimals();
        }

        private void HandleInput()
        {
            string input;


            while ((input = Console.ReadLine()) != "End")
            {
                string foodInput = Console.ReadLine();

                IAnimal animal = DefineAnimal(input);
                FeedAnimal(animal, foodInput);
            }
        }

        public IAnimal DefineAnimal(string input)
        {
            IAnimal animal = null;
            string[] animalArgs = input.Split(" ", StringSplitOptions.RemoveEmptyEntries);

            string type = animalArgs[0];
            string name = animalArgs[1];
            double weight = double.Parse(animalArgs[2]);
            double wingSize = default(double);
            string livingRegion = default(string);
            string breed = default(string);
            switch(type)
            {
                case "Hen":
                    wingSize = double.Parse(animalArgs[3]);
                    animal = new Hen(name, weight, wingSize);
                    break;
                case "Owl":
                    wingSize = double.Parse(animalArgs[3]);
                    animal = new Owl(name, weight, wingSize);
                    break;
                case "Mouse":
                    livingRegion = animalArgs[3];
                    animal = new Mouse(name, weight, livingRegion);
                    break;
                case "Cat":
                    livingRegion = animalArgs[3];
                    breed = animalArgs[4];
                    animal = new Cat(name, weight, livingRegion, breed);
                    break;
                case "Dog":
                    livingRegion = animalArgs[3];
                    animal = new Dog(name, weight, livingRegion);
                    break;
                case "Tiger":
                    livingRegion = animalArgs[3];
                    breed = animalArgs[4];
                    animal = new Tiger(name, weight, livingRegion, breed);
                    break;
            }
            animals.Add(animal);
            Console.WriteLine(animal.AskForFood());
            return animal;
        }

        public void FeedAnimal(IAnimal animal, string foodInput)
        {
            
            string[] foodArgs = foodInput.Split(" ", StringSplitOptions.RemoveEmptyEntries);
            string type = foodArgs[0];
            int quantity = int.Parse(foodArgs[1]);
            IFood food = null;
            switch(type)
            {
                case "Vegetable":
                    food = new Vegetable(quantity);
                    break;
                case "Fruit":
                    food = new Fruit(quantity);
                    break;
                case "Meat":
                    food = new Meat(quantity);
                    break;
                case "Seeds":
                    food = new Seeds(quantity);
                    break;
            }

            animal.Feed(food);
        }

        public void PrintAnimals()
        {
            
            foreach(IAnimal animal in animals)
            {
                Console.WriteLine(animal.ToString());
            }

            
        }

    }
}
