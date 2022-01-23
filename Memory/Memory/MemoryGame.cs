using System.Diagnostics;

namespace Memory
{
    public class MemoryGame
    {
        private readonly GameResults _gameResults;
        private readonly Coordinates _coordinates;
        private readonly ValuesInputControl _valuesInputcontrol;
        public MemoryGame(GameResults gameResults, Coordinates coordinates, ValuesInputControl valuesInputcontrol)
        {
            _gameResults = gameResults;
            _coordinates = coordinates;
            _valuesInputcontrol = valuesInputcontrol;
        }
        public void StartGame()
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
                level = _valuesInputcontrol.GetLevel();
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
                FillCoveredWords(covered);
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
                    displayGame(level, covered, chances, sizeOfmemory);
                    string firstCoordinate, secoundCoordinate;
                    int firstWord, secoundWord;
                    do
                    {
                        Console.WriteLine("Write cordinates of the first word what you would like to discover (ex. 'A1'): ");
                        firstCoordinate = _coordinates.GetCoordinates(words);
                        firstWord = _coordinates.CalculateCoordinates(firstCoordinate, words);
                    } while (_coordinates.WrongCoordinates(covered[firstWord]));
                    covered[firstWord] = memory[firstWord];
                    displayGame(level, covered, chances, sizeOfmemory);
                    do
                    {
                        Console.WriteLine("Write coordinates of the secound word what you would like to discover (ex. 'A1'): ");
                        secoundCoordinate = _coordinates.GetCoordinates(words);
                        secoundWord = _coordinates.CalculateCoordinates(secoundCoordinate, words);
                    } while (_coordinates.WrongCoordinates(covered[secoundWord]));
                    covered[secoundWord] = memory[secoundWord];
                    displayGame(level, covered, chances, sizeOfmemory);
                    guessingTrials++;
                    if (memory[firstWord] == memory[secoundWord])
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
                        covered[firstWord] = "X";
                        covered[secoundWord] = "X";
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
                    _gameResults.AddHighScore(stopwatch.Elapsed.Seconds, guessingTrials);
                }
                Console.WriteLine("Would you like to play again ? (write: 'yes' / 'no')");
                if (_valuesInputcontrol.GetPlayAgain() == "no")
                {
                    break;
                }
            } while (true);
        }
        static void displayGame(string level, string[] covered, int chances, int sizeOfmemory)
        {
            Console.Clear();
            Console.WriteLine($"Level: { level}");
            Console.WriteLine();
            Console.WriteLine($"Chances: { chances}");
            Console.WriteLine();
            Console.Write("   ");
            for (int i = 1; i <= sizeOfmemory / 2; ++i)
            {
                Console.Write($" | {i}");
            }
            Console.WriteLine();
            Console.Write("A ");
            for (int i = 0; i < sizeOfmemory; i++)
            {
                Console.Write($"{covered[i]} ");
                if (i == sizeOfmemory / 2 - 1)
                {
                    Console.WriteLine();
                    Console.Write("B ");
                }
            }
            Console.WriteLine();
            Console.WriteLine();
        }
        static void FillCoveredWords(string[] covered)
        {
            for (int i = 0; i < covered.Length; ++i)
            {
                covered[i] = "X";
            }
        }
    }
}
