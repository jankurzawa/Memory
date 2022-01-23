using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memory
{
    public class Coordinates
    {
        public string GetCoordinates(int words)
        {

            string result;
            int zeroInASCII = 48;
            do
            {
                result = Console.ReadLine();
                if (result == null || result.Length != 2)
                {
                    Console.WriteLine("Coordinates should be written in two chars.");
                    continue;
                }
                result = result.ToUpper();
                if (result[0] != 'A' && result[0] != 'B')
                {
                    Console.WriteLine($"You cannot use  char '{result[0]}'.");
                    continue;
                }
                if (result[1] < '1' || result[1] > Convert.ToChar(words + zeroInASCII))
                {
                    Console.WriteLine($"You should use value between 1 and {words}.");
                    continue;
                }
                break;
            } while (true);
            return result;     
        }
        public int CalculateCoordinates(string coord, int words)
        {
            int calc;
            int zeroInASCII = 48;
            if (coord[0] == 'B')
            {
                calc = coord[1] + words - 1 - zeroInASCII;
            }
            else
            {
                calc = coord[1] - 1 - zeroInASCII;
            }
            return calc;
        }
        public bool WrongCoordinates(string guessingWord)
        {
            if (guessingWord != "X")
            {
                Console.WriteLine("You have guessed this word. Choose anotherone");
                return true;
            }
            return false;
        }
    }
}
