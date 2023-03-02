using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telephony
{
    public class Smartphone : ICaller, IBrowser
    {
        public void Browse(string url)
        {
            if(url.Any(c=>char.IsNumber(c)))
            {
                Console.WriteLine($"Invalid URL!");
            }
            else
            {
                Console.WriteLine($"Browsing: {url}!");
            }
        }


        public void Call(string number)
        {
            if (number.Any(c => !char.IsNumber(c)))
            {
                Console.WriteLine("Invalid number!");
            }
            else
            {
                Console.WriteLine($"Calling... {number}");
            }
        }

    }
}
