//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Net.Http.Headers;
//using System.Reflection;
//using System.Security.Policy;
//using System.Text;
//using System.Threading.Tasks;
//using System.Xml.Linq;

//namespace NHLStats
//{
//    public class Stats
//    {
//        public string Name;                                                  // Name
//        public string Team;                                                 // Team
//        public char Position;                                              // Pos
//        public int GamesPlayed;                                           // GP
//        public int Goals;                                                // G
//        public int Assists;                                             // A
//        public int Points;                                             // P
//        public int PlusMinus;                                         // +/-
//        public int PenaltyInfractionMinutes;                         // PIM
//        public double PowerPlayGoalsPerGame;                        // P/GP
//        public int PowerPlayGoals;                                 // PPG
//        public int InPPP;                                         // PPP
//        public int ShorthandedGoals;                             // SHG 
//        public int ShorthandedPoints;                           // SHP
//        public int GameWinningGoals;                           // GWG
//        public int OvertimeGoals;                             // OTG
//        public int ShotsOnGoal;                              // S
//        public double ShotsPercentage;                      // S%
//        public string TimeOnIcePerGame;                    // TOI/GP
//        public double ShiftsPerGame;                      // Shifts/GP
//        public double FaceOffWonPercentage;              // FOW%
//        private string NextValue(string line, ref int index)
//        {
//            string result = "";
//            if (index < line.Length)
//            {
//                if (line[index] == ',')
//                {
//                    index++;
//                }
//                else if (line[index] == '"')
//                {
//                    int endIndex = line.IndexOf('"', index + 1);
//                    result = line.Substring(index + 1, endIndex - (index + 1));
//                    index = endIndex + 2;
//                }
//                else
//                {
//                    int endIndex = line.IndexOf(',', index);
//                    if (endIndex == -1) result = line.Substring(index);
//                    else result = line.Substring(index, endIndex - index);
//                    index = endIndex + 1;
//                }
//            }
//            return result;
//        }

//        public Stats(string line)
//        {
//            int index = 0;
//            Name = NextValue(line, ref index);
//            Team = NextValue(line, ref index);
//            char.TryParse(NextValue(line, ref index), out Position);
//            int.TryParse(NextValue(line, ref index), out GamesPlayed);
//            int.TryParse(NextValue(line, ref index), out Goals);
//            int.TryParse(NextValue(line, ref index), out Assists);
//            int.TryParse(NextValue(line, ref index), out Points);
//            int.TryParse(NextValue(line, ref index), out PlusMinus);
//            int.TryParse(NextValue(line, ref index), out PenaltyInfractionMinutes);
//            double.TryParse(NextValue(line, ref index), out PowerPlayGoalsPerGame);
//            int.TryParse(NextValue(line, ref index), out PowerPlayGoals);
//            int.TryParse(NextValue(line, ref index), out InPPP);
//            int.TryParse(NextValue(line, ref index), out ShorthandedGoals);
//            int.TryParse(NextValue(line, ref index), out ShorthandedPoints);
//            int.TryParse(NextValue(line, ref index), out GameWinningGoals);
//            int.TryParse(NextValue(line, ref index), out OvertimeGoals);
//            int.TryParse(NextValue(line, ref index), out ShotsOnGoal);
//            double.TryParse(NextValue(line, ref index), out ShotsPercentage);
//            TimeOnIcePerGame = NextValue(line, ref index);
//            double.TryParse(NextValue(line, ref index), out ShiftsPerGame);
//            double.TryParse(NextValue(line, ref index), out FaceOffWonPercentage);
//        }
//    }
//    internal class Program
//    {
//        static List<Stats> Stats = new List<Stats>();
//        static void BuildDBFromFile()
//        {
//            using (var reader = File.OpenText("NHL Player Stats 2017-18.csv"))
//            {
//                string input = reader.ReadLine();
//                while ((input = reader.ReadLine()) != null)
//                {
//                    Stats stats = new Stats(input);
//                    Stats.Add(stats);
//                }
//            }
//        }
//        static void Main(string[] args)
//        {
//            Console.WriteLine("Populating database with players stats... \n");
//            BuildDBFromFile();
//            Console.WriteLine("Ready! \n");
//            {
//                //Console.WriteLine("Query #1 - 20 Best Selling Games in Japan");
//                //var result = Games.OrderByDescending(Game => Game.JPSales).Take(20);
//                //foreach (var game in result)
//                //{
//                //    Console.WriteLine(game.Name + ", sold " + game.JPSales + " copies");
//                //}
//            }
//        }
//    }
//}

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
using System.Linq.Dynamic.Core; // LINQ

namespace NHLStats
{
    public class Stats // class which includes all data and positions of file NHL Players
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


        //This method reads and extracts the next value from a line of text (which contains data separated by commas) based on a position index.
        private string NextValue(string line, ref int index) // index is the position of the line
        {
            string result = "";
            if (index < line.Length) // if is the same row
            {
                if (line[index] == ',') //check if the value of current position is a comma, which means that after this position there is another value to be read
                {
                    index++; // go to next position to get new value
                }
                else if (line[index] == '"')
                {
                    int endIndex = line.IndexOf('"', index + 1);
                    result = line.Substring(index + 1, endIndex - (index + 1));
                    index = endIndex + 2;
                }
                else
                {   // endIndex - stores index of the next comma from current index
                    int endIndex = line.IndexOf(',', index); // try to find the next comma after current index

                    // no more commas in the same row
                    if (endIndex == -1) // "-1": no more commas from position, which means the last value of row and there is no commas to separate other values
                        result = line.Substring(index); // get the last value of row (-1)

                    // if a comma was found, there are more values after current index
                    else //endIndex != -1
                        result = line.Substring(index, endIndex - index); // get a previous value after finding "'"

                    index = endIndex + 1;
                }
            }
            return result;
        }

        public Stats(string line) // constructor method
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
    } // end of class Stats

    internal class Program
    {
        static List<Stats> Stats = new List<Stats>(); // list of class "Stats" to include all rows of file
        static void BuildDBFromFile()
        {
            using (var reader = File.OpenText("NHL Player Stats 2017-18.csv")) // search file from directory "bin/debug"
            {
                string input = reader.ReadLine();
                while ((input = reader.ReadLine()) != null)
                {
                    Stats newStats = new Stats(input);
                    Stats.Add(newStats);
                }
            }
        }

        static void Main(string[] args)
        {
            BuildDBFromFile(); // Giovanni

            while (true)
            {
                Console.WriteLine("\nWelcome to NHL Players !!! ");
                Console.WriteLine();
                Console.WriteLine("Choose an option to print:");
                Console.WriteLine("0 -  Print all data file");
                Console.WriteLine("1 -  Filter by Name");
                Console.WriteLine("2 -  Filter by Team");
                Console.WriteLine("3 -  Filter by Position");
                Console.WriteLine("4 -  Filter by Games Played (GP)");
                Console.WriteLine("5 -  Filter by Goals (G)");
                Console.WriteLine("6 -  Filter by Assists (A)");
                Console.WriteLine("7 -  Filter by Points (P)");
                Console.WriteLine("8 -  Filter by PlusMinus (+/-)");
                Console.WriteLine("9 -  Filter by Penalty Infraction Minutes (PIM)");
                Console.WriteLine("10 - Filter by Power Play Goal Per Game (P/GP)");
                Console.WriteLine("11 - Filter by Power Play Goals (PPG)");
                Console.WriteLine("12 - Filter by InPPP (PPP)");
                Console.WriteLine("13 - Filter by Power Short handed Goals (SHG)");
                Console.WriteLine("14 - Filter by Short handed Points (SHP)");
                Console.WriteLine("15 - Filter by Game Winning Goals (GWG)");
                Console.WriteLine("16 - Filter by Overtime Goals (OTG)");
                Console.WriteLine("17 - Filter by Shots On Goal (S)");
                Console.WriteLine("18 - Filter by Shots Percentage (S%)");
                Console.WriteLine("19 - Filter by Time On Ice Per Game (TOI/GP)");
                Console.WriteLine("19 - Filter by Shifts Per Game (Shifts/GP)");
                Console.WriteLine("20 - Filter by Face Off Won Percentage (FOW%)");
                Console.WriteLine("21 - Exit");

                string option = Console.ReadLine();

                switch (option)
                {
                    case "0":
                        Console.WriteLine("Displaying all data file! \n");
                        foreach (var all in Stats.OrderBy(stats => stats.Name))
                        {
                            Console.WriteLine($"Name: {all.Name}, Team: {all.Team}, Position: {all.Position}, Games Played: {all.GamesPlayed}, Goals: {all.Goals}, Assists: {all.Assists}, Points: {all.Points}");
                        }
                        break;

                    case "1":
                        FilterByName(); // Patricia                        
                        break;

                    case "21":
                        Console.WriteLine("Exiting the program.");
                        return;

                    default:
                        Console.WriteLine("Invalid option.");
                        break;
                } // end of switch
            } // end of while
        } // end of Main


        /////////////////////////////////////////////  METHODS /////////////////////////////////////////////

        static void FilterByName()
        {
            Console.WriteLine("\nFiltering players by Name ...\n");
            Console.WriteLine("\nFilter by:");
            Console.WriteLine("0 - Initial(s) of name");
            Console.WriteLine("1 - Any part of name");
            Console.WriteLine("2 - Range of names");
            Console.WriteLine("3 - Full list of names");
            Console.WriteLine("4 - Dynamic Searching"); // works similarly to option 2(Range of names, but here is a dynamic searching)

            string filterOption = Console.ReadLine();

            switch (filterOption)
            {
                case "0": // Initial(s) letter of name
                    Console.WriteLine("\nEnter with initial(s) of name: ");
                    string initialLetter = Console.ReadLine().ToLower();
                    var result0 = (from stats in Stats
                                   where stats.Name.ToLower().StartsWith(initialLetter)
                                   select stats)
                                   .OrderBy(stats => stats.Name)
                                   .ToList();
                    if (result0.Count > 0)
                    {
                        foreach (var player in result0)
                        {
                            Console.WriteLine($"Name: {player.Name}, Team: {player.Team}, Position: {player.Position}, Games Played: {player.GamesPlayed}, Goals: {player.Goals}, Assists: {player.Assists}, Points: {player.Points}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("No players found with the specified initial letter.");
                    }
                    break;

                case "1": // Any part of name 
                    Console.WriteLine("\nEnter with any part of name: ");
                    string subString = Console.ReadLine().ToLower();
                    var result1 = (from stats in Stats
                                   where stats.Name.ToLower().Contains(subString)
                                   select stats)
                                   .OrderBy(stats => stats.Name)
                                   .ToList();
                    if (result1.Count > 0)
                    {
                        foreach (var player in result1)
                        {
                            Console.WriteLine($"Name: {player.Name}, Team: {player.Team}, Position: {player.Position}, Games Played: {player.GamesPlayed}, Goals: {player.Goals}, Assists: {player.Assists}, Points: {player.Points}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("No players found with the specified name.");
                    }
                    break;

                case "2": // Range of names
                    Console.WriteLine("\nEnter the first name of range (inclusive): ");
                    string startRange = Console.ReadLine().ToLower().Trim();

                    Console.WriteLine("\nEnter the last name of range (inclusive): ");
                    string endRange = Console.ReadLine().ToLower().Trim();

                    var startExists = Stats.Where(stats => stats.Name.ToLower().Trim().Equals(startRange)).ToList();
                    var endExists = Stats.Where(stats => stats.Name.ToLower().Trim().Equals(endRange)).ToList();

                    if (!startExists.Any() && !endExists.Any())
                    {
                        Console.WriteLine($"Error: Both players with the names '{startRange}' and {endRange}' do not exist.");
                    }
                    else if (!startExists.Any())
                    {
                        Console.WriteLine($"Error: The player '{startRange}' does not exist.");
                    }
                    else if (!endExists.Any())
                    {
                        Console.WriteLine($"Error: The player '{endRange}' does not exist.");
                    }
                    else
                    {
                        var result2 = (from stats in Stats
                                       where string.Compare(stats.Name.ToLower(), startRange) >= 0 && string.Compare(stats.Name.ToLower(), endRange) <= 0
                                       select stats)
                                       .OrderBy(stats => stats.Name)
                                       .ToList();
                        if (result2.Count > 0)
                        {
                            foreach (var player in result2)
                            {
                                Console.WriteLine($"Name: {player.Name}, Team: {player.Team}, Position: {player.Position}, Games Played: {player.GamesPlayed}, Goals: {player.Goals}, Assists: {player.Assists}, Points: {player.Points}");
                            }
                        }
                        else
                        {
                            Console.WriteLine("No players found within the specified name range.");
                        }
                    }
                    break;

                case "3": // Full list of names
                    Console.WriteLine("\nFull list of names ordered: ");
                    var result3 = (from stats in Stats
                                   select stats)
                                   .OrderBy(stats => stats.Name)
                                   .ToList();
                    if (result3.Count > 0)
                    {
                        foreach (var player in result3)
                        {
                            Console.WriteLine($"Name: {player.Name}, Team: {player.Team}, Position: {player.Position}, Games Played: {player.GamesPlayed}, Goals: {player.Goals}, Assists: {player.Assists}, Points: {player.Points}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("No name in this file to display!");
                    }
                    break;

                case "4":
                    //   Console.WriteLine("\nEnter the field to filter by (e.g., Name, Team, GamesPlayed, Goals, etc.): ");
                    //   string field = Console.ReadLine().Trim();
                    string field = "Name";
                    //field = field.ToLower();

                    Console.WriteLine($"\nEnter the first value of the range for {field} (inclusive): ");
                    string startRange1 = Console.ReadLine().Trim();

                    Console.WriteLine($"\nEnter the last value of the range for {field} (inclusive): ");
                    string endRange1 = Console.ReadLine().Trim();

                    // Dynamic expression 
                    // string dynamicExpression = $"{field} >= @0 && {field} <= @1";
                    string dynamicExpression = $"{field}.ToLower().StartsWith(@0.ToLower()) && {field}.ToLower().CompareTo(@1.ToLower()) <= 0";


                    var result4 = Stats.AsQueryable()
                                      .Where(dynamicExpression, startRange1, endRange1)
                                      .OrderBy(field)
                                      .ToList();

                    if (result4.Count > 0)
                    {
                        foreach (var player in result4)
                        {
                            Console.WriteLine($"Name: {player.Name}, Team: {player.Team}, Position: {player.Position}, Games Played: {player.GamesPlayed}, Goals: {player.Goals}, Assists: {player.Assists}, Points: {player.Points}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("No name in this file to display!");
                    }
                    break;

                default:
                    Console.WriteLine("Invalid filter option.");
                    break;
            }
        } // end of method "FilterByName()"
    } // end of class Program
} // end of NHL Stats


