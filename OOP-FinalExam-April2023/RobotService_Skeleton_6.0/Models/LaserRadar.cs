using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotService.Models
{
    public class LaserRadar : Supplement
    {
        private const int LaserReaderInterfaceStandard = 20082;
        private const int LaserReaderBatteryUsage = 5000;
        public LaserRadar() 
            : base(LaserReaderInterfaceStandard, LaserReaderBatteryUsage)
        {
        }
    }
}
