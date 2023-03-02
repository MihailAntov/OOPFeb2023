using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilitaryElite.Models
{
    public class Mission : IMission
    {
        private string state;
        public string CodeName { get; set; }
        public Mission(string codeName,string state)
        {
            CodeName = codeName;
            State = state;
        }
        public string State 
        { 
            get { return state; } 
            private set
            {
                if(value != "inProgress" && value != "Finished")
                {
                    throw new ArgumentException("Invalid mission state!");
                }
                state = value;
            }
        }

        public void  CompleteMission()
        {
            this.State = "Finished";
        }

        public override string ToString()
        {
            return $"Code Name: {CodeName} State: {State}";
        }


    }
}
