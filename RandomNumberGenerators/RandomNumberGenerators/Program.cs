using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomRandomNumberGenerators;

namespace RandomNumberGenerators
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //parametri za minimalnu i maksimalnu vrednsost
            int DEFAULT_MIN = 0;
            int DEFAULT_MAX = 10;

            //parametri za middle square weyl sequence
            ulong x = 0;
            ulong w = 0;
            ulong[] s = new ulong[25000];
            List<string> numbers = File.ReadAllLines("Seed.txt").ToList();
            int i = 0;

            foreach(var item in numbers)
            {
                s[i] = Convert.ToUInt64(item.Split(',')[0], 16);
                i++;
            }

            LinearCongruentialGenerator lcg = new LinearCongruentialGenerator();
            MersenneTwister mt = new MersenneTwister((uint) Guid.NewGuid().GetHashCode());
            MiddleSquareWeylSequence msws = new MiddleSquareWeylSequence();
            Random rng = new Random();

            int randomNumberLCG = Guid.NewGuid().GetHashCode();
            int randomNumberMT = 0;
            uint randomNumberMSWS = 0;

            ConsoleKeyInfo keyInfo;

            do
            {
                Console.WriteLine("\nOdaberite jedan od algiritama za generisanje nasumicnog broja:");
                Console.WriteLine("1.Linear Congruental Generator");
                Console.WriteLine("2.Mersenne-Twister");
                Console.WriteLine("3.Middle Square Weyl Sequence\n");

                keyInfo = Console.ReadKey();

                //ukoliko pritisnemo 1 generisemo brojeve pomocu lcg-a
                if (keyInfo.Key == ConsoleKey.D1)
                {
                    do
                    {
                        keyInfo = Console.ReadKey();
                        randomNumberLCG = lcg.Next(randomNumberLCG, DEFAULT_MIN, DEFAULT_MAX);
                        string textLCG = $"LCG: {randomNumberLCG}";

                        File.AppendAllText("LCG_Storage.txt", textLCG + '\n');

                        Console.WriteLine(textLCG);
                    }
                    //na glavni meni se izlazi pritiskom f1
                    while (keyInfo.Key != ConsoleKey.F1);
                }
                //ukoliko pritisnemo 2 generisemo brojeve pomocu mersenne-twistera
                else if (keyInfo.Key == ConsoleKey.D2)
                {
                    do
                    {
                        keyInfo = Console.ReadKey();
                        randomNumberMT = mt.Next(DEFAULT_MIN, DEFAULT_MAX);
                        string textMT = $"Mersenne-Twister: {randomNumberMT}";

                        File.AppendAllText("MT_Storage.txt", textMT + '\n');

                        Console.WriteLine(textMT);
                    }
                    //na glavni meni se izlazi pritiskom f2
                    while (keyInfo.Key != ConsoleKey.F2);
                }
                //ukoliko pritisnemo 3 generisemo brojeve pomocu middle square weyl sequence
                else if (keyInfo.Key == ConsoleKey.D3)
                {
                    do
                    {
                        keyInfo = Console.ReadKey();
                        randomNumberMSWS = (uint)(msws.Next(x, w, s[rng.Next(0, 25000)]) % (DEFAULT_MAX - DEFAULT_MIN) + DEFAULT_MIN);
                        string textMSWS = $"Middle Square Weyl Sequence: {randomNumberMSWS}";

                        File.AppendAllText("MSWS_Storage.txt", textMSWS + '\n');

                        Console.WriteLine(textMSWS);
                    }
                    //na glavni meni se izlazi pritiskom f3
                    while (keyInfo.Key != ConsoleKey.F3);
                }
                else
                    Console.WriteLine("\nNepostojeca opcija!");
            }
            //iz programa se izlazi pritiskom esc tastera
            while (keyInfo.Key != ConsoleKey.Escape);
        }
    }
}