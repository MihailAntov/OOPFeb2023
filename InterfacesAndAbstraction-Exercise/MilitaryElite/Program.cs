using System;
using System.Collections.Generic;
using System.Linq;
using MilitaryElite.Models;

namespace MilitaryElite;

internal class Program
{
    static void Main(string[] args)
    {
        string input;
        List<ISoldier> soldiers = new List<ISoldier>();

        while ((input = Console.ReadLine()) != "End")
        {
            string[] inputArgs = input.Split(" ", StringSplitOptions.RemoveEmptyEntries);
            string soldierType = inputArgs[0];

            string[] soldierArgs = inputArgs[1..];
            int id = int.Parse(soldierArgs[0]);
            string firstName = soldierArgs[1];
            string lastName = soldierArgs[2];

            if (soldierType == "Spy")
            {
                int codeNumber = int.Parse(soldierArgs[3]);
                Spy spy = new Spy(id, firstName, lastName, codeNumber);
                soldiers.Add(spy);
            }
            else
            {
                decimal salary = decimal.Parse(soldierArgs[3]);

                if (soldierType == "Private")
                {
                    Private soldier = new Private(id, firstName, lastName, salary);
                    soldiers.Add(soldier);
                }
                else if (soldierType == "LieutenantGeneral")
                {
                    List<Private> privates = new List<Private>();

                    for (int i = 4; i < soldierArgs.Length; i++)
                    {
                        int currentId = int.Parse(soldierArgs[i]);
                        Private thisPrivate = soldiers.Single(s => s.Id == currentId) as Private;
                        privates.Add(thisPrivate);
                    }

                    LieutenantGeneral general = new LieutenantGeneral(id, firstName, lastName, salary, privates.ToArray());
                    soldiers.Add(general);
                }
                else if (soldierType == "Engineer")
                {
                    string corps = soldierArgs[4];
                    List<Repair> repairs = new List<Repair>();

                    for (int i = 5; i < soldierArgs.Length; i += 2)
                    {

                        string partName = soldierArgs[i];
                        int hoursWorked = int.Parse(soldierArgs[i + 1]);
                        try
                        {
                            Repair repair = new Repair(partName, hoursWorked);
                            repairs.Add(repair);
                        }
                        catch 
                        {
                            continue;
                        }
                    }

                    try
                    {
                        Engineer engineer = new Engineer(id, firstName, lastName, salary, corps, repairs.ToArray());
                        soldiers.Add(engineer);
                    }
                    catch 
                    {
                        continue;
                    }

                    
                }
                else if (soldierType == "Commando")
                {
                    string corps = soldierArgs[4];
                    List<Mission> missions = new List<Mission>();

                    for (int i = 5; i < soldierArgs.Length; i += 2)
                    {

                        string codeName = soldierArgs[i];
                        string state = soldierArgs[i + 1];
                        try
                        {
                            Mission mission = new Mission(codeName, state);
                            missions.Add(mission);
                        }
                        catch 
                        {
                            continue;
                        }
                    }

                    try
                    {
                        Commando commando = new Commando(id, firstName, lastName, salary, corps, missions.ToArray());
                        soldiers.Add(commando);
                    }
                    catch 
                    {
                        continue;
                    }
                }
            }
        }

        foreach(ISoldier soldier in soldiers)
        {
            Console.WriteLine(soldier);
        }
    }
}
