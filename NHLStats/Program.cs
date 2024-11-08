using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Reflection;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace NHLStats
{
    public class Stats
    {
        public string Name;                                                  // Name
        public string Team;                                                 // Team
        public char Position;                                              // Pos
        public int GamesPlayed;                                           // GP
        public int Goals;                                                // G
        public int Assists;                                             // A
        public int Points;                                             // P
        public int PlusMinus;                                         // +/-
        public int PenaltyInfractionMinutes;                         // PIM
        public double PowerPlayGoalsPerGame;                        // P/GP
        public int PowerPlayGoals;                                 // PPG
        public int InPPP;                                         // PPP
        public int ShorthandedGoals;                             // SHG 
        public int ShorthandedPoints;                           // SHP
        public int GameWinningGoals;                           // GWG
        public int OvertimeGoals;                             // OTG
        public int ShotsOnGoal;                              // S
        public double ShotsPercentage;                      // S%
        public string TimeOnIcePerGame;                    // TOI/GP
        public double ShiftsPerGame;                      // Shifts/GP
        public double FaceOffWonPercentage;              // FOW%
        private string NextValue(string line, ref int index)
        {
            string result = "";
            if (index < line.Length)
            {
                if (line[index] == ',')
                {
                    index++;
                }
                else if (line[index] == '"')
                {
                    int endIndex = line.IndexOf('"', index + 1);
                    result = line.Substring(index + 1, endIndex - (index + 1));
                    index = endIndex + 2;
                }
                else
                {
                    int endIndex = line.IndexOf(',', index);
                    if (endIndex == -1) result = line.Substring(index);
                    else result = line.Substring(index, endIndex - index);
                    index = endIndex + 1;
                }
            }
            return result;
        }

        public Stats(string line)
        {
            int index = 0;
            Name = NextValue(line, ref index);
            Team = NextValue(line, ref index);
            char.TryParse(NextValue(line, ref index), out Position);
            int.TryParse(NextValue(line, ref index), out GamesPlayed);
            int.TryParse(NextValue(line, ref index), out Goals);
            int.TryParse(NextValue(line, ref index), out Assists);
            int.TryParse(NextValue(line, ref index), out Points);
            int.TryParse(NextValue(line, ref index), out PlusMinus);
            int.TryParse(NextValue(line, ref index), out PenaltyInfractionMinutes);
            double.TryParse(NextValue(line, ref index), out PowerPlayGoalsPerGame);
            int.TryParse(NextValue(line, ref index), out PowerPlayGoals);
            int.TryParse(NextValue(line, ref index), out InPPP);
            int.TryParse(NextValue(line, ref index), out ShorthandedGoals);
            int.TryParse(NextValue(line, ref index), out ShorthandedPoints);
            int.TryParse(NextValue(line, ref index), out GameWinningGoals);
            int.TryParse(NextValue(line, ref index), out OvertimeGoals);
            int.TryParse(NextValue(line, ref index), out ShotsOnGoal);
            double.TryParse(NextValue(line, ref index), out ShotsPercentage);
            TimeOnIcePerGame = NextValue(line, ref index);
            double.TryParse(NextValue(line, ref index), out ShiftsPerGame);
            double.TryParse(NextValue(line, ref index), out FaceOffWonPercentage);
        }
    }
    internal class Program
    {
        static List<Stats> Stats = new List<Stats>();
        static void BuildDBFromFile()
        {
            using (var reader = File.OpenText("NHL Player Stats 2017-18.csv"))
            {
                string input = reader.ReadLine();
                while ((input = reader.ReadLine()) != null)
                {
                    Stats stats = new Stats(input);
                    Stats.Add(stats);
                }
            }
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Populating database with players stats... \n");
            BuildDBFromFile();
            Console.WriteLine("Ready! \n");
            {
                //Console.WriteLine("Query #1 - 20 Best Selling Games in Japan");
                //var result = Games.OrderByDescending(Game => Game.JPSales).Take(20);
                //foreach (var game in result)
                //{
                //    Console.WriteLine(game.Name + ", sold " + game.JPSales + " copies");
                //}
            }
        }
    }
}
