using System.Collections.Generic;

namespace MilitaryElite.Models
{
    public interface ICommando
    {
        List<Mission> Missions { get; }
    }
}
