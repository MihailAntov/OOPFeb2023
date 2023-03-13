using System.Collections.Generic;

namespace P03.DetailPrinter
{
    class Program
    {
        static void Main()
        {
            IEmployee vili = new Employee("Vili");
            IEmployee chocho = new Manager("Chocho", new string[] { "mqu", "mqu" });
            IEmployee alan = new SuperManager("Alan", new string[] { "WHAT" }, "white");
            List<IEmployee> employees = new List<IEmployee>();
            employees.Add(vili);
            employees.Add(chocho);
            employees.Add(alan);


            foreach(IEmployee employee in employees)
            {
                System.Console.WriteLine(employee.Print());
            }
        }
    }
}
