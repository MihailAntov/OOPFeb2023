using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BorderControl
{
    public class Robot : ICheckable
    {
        public Robot(string model, string serialNumber)
        {
            Model = model;
            SerialNumber = serialNumber;
        }

        public string Model { get; set; }
        public string SerialNumber { get; set; }

        public string Credentials => SerialNumber;

        public bool CheckId(string lastDigits)
        {
            int checkLength = lastDigits.Length;

            string currentLastDigits = String.Join("",SerialNumber.TakeLast(checkLength));
            return currentLastDigits == lastDigits;
        }
    }
}
