namespace P03.DetailPrinter
{
    public class Employee : IEmployee
    {
        public Employee(string name)
        {
            this.Name = name;
        }

        public string Name { get; set; }

        public virtual string Print()
        {
            return this.Name;
        }
    }
}
