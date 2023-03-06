namespace SumOfCoins
{
    using System.Collections.Generic;
    using System;
    using System.Linq;
    public class StartUp
    {
        public static void Main(string[] args)
        {
            int[] coins = Console.ReadLine()
                .Split(",",StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .OrderByDescending(x => x)
                .ToArray();

            int targetSum = int.Parse(Console.ReadLine());
            try
            {
                Dictionary<int, int> result = ChooseCoins(coins, targetSum);
                Console.WriteLine($"Number of coins to take: {result.Values.Sum()}");
                foreach (KeyValuePair<int, int> coin in result)
                {
                    Console.WriteLine($"{coin.Value} coin(s) with value {coin.Key}");
                }
            }
            catch (InvalidOperationException )
            {
                Console.WriteLine("Error");
            }

            
        }

        public static Dictionary<int, int> ChooseCoins(IList<int> coins, int targetSum)
        {
            Dictionary<int, int> result = new Dictionary<int, int>();
            while(targetSum > 0)
            {
                bool suitableCoin = false;
                foreach(int coin in coins)
                {
                    if(coin <= targetSum)
                    {
                        if(!result.ContainsKey(coin))
                        {
                            result.Add(coin, 0);
                        }
                        int numberOfCoins = targetSum / coin;
                        suitableCoin = true;
                        targetSum -= coin * numberOfCoins;
                        result[coin] += numberOfCoins;
                        break;
                    }
                }

                if(!suitableCoin)
                {
                    throw new InvalidOperationException();
                }
            }
            return result;
        }
    }
}