using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P03.DetailPrinter
{
    public class SuperManager : Manager
    {
        private string shirtColor;
        public SuperManager(string name, ICollection<string> documents, string shirtColor) : base(name, documents)
        {
            ShirtColor = shirtColor;
        }

        public string ShirtColor { get { return shirtColor; } set { shirtColor = value; } }

        public override string Print()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(base.Print());
            sb.AppendLine($" Shirt Color : {ShirtColor}");
            return sb.ToString().TrimEnd();
        }
    }
}
