﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Models.Products.Peripherals
{
    public class Keyboard : Peripheral
    {
        public Keyboard(int id, string manufacturer, string model, decimal price, double overallPerformance, string connectionType) 
            : base(id, manufacturer, model, price, overallPerformance, connectionType)
        {
        }
    }
}
