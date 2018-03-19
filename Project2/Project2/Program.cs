using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace Project2
{
    class Program
    {
        static void Main(string[] args)
        {
            readInFile();
            
        }// end of main
        public static void readInFile()
        {
            //declarations
            
            List<Superbowl> superbowl = new List<Superbowl>();
            Superbowl aSuperbowl;
            const char DELIMITER = ',';
            string[] arrayOfValues;
            const string FILEPATH = @"C:\Users\perbraa\Desktop\AdvanceProg\Super_Bowl_Project.csv";
            //string FILENAME = @"C:\Users\perbraa\Desktop\AdvanceProg\";

            //name file to write to 
            //Console.WriteLine("Input file path");
            //string username = Console.ReadLine();
            //string FILENAME = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), username + ".txt");

            try
            {
                //read in from csv
                FileStream file = new FileStream(FILEPATH, FileMode.Open, FileAccess.Read);
                StreamReader read = new StreamReader(file);

                //create file to write to 
                //FileStream Writeto = new FileStream(FILENAME, FileMode.Create, FileAccess.Write);
                //StreamWriter write = new StreamWriter(Writeto);



                while (!read.EndOfStream)
                {
                    
                    arrayOfValues = read.ReadLine().Split(DELIMITER); // split info at ',' delimiter
                    aSuperbowl = new Superbowl(arrayOfValues[0],arrayOfValues[1],Convert.ToInt32(arrayOfValues[2]),arrayOfValues[3],arrayOfValues[4],arrayOfValues[5], Convert.ToInt32(arrayOfValues[6]),arrayOfValues[7],arrayOfValues[8], arrayOfValues[9], Convert.ToInt32(arrayOfValues[10]),arrayOfValues[11],arrayOfValues[12],arrayOfValues[13],arrayOfValues[14]);
                    Console.WriteLine(aSuperbowl);
                    superbowl.Add(aSuperbowl);
                    
                }
                //queries
                SuperBowlWinners(superbowl);
                TopAttended(superbowl);
                CityHost(superbowl);
                MVP(superbowl);
                LosingCoach(superbowl);
                read.Close();
                file.Close();
            }catch(Exception i)
            {
                Console.WriteLine(i.Message);
            }
            


        }
        public static void SuperBowlWinners(List<Superbowl> superbowl)
        {
            var winners = 
                from sb in superbowl
                let elements = sb.SB.Split(',')
                select new {sb.date, sb.winner, sb.QBWinner, sb.CoachWinner, sb.MVP };
            Console.WriteLine("List of superbowl Winners");
            foreach (var sb in winners)
            {
                Console.WriteLine($"Date:{sb.date}\nTeam Name: {sb.winner} Winning Quarterback: {sb.QBWinner} Winning Coach: {sb.CoachWinner} MVP: {sb.MVP}");
                
            }
        }

        public static void TopAttended(List<Superbowl> superbowl)
        {
            var topattend =
                (from sb in superbowl
                orderby sb.attendance descending 
                select new { sb.date,sb.attendance, sb.winner, sb.loser, sb.city, sb.state, sb.Stadium }).Take(5);
            
            Console.WriteLine("Top Five Attended Superbowls");
            foreach (var sb in topattend)
            {
                Console.WriteLine($"Date: {sb.date}\nAttendance: {sb.attendance}\nWinning Team: {sb.winner}\nLosing Team: {sb.loser}\nCity{sb.city},\nState{sb.state}\nStadium:{sb.Stadium}");
            }

        }

        public static void CityHost(List<Superbowl> superbowl)
        {
            var cityhost =
                from sb in superbowl
                orderby sb.state ascending
                where sb.state.Count() > 9
                select new {sb.date, sb.city, sb.state, sb.Stadium };
            Console.WriteLine("City With Most Hosted Superbowls");
            foreach (var sb in cityhost)
            {
                Console.WriteLine($"Date:{sb.date}\n{sb.city},{sb.state}\nStadium:{sb.Stadium}");
                
            }

        }


        public static void MVP(List<Superbowl> superbowl)
        {
            var mvp =
                from sb in superbowl
                orderby sb.MVP ascending
                where sb.MVP.Count() > 2
                select new { sb.MVP, sb.winner, sb.loser };
            Console.WriteLine("MVP who won more than Twice");
            foreach (var sb in mvp)
            {
                Console.WriteLine($"Name:{sb.MVP}\nWinning Team: {sb.winner}\nLosing Team:{sb.loser}");
            }
            
        }

        public static void LosingCoach(List<Superbowl> superbowl)
        {
            var maxloses =
                from sb in superbowl
                let cnt = sb.CoachLoser.Count()
                select new { sb.CoachLoser };
            Console.WriteLine("Coach with most lost superbowls");
            foreach(var sb in maxloses)
            {
                Console.WriteLine($"{sb.CoachLoser}");
            }
               
        }

    }//end of readfile
    class Superbowl
    {
        public string date { get; set; }
        public string SB { get; set; }
        public int attendance { get; set; }
        public string QBWinner { get; set; }
        public string CoachWinner { get; set; }
        public string winner { get; set; }
        public int WinningPts { get; set; }
        public string QBLoser { get; set; }
        public string CoachLoser { get; set; }
        public string loser { get; set; }
        public int LosingPts { get; set; }
        public string MVP { get; set; }
        public string Stadium { get; set; }
        public string city { get; set; }
        public string state { get; set; }

        public Superbowl(string date, string sb, int attendance, string QBWinner, string CoachWinner, string winner, int winningPts, string QBLoser, string coachLoser, string loser, int losingPts, string mvp, string stadium, string city, string state)
        {
            this.date = date;
            SB = sb;
            this.attendance = attendance;
            this.QBWinner = QBWinner;
            this.CoachWinner = CoachWinner;
            this.winner = winner;
            WinningPts = WinningPts;
            this.QBLoser = QBLoser;
            this.CoachLoser = coachLoser;
            this.loser = loser;
            LosingPts = losingPts;
            MVP = mvp;
            Stadium = stadium;
            this.city = city;
            this.state = state;

            
        }
        public Superbowl()
        {

        }
        public override string ToString()
        {
            return String.Format($" Date: {date} \nSuperBowl: {SB} \nAttendance {attendance}\nWinning Quarterback {QBWinner}\nWinning Coach: {CoachWinner}\nSuperBowl Winner {winner}\nWinners Points: {WinningPts}\nLosing Quarterback {QBLoser}\nLosing Coach: {CoachLoser}\nLosers Points:{LosingPts}\nMVP {MVP}\nStadium: {Stadium}\nCity: {city}\nState:{state}");
               

        }
    }// end of superbowl class

}
