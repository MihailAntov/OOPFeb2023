using Easter.Models.Bunnies.Contracts;
using Easter.Models.Dyes.Contracts;
using Easter.Models.Eggs.Contracts;
using Easter.Models.Workshops.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easter.Models.Workshops
{
    public class Workshop : IWorkshop
    {
        public void Color(IEgg egg, IBunny bunny)
        {
            while (!egg.IsDone())
            {
                IDye currentDye = bunny.Dyes
                    .FirstOrDefault(d => !d.IsFinished());

                if(currentDye == null)
                {
                    return;
                }

                if(bunny.Energy == 0)
                {
                    return;
                }
                
                bunny.Work();
                currentDye.Use();
                egg.GetColored();
            }
        }
    }
}
