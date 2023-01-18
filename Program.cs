using System;
using System.Collections.Generic;
using System.Threading;

namespace Jaywapp.Algorithm.KMP
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Write($"Type [PrefixFunction(P) or MatchString(M)] : ");

                var input = Console.ReadLine();

                if (input == "P")
                {
                    RunPrefixFunction();
                }
                else if (input == "M")
                {
                    RunStringMatch();
                }
            }
        }

        private static bool RunPrefixFunction()
        {
            Console.Write("pattern : ");
            var pattern = Console.ReadLine();
            if (pattern == "EOF")
                return false;

            var array = PrefixAnalysis.Analyze(pattern, out List<string> traces);
            var chars = pattern.ToCharArray();

            Console.WriteLine(string.Join(" | ", chars));
            Console.WriteLine(string.Join(" | ", array));


            Console.Write("Do you want to see traces? (Y/N) : ");
            var answer = Console.ReadLine();

            if (answer == "Y")
                DisplayPrefixTraces(traces);

            return true;
        }

        private static void DisplayPrefixTraces(List<string> traces)
        {
            Console.Write("Input delay time : ");

            var answer = Console.ReadLine();
            var time = int.TryParse(answer, out int t) ? t : 2;

            foreach (var trace in traces)
            {
                Console.Clear();

                Console.WriteLine(trace);
                Thread.Sleep(time * 1000);
            }

            Console.Clear();
        }

        private static bool RunStringMatch()
        {
            Console.Write("text : ");
            var text = Console.ReadLine();
            if (text == "EOF")
                return false;
            Console.Write("pattern : ");
            var pattern = Console.ReadLine();
            if (pattern == "EOF")
                return false;

            if (!TryRun(BruthForce.Match, text, pattern, out int bruthForthIndex, out TimeSpan bruthForthPerformance)
                    || !TryRun(KnuthMorrisPratt.Match, text, pattern, out int kmpIndex, out TimeSpan kmpPerformance))
                return false;

            Report(bruthForthIndex, bruthForthPerformance, kmpIndex, kmpPerformance);
            return true;
        }

        private static void Report(
            int bruthForthIndex, TimeSpan bruthForthPerformance,
            int kmpIndex, TimeSpan kmpPerformance)
        {
            Console.WriteLine("---------------------------------");

            Console.WriteLine("* Bruth Forth");
            Console.WriteLine($"Index : {bruthForthIndex} / Performance : {bruthForthPerformance}");

            Console.WriteLine("* KMP");
            Console.WriteLine($"Index : {kmpIndex} / Performance : {kmpPerformance}");

            Console.WriteLine("---------------------------------");
        }

        private static bool TryRun(Func<string, string, int> func, string text, string pattern, out int index, out TimeSpan performance)
        {
            index = -1;
            performance = default;

            try
            {
                var start = DateTime.Now;
                var result = func(text, pattern);
                var end = DateTime.Now;

                index = result;
                performance = end - start;

                return true;
            }
            catch
            {

                return false;
            }
        }
    }
}
