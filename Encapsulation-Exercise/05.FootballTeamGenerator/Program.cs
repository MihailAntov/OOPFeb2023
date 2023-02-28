using System;
using System.Collections.Generic;
using System.Linq;

namespace _05.FootballTeamGenerator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string input;
            List<Team> teams = new List<Team>();
            while((input = Console.ReadLine())!= "END")
            {
                string[] inputArgs = input.Split(";");
                string command = inputArgs[0];


                try
                {
                    string teamName = inputArgs[1];
                    if (command == "Team")
                    {

                        Team team = new Team(teamName);
                        teams.Add(team);


                    }
                    else if (command == "Add")
                    {

                        Team team = teams.FirstOrDefault(t => t.Name == teamName);
                        if(team == null)
                        {
                            throw new ArgumentException($"Team {teamName} does not exist.");
                        }

                        string playerName = inputArgs[2];
                        int endurance = int.Parse(inputArgs[3]);
                        int sprint = int.Parse(inputArgs[4]);
                        int dribble = int.Parse(inputArgs[5]);
                        int passing = int.Parse(inputArgs[6]);
                        int shooting = int.Parse(inputArgs[7]);

                        Player player = new Player(playerName, endurance, sprint, dribble, passing, shooting);

                        team.AddPlayer(player);

                    }
                    else if (command == "Remove")
                    {
                        Team team = teams.FirstOrDefault(t => t.Name == teamName);
                        if (team == null)
                        {
                            throw new ArgumentException($"Team {teamName} does not exist.");
                        }
                        string playerName = inputArgs[2];
                        team.RemovePlayer(playerName);
                    }
                    else if (command == "Rating")
                    {
                        Team team = teams.FirstOrDefault(t => t.Name == teamName);
                        if (team == null)
                        {
                            throw new ArgumentException($"Team {teamName} does not exist.");
                        }
                        Console.WriteLine($"{teamName} - {team.Raiting}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                
            }
        }
    }
}