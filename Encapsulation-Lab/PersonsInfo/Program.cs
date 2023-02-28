

namespace PersonsInfo;

public class StartUp
{
    public static void Main()
    {
        int n = int.Parse(Console.ReadLine());

        List<Person> persons = new List<Person>();
        for (int i = 0; i < n; i ++)
        {
            string[] playerArgs = Console.ReadLine()
                .Split(" ", StringSplitOptions.RemoveEmptyEntries);

            string firstName = playerArgs[0];
            string lastName = playerArgs[1];
            int age = int.Parse(playerArgs[2]);
            decimal salary = decimal.Parse(playerArgs[3]);

            persons.Add(new Person(firstName, lastName, age, salary));
                
        }

        Team team = new Team("SoftUni");

        foreach (Person person in persons)
        {
            team.AddPlayer(person);
        }

        Console.WriteLine(team);








    }

}