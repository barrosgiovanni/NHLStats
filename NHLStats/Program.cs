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
        public int ShortHandedGoals;                             // SHG 
        public int ShortHandedPoints;                           // SHP
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
            int.TryParse(NextValue(line, ref index), out ShortHandedGoals);
            int.TryParse(NextValue(line, ref index), out ShortHandedPoints);
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

            while (true) // Patricia
            {
                Console.WriteLine("\nWelcome to NHL Players !!! ");
                Console.WriteLine();
                Console.WriteLine("Choose an option to print:");
                Console.WriteLine("0 -  Print all data file");
                Console.WriteLine("1 -  Filter by Name");
                Console.WriteLine("2 -  Filter by Team");
                Console.WriteLine("3 -  Filter by Position (POS)");
                Console.WriteLine("4 -  Filter by Games Played (GP)");
                Console.WriteLine("5 -  Filter by Goals (G)");
                Console.WriteLine("6 -  Filter by Assists (A)");
                Console.WriteLine("7 -  Filter by Points (P)");
                Console.WriteLine("8 -  Filter by PlusMinus (+/-)");
                Console.WriteLine("9 -  Filter by Penalty Infraction Minutes (PIM)");
                Console.WriteLine("10 - Filter by Power Play Goal Per Game (P/GP)");
                Console.WriteLine("11 - Filter by Power Play Goals (PPG)");
                Console.WriteLine("12 - Filter by InPPP (PPP)");
                Console.WriteLine("13 - Filter by Power Short Handed Goals (SHG)");
                Console.WriteLine("14 - Filter by Short Handed Points (SHP)");
                Console.WriteLine("15 - Filter by Game Winning Goals (GWG)");
                Console.WriteLine("16 - Filter by Overtime Goals (OTG)");
                Console.WriteLine("17 - Filter by Shots On Goal (S)");
                Console.WriteLine("18 - Filter by Shots Percentage (S%)");
                Console.WriteLine("19 - Filter by Time On Ice Per Game (TOI/GP)");
                Console.WriteLine("20 - Filter by Shifts Per Game (Shifts/GP)");
                Console.WriteLine("21 - Filter by Face Off Won Percentage (FOW%)");
                Console.WriteLine("22 - Exit\n");

                Console.WriteLine("/---------------------------------------------------- DIRECTIONS ------------------------------------------------------/");
                Console.WriteLine("   1) You are able to include more than one filter after the first one");
                Console.WriteLine("                 eg: Name >= Justin Abdelkader, Team = DET, GamesPlayed > 20, Goals >= 15 or Assists < 10\n");
                Console.WriteLine("   2) For filters with more than 1 word, join all words");
                Console.WriteLine("                 eg: PenaltyInfractionMinutes > 24");

                Console.WriteLine("   3) Type 'RETURN' to go back to main menu");
                Console.WriteLine("   4) ',' refers to 'and'");
                Console.WriteLine("   5) Quotes are not needed when filtering by name");
                Console.WriteLine("/----------------------------------------------------------------------------------------------------------------------/");

                Console.Write("\nType your filter option: ");
                string option = Console.ReadLine();
                string filters = string.Empty;
                Console.WriteLine();

                switch (option)
                {
                    case "0":
                        Console.Write("Type ASC to sort by ascending or DESC to sort by descending: ");
                        string sort = Console.ReadLine();
                        Console.WriteLine();
                        sort = sort.ToLower();
                        Console.WriteLine("Displaying all data file! \n");
                        PrintValuesSorted(Stats, sort);
                        break;

                    case "1":
                        FilterByName(); // inside this method, the method ProcessFilters() also is called
                        break;

                    case  "2": case  "3": case  "4": case "5": case  "6": case  "7": case  "8": case  "9": case "10": case "11":
                    case "12": case "13": case "14": case "15":case "16": case "17": case "18": case "19": case "20": case "21":
                        ProcessFilters();
                        break;
                    
                    case "22":
                        Console.WriteLine("Exiting the program.");
                        return;

                    default:
                        Console.WriteLine("Invalid option.");
                        break;
                } // end of switch
            } // end of while
        } // end of Main


        /////////////////////////////////////////////  METHODS /////////////////////////////////////////////
        static void PrintHeader() // Patricia
        {
            Console.WriteLine($"{"Name".PadRight(25)}" +
                                $"{"Team".PadRight(8)}" +
                                $"{"Pos".PadRight(6)}" +
                                $"{"GP".PadRight(6)}" +
                                $"{"G".PadRight(6)}" +
                                $"{"A".PadRight(6)}" +
                                $"{"P".PadRight(6)}" +
                                $"{"+/−".PadRight(6)}" +
                                $"{"PIM".PadRight(6)}" +
                                $"{"P/GP".PadRight(8)}" +
                                $"{"PPG".PadRight(6)}" +
                                $"{"PPP".PadRight(6)}" +
                                $"{"SHG".PadRight(6)}" +
                                $"{"SHP".PadRight(6)}" +
                                $"{"GWG".PadRight(6)}" +
                                $"{"OTG".PadRight(6)}" +
                                $"{"S".PadRight(6)}" +
                                $"{"S%".PadRight(6)}" +
                                $"{"TOI/GP".PadRight(8)}" +
                                $"{"Shifts/GP".PadRight(11)}" +
                                $"{"FOW%".PadRight(8)}");
        }

        static void PrintValuesSorted(List<Stats> players, string sort) // Patricia
        {
            PrintHeader();
            var orderedStats = sort == "asc" ? players.OrderBy(stats => stats.Name) : players.OrderByDescending(stats => stats.Name);
            int count = 0;

            foreach (var all in orderedStats)
            {
                string positionText = all.Position == '\0' ? "".PadRight(6) : all.Position.ToString().PadRight(6); // not always the field "P" is filled out

                Console.WriteLine($"{all.Name.Trim().PadRight(25)}" +
                                    $"{all.Team.Trim().PadRight(8)}" +
                                    $"{positionText}" +
                                    $"{all.GamesPlayed.ToString().PadRight(6)}" +
                                    $"{all.Goals.ToString().PadRight(6)}" +
                                    $"{all.Assists.ToString().PadRight(6)}" +
                                    $"{all.Points.ToString().PadRight(6)}" +
                                    $"{all.PlusMinus.ToString().PadRight(6)}" +
                                    $"{all.PenaltyInfractionMinutes.ToString().PadRight(6)}" +
                                    $"{all.PowerPlayGoalsPerGame.ToString("0.0").PadRight(8)}" +
                                    $"{all.PowerPlayGoals.ToString().PadRight(6)}" +
                                    $"{all.InPPP.ToString().PadRight(6)}" +
                                    $"{all.ShortHandedGoals.ToString().PadRight(6)}" +
                                    $"{all.ShortHandedPoints.ToString().PadRight(6)}" +
                                    $"{all.GameWinningGoals.ToString().PadRight(6)}" +
                                    $"{all.OvertimeGoals.ToString().PadRight(6)}" +
                                    $"{all.ShotsOnGoal.ToString().PadRight(6)}" +
                                    $"{all.ShotsPercentage.ToString("0.0").PadRight(6)}" +
                                    $"{all.TimeOnIcePerGame.Trim().PadRight(8)}" +
                                    $"{all.ShiftsPerGame.ToString("0.0").PadRight(11)}" +
                                    $"{all.FaceOffWonPercentage.ToString("0.0").PadRight(8)}"
                                );
                count += 1;
            }
            Console.WriteLine($"\nTotal of records: {count}");
        } // end of method PrintValuesSorted(List<Stats> players, string sort)

        static void FilterByName() // Patricia
        {
            Console.WriteLine("\nFilter by:");
            Console.WriteLine("1 - Any part of name");
            Console.WriteLine("2 - Dynamic Searching");
            Console.WriteLine("3 - Return to Main Menu");

            string filterOption = Console.ReadLine();

            switch (filterOption)
            {   
                case "1": // Any part of name 
                    Console.Write("\nEnter with any part of name: ");
                    string subString = Console.ReadLine().ToLower();
                    Console.Write("Type ASC to sort by ascending or DESC to sort by descending: ");
                    string sort = Console.ReadLine().ToLower().Trim();
                    
                    Console.WriteLine();
                  
                    var result1 = (from stats in Stats
                                   where stats.Name.ToLower().Contains(subString)
                                   select stats)
                                   .ToList();
                    
                    if (result1.Count > 0)
                    {
                        PrintValuesSorted(result1, sort);   
                    }
                    else
                    {
                        Console.WriteLine("No players found for this specified name! Try again!");
                    }
                    break;

                case "2": // Dynamic Searching
                    ProcessFilters();
                    break;
                    
                case "3": // Return to Menu
                    return;
               
                default:
                    Console.WriteLine("Invalid option! Try again!");
                    break;
            }
        } // end of method "FilterByName()"


        /* All filters and its values will be processed by this method
         // Query by using LINQ (C#) */
        static void ProcessFilters() // Patricia
        {
            Console.Write("Type filter(s) to search: ");
            string filters = Console.ReadLine();
            if (string.Equals(filters, "return", StringComparison.OrdinalIgnoreCase))
            {
                return;
            }
            string sort1 = string.Empty;

            // Array of operators to check if data should be sorted
            string[] comparisonOperators = { "==", ">=", "<=", ">", "<" };
            foreach (var op in comparisonOperators) // check operators from user
            {
                if (filters.Contains(op))
                {
                    Console.Write("Type ASC to sort by ascending or DESC to sort by descending: ");
                    sort1 = Console.ReadLine().ToLower().Trim();
                    Console.WriteLine();
                    break;
                }
            }

            // Convert user valid screen characters to logical operators
            filters = filters.Replace(",", "&&")
                             .Replace(" and ", " && ")
                             .Replace(" AND ", " && ")
                             .Replace(" or ", " || ")
                             .Replace(" OR ", " || ");

            // Replace '=' to '==' if user operator is "=" and is not part of other operators ">=", "<=", ">", "<"
            if (filters.Contains("="))
                filters = System.Text.RegularExpressions.Regex.Replace(filters, @"(?<![><!])=(?![><=])", "==");


            // Split "filters" in parts based on '||'. If there is no ||, the user filter is added into this variable "orParts"
            var orParts = filters.Split(new[] { "||" }, StringSplitOptions.RemoveEmptyEntries);


            for (int i = 0; i < orParts.Length; i++)
            {
                // Split "filters" in parts based on both '&&' and "orParts". If there is no &&, the user filter is added into this variable "andParts"
                var andParts = orParts[i].Split(new[] { "&&" }, StringSplitOptions.RemoveEmptyEntries);
                for (int j = 0; j < andParts.Length; j++)
                {
                    var part = andParts[j].Trim();
                    foreach (var op in comparisonOperators)
                    {
                        if (part.Contains(op))
                        {
                            int operatorIndex = part.IndexOf(op);
                            string field = part.Substring(0, operatorIndex).Trim();
                            string value = part.Substring(operatorIndex + op.Length).Trim();

                            // Verify if it's a string
                            if (!double.TryParse(value, out _))
                            {
                                // Check if there are quotes. If not, add it into string value. "\"" - this is a inner default format of C#
                                if (!value.StartsWith("\"") && !value.EndsWith("\""))
                                {
                                    value = $"\"{value}\""; //quotes added
                                }

                                if (op == "==")
                                {
                                    andParts[j] = $"{field}.ToLower().Contains({value}.ToLower())";
                                }
                                else // other operators
                                {
                                    andParts[j] = $"string.Compare({field}, {value}) {op} 0";
                                }
                            }
                            else
                            {
                                // other numeric values, keep original format
                                andParts[j] = $"{field} {op} {value}";
                            }
                            break;
                        }
                    }
                }
                // add parentheses to string to keep precedence of &&
                orParts[i] = "(" + string.Join(" && ", andParts) + ")";
            } // end of for

            filters = string.Join(" || ", orParts); // keep this row regardless has or not this operator

            try
            {
               // Run dynamic Query by using LINQ (C#)
                var query = Stats.AsQueryable().Where(filters);
                var filteredPlayers = query.ToList();

                if (filteredPlayers.Any())
                {
                    PrintValuesSorted(filteredPlayers, sort1);
                }
                else
                {
                    Console.WriteLine("No player found for this filter! Try again!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error by applying filters: {ex.Message}");
            }

        } // end of method ProcessFilters()
    } // end of class Program
} //  end of NHL Stats


