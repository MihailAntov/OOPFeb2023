﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilitaryElite.Models
{
    public class Repair : IRepair
    {
        private string partName;
        private int hoursWorked;

        public Repair(string partName, int hoursWorked)
        {
            PartName = partName;
            HoursWorked = hoursWorked;
        }

        public string PartName
        {
            get { return partName; }
            set { partName = value; }
        }

        

        public int HoursWorked
        {
            get { return hoursWorked; }
            set { hoursWorked = value; }
        }
        public override string ToString()
        {
            return $"Part Name: {PartName} Hours Worked: {HoursWorked}";
        }


    }
}
