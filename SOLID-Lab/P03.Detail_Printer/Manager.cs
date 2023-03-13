using System.Collections.Generic;
using System.Text;

namespace P03.DetailPrinter
{
    public class Manager : Employee
    {
        public Manager(string name, ICollection<string> documents) : base(name)
        {
            this.Documents = new List<string>(documents);
        }

        public IReadOnlyCollection<string> Documents { get; set; }

        public override string Print()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(base.Print());
            if(this.Documents.Count > 0)
            {
                sb.AppendLine(" Documents:");
            }
            foreach(string document in this.Documents)
            {
                sb.AppendLine($"  {document}");
            }

            return sb.ToString().TrimEnd();
        }
    }
}
