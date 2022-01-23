using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memory
{
    public class GameResults
    {
        public void AddHighScore(int stopwatch, int guessingTrials)
        {
            Console.WriteLine("Write your name.");
            string name = Console.ReadLine();
            using (StreamWriter streamWriter = File.AppendText("high_scores.txt"))
            {
                streamWriter.WriteLine(name + " | " + DateTime.Now.ToString() + " | " + stopwatch + " | " + guessingTrials);
            }
        }
    }
}
