using System.Collections.Generic;

namespace MilitaryElite.Models
{
    public interface IEngineer
    {
        List<Repair> Repairs { get; }
    }
}
