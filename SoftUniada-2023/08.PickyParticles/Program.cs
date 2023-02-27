using System;
using System.Linq;
using System.Collections.Generic;

int n = int.Parse(Console.ReadLine());

Proton[] protons = new Proton[n];
Electron[] electrons = new Electron[n];

for(int i = 0; i < n; i++)
{
    List<int> affinity = Console.ReadLine()
        .Split(" ", StringSplitOptions.RemoveEmptyEntries)
        .Select(int.Parse)
        .ToList();
    protons[i] = new Proton(affinity);
}

for (int i = 0; i < n; i ++)
{
    List<int> affinity = Console.ReadLine()
        .Split(" ", StringSplitOptions.RemoveEmptyEntries)
        .Select(int.Parse)
        .ToList();
    electrons[i] = new Electron(affinity);
}

while(protons.Any(p=>p.MergedWith == null))
{
    Proton proton = protons.FirstOrDefault(p => p.MergedWith == null);
    for (int affiinitySlot = 0; affiinitySlot < proton.Affinity.Count; affiinitySlot++)
    {
        int electronIndex = proton.Affinity[affiinitySlot];
        int protonIndex = Array.IndexOf(protons, proton);
        Electron electron = electrons[electronIndex];

        if (electron.MergedWith == null)
        {
            proton.MergedWith = electron;
            electron.MergedWith = proton;
            break;
        }
        else
        {
            int newProtonPriority = proton.Affinity.IndexOf(electronIndex);
            int oldProtonPriority = electron.MergedWith.Affinity.IndexOf(electronIndex);
            
            int newElectronPriority = electron.Affinity.IndexOf(protonIndex);
            int oldProtonIndex = Array.IndexOf(protons, electron.MergedWith);
            int oldELectronPriority = electron.Affinity.IndexOf(oldProtonIndex);
            
            
            if (newProtonPriority + newElectronPriority < oldELectronPriority + oldProtonPriority)
            {
                electron.MergedWith.MergedWith = null;
                electron.MergedWith = proton;
                proton.MergedWith = electron;
                break;
            }
            else if (newProtonPriority + newElectronPriority == oldELectronPriority + oldProtonPriority)
            {
                

                if(newProtonPriority < oldProtonPriority)
                {
                    electron.MergedWith.MergedWith = null;
                    electron.MergedWith = proton;
                    proton.MergedWith = electron;
                    break;
                }
                
            }
            
        }

    }
}

for(int protonIndex = 0; protonIndex < n; protonIndex++)
{
    Electron electron = protons[protonIndex].MergedWith;
    int electronIndex = Array.IndexOf(electrons, electron);

    Console.WriteLine($"{protonIndex} <-> {electronIndex}");
}

    





class Proton
{
    public Proton(List<int> affinity)
    {
        Affinity = affinity;
    }
    public List<int> Affinity { get; set; }
    public Electron MergedWith { get; set; } 
    public int Strength { get; set; }
}

class Electron
{
    public Electron(List<int> affinity)
    {
        Affinity = affinity;
    }
    public List<int> Affinity { get; set; }
    public Proton MergedWith { get; set; }
    public int Strength { get; set; }
}