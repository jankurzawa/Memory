using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memory
{
    public class ValuesInputControl
    {
        public string GetLevel()
        {
            string level;
            do
            {
                level = Console.ReadLine();
                level = level.ToLower();
                if (level == null || level != "easy" && level != "hard")
                {
                    Console.WriteLine("You have to write 'easy' or 'hard'.");
                    continue;
                }
                break;
            } while (true);
            return level;
        }
        public string GetPlayAgain()
        {
            string answer;
            do
            {
                answer = Console.ReadLine();
                answer = answer.ToLower();
                if (answer == "yes" || answer == "no")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("You have to write 'yes' or 'no'");
                    continue;
                }
            } while (true);
            return answer;
        }
        


    }
}
