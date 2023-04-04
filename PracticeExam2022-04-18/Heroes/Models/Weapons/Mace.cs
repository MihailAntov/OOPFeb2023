﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heroes.Models.Weapons
{
    public class Mace : Weapon
    {
        public Mace(string name, int durability) 
            : base(name, durability)
        {
        }

        public override int DoDamage()
        {
            if(Durability > 0)
            {
            Durability--;
            }

            if (Durability == 0)
            {
                return 0;
            }

            return 25;
        }
    }
}
