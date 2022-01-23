using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace Memory

{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello Player!");
            Console.WriteLine("Welcome to 'Memory' game");
            do
            {
                string level;
                int words, chances, guessed = 0; ;
                Random random = new Random();
                Stopwatch stopwatch = new Stopwatch();
                Console.WriteLine("Choose the difficulty level (write: 'easy' / 'hard' )");
                level = GetLevel();
                if (level == "easy")
                {
                    words = 4;
                    chances = 10;
                }
                else 
                {
                    words = 8;
                    chances = 15;
                }
                int sizeOfmemory = words * 2, guessingTrials = 0, placeToword; ;
                string[] memory = new string[sizeOfmemory]; 
                string[] tabOfwords = File.ReadAllLines("Words.txt");
                string[] covered = new string[sizeOfmemory];
                for (int i = 0; i < covered.Length; ++i)
                {
                    covered[i] = "X";
                }
                for (int i = 0; i < words; i++)
                {
                    string radomWord = tabOfwords[random.Next(0, tabOfwords.Count())];
                    for (int j = 0; j < 2; j++)
                    {
                        do
                        {
                            placeToword = random.Next(0, memory.Length);
                        } while (memory[placeToword] != null);
                        memory[placeToword] = radomWord;
                    }
                }
                stopwatch.Start();
                do
                {
                    Write(level, covered, chances, sizeOfmemory);
                    string coord1, coord2;
                    int guessing1, guessing2;
                    do
                    { 
                        Console.WriteLine("Write cordinates of the first word what you would like to discover (ex. 'A1'): ");
                        coord1 = GetCoordinate(words);
                        guessing1 = CalcCoords(coord1, words);
                        if (covered[guessing1] != "X")
                        {
                            Console.WriteLine("You have guessed this word. Choose anotherone.");
                        }
                    }while (covered[guessing1] != "X");
                    covered[guessing1] = memory[guessing1];
                    Write(level, covered, chances, sizeOfmemory);
                    do
                    { Console.WriteLine("Write coordinates of the secound word what you would like to discover (ex. 'A1'): ");
                        coord2 = GetCoordinate(words);
                        guessing2 = CalcCoords(coord2, words);
                        if (covered[guessing2] != "X")
                        {
                            Console.WriteLine("You have guessed this word. Choose anotherone");
                        }
                    }while (covered[guessing2] != "X");
                    covered[guessing2] = memory[guessing2];
                    Write(level, covered, chances, sizeOfmemory);
                    guessingTrials++;
                    if (memory[guessing1] == memory[guessing2])
                    {
                        Console.WriteLine("YOU GUESED!");
                        Console.WriteLine("Press anything to continue.");
                        Console.ReadKey();
                        guessed++;
                        if (guessed == words)
                        {
                            break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("You didn't gues.");
                        Console.WriteLine("Press anything to continue.");
                        Console.ReadKey();
                        chances--;
                        covered[guessing1] = "X";
                        covered[guessing2] = "X";
                        if (chances == 0)
                        {
                            break;
                        }
                    }
                } while (true);
                Console.Clear();
                if (chances == 0)
                {
                    Console.WriteLine("Unfortunately you lose.");
                }
                else
                {
                    Console.WriteLine("Congratulations, you won the game!");
                    Console.WriteLine($"You solved the memory game after {guessingTrials} tries. It took you {stopwatch.Elapsed.Seconds} seconds ");
                    AddHighScore(stopwatch.Elapsed.Seconds, guessingTrials);
                }
                Console.WriteLine("Would you like to play again ? (write: 'yes' / 'no')");
                if (GetPlayAgain() == "no")
                {
                    break;
                }
            } while (true);
        }
        static void Write(string level, string[] covered, int chances, int sizeOfmemory)
        {
            Console.Clear();
            Console.WriteLine($"Level: { level}");
            Console.WriteLine();
            Console.WriteLine($"Chances: { chances}");
            Console.WriteLine();
            Console.Write("   ");
            for (int i = 1; i <= sizeOfmemory/2; ++i)
            {
                Console.Write($" | {i}");
            }
            Console.WriteLine();
            Console.Write("A ");
            for (int i = 0; i < sizeOfmemory; i++)
            {
                Console.Write($"{covered[i]} ");
                if (i == sizeOfmemory/2 - 1)
                {
                    Console.WriteLine();
                    Console.Write("B ");
                }
            }
            Console.WriteLine();
            Console.WriteLine();
        }
        static int CalcCoords(string cord, int words)
        {
            int calc;
            int zeroInASCII = 48;
            if (cord[0] == 'B')
            {
                calc = cord[1] + words - 1 - zeroInASCII;
            }
            else 
            {
                calc = cord[1] - 1 - zeroInASCII;
            }
            return calc;
        }
        static string GetCoordinate(int words)
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
            }while (true);
            return result;
        }
        static string GetLevel()
        {
            string level;
            do
            {
                level = Console.ReadLine();
                level = level.ToLower();
                if (level == null || level != "easy" &&  level != "hard")
                {
                    Console.WriteLine("You have to write 'easy' or 'hard'.");
                    continue;
                }
                break;
            } while (true);
            return level;
        }
        static string GetPlayAgain()
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
        static void AddHighScore(int stopwatch, int guessingTrials)
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
