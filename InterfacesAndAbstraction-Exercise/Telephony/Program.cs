using System;
using System.Collections.Generic;

namespace Telephony
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            IEnumerable<string> numbers = Console.ReadLine()
                .Split(" ", StringSplitOptions.RemoveEmptyEntries);

            IEnumerable<string> urls = Console.ReadLine()
                .Split(" ", StringSplitOptions.RemoveEmptyEntries);

            Smartphone smartPhone = new Smartphone();
            StationaryPhone stationaryPhone = new StationaryPhone();

            foreach(string number in numbers)
            {
                if(number.Length == 7)
                {
                    stationaryPhone.Call(number);
                }
                else
                {
                    smartPhone.Call(number);
                }
            }

            foreach(string url in urls)
            {
                smartPhone.Browse(url);
            }
        }
    }
}