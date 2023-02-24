using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Animals
{
    public class Tomcat : Cat
    {

        private const string kittenGender = "Male";
        public Tomcat(string name, int age) : base(name, age, kittenGender)
        {

        }

        public Tomcat(string name, int age, string gender) : base(name, age, kittenGender)
        {

        }

        public override string ProduceSound()
        {
            return "MEOW";
        }
    }
}
