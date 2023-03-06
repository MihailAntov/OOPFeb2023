namespace SetCover
{
    using System.Collections.Generic;
    using System.Linq;
    using System;
    class StartUp
    {
        static void Main(string[] args)
        {
            int[] universe = Console.ReadLine()
                .Split(", ", StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();

            int numberOfSets = int.Parse(Console.ReadLine());
            int[][] sets = new int[numberOfSets][];
            for (int i = 0; i < numberOfSets; i++)
            {
                sets[i] = Console.ReadLine()
                    .Split(", ", StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToArray();
            }

            List<int[]> result = ChooseSets(sets, universe);
            Console.WriteLine($"Sets to take ({result.Count}):");

            foreach (int[] set in result)
            {
                Console.WriteLine($"{{ {string.Join(", ",set)} }}");
            }
        }

        public static List<int[]> ChooseSets(IList<int[]> sets, IList<int> universe)
        {
            sets = sets.ToList();
            List<int[]> result = new List<int[]>();
            
            while(universe.Count > 0)
            {
                int[] currentSet = sets
                    .OrderByDescending(s => s.Count(x => universe.Contains(x)))
                    .FirstOrDefault();

                result.Add(currentSet);
                sets.Remove(currentSet);

                universe = universe.Where(i => !currentSet.Contains(i)).ToArray();
            }

            return result;
        }
    }
}
