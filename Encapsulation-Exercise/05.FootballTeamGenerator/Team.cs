using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _05.FootballTeamGenerator
{
    public class Team
    {
        private List<Player> players;
        private string name;
        public Team(string name)
        {
            Name = name;
            players = new List<Player>();
        }
        public string Name
        {
            get { return name; }
            private set
            {
                if(string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("A name should not be empty.");
                }
                name = value;
            }
        }
        public int Raiting 
        { 
            get
            {
                if(players.Count == 0)
                {
                    return 0;
                }
                return (int)Math.Ceiling(players.Select(p => p.SkillLevel).Average());
            }
        }

        public void AddPlayer (Player player)
        {
            players.Add(player);
        }

        public void RemovePlayer(string name)
        {
            Player playerToRemove = players.FirstOrDefault(p => p.Name == name);
            if(playerToRemove == null)
            {
                throw new InvalidOperationException($"Player {name} is not in {Name} team.");
            }

            players.Remove(playerToRemove);
        }
    }
}
