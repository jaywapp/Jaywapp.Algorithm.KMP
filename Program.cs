using Jaywapp.Algorithm.String;
using Jaywapp.Algorithm.String.Base;
using Jaywapp.Algorithm.String.KMP;
using System;

namespace Jaywapp.Algorithm.KMP
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                try
                {
                    var menu = ReadMenu();

                    if (menu == eMenu.None)
                        continue;

                    if(menu == eMenu.BruteForce || menu == eMenu.KMP)
                    {
                        var text = Read("text");
                        var pattern = Read("pattern");
                        var match = Create(menu, text, pattern);
                        var showTrace = Read("show trace? (Y/N)");

                        Print(match, showTrace == "Y");
                    }
                    else if(menu == eMenu.KMP)
                    {
                        var pattern = Read("pattern");
                        var showTrace = Read("show trace? (Y/N)");

                        var analysis = new PrefixAnalysis(pattern);
                        analysis.Analyze();

                        var array = analysis.Values;
                        var traces = analysis.Traces;
                        var chars = pattern.ToCharArray();

                        Console.WriteLine(string.Join(" | ", chars));
                        Console.WriteLine(string.Join(" | ", array));

                        if(showTrace == "Y")
                        {
                            foreach (var trace in traces)
                                Console.WriteLine(trace);
                        }
                    }
                    else if(menu == eMenu.Compare)
                    {
                        var text = Read("text");
                        var pattern = Read("pattern");

                        if (!TryRun(BruteForce.Match, text, pattern, out int bruteForthIndex, out TimeSpan bruteForthPerformance)
                                || !TryRun(KnuthMorrisPratt.Match, text, pattern, out int kmpIndex, out TimeSpan kmpPerformance))
                            continue;

                        ReportComparing(bruteForthIndex, bruteForthPerformance, kmpIndex, kmpPerformance);
                    }
                }
                catch
                {
                    return;
                }
            }
        }

        private static eMenu ReadMenu()
        {
            var input = Read("Enter menu name");

            if (input == "B" || input == "BruteForce")
                return eMenu.BruteForce;
            else if (input == "K" || input == "KMP" || input == "KnuthMorrisPrett")
                return eMenu.KMP;
            else if (input == "P" || input == "Prefix")
                return eMenu.Prefix;
            else if (input == "C" || input == "Compare")
                return eMenu.Compare;
            else if (input == "End")
                throw new Exception("End");

            return eMenu.None;
        }

        private static string Read(string keyword)
        {
            Console.Write($"{keyword} : ");
            var text = Console.ReadLine();

            if (text == "End")
                throw new Exception("End");

            return text;
        }

        private static StringMatch Create(eMenu menu, string text, string pattern)
        {
            if (menu == eMenu.BruteForce)
                return new BruteForce(text, pattern);
            else if (menu == eMenu.KMP)
                return new KnuthMorrisPratt(text, pattern);

            return null;
        }

        private static void Print(StringMatch match, bool showTrace)
        {
            if(match == null)
            {
                Console.WriteLine($"Error : {nameof(match)} is null.");
                return;
            }

            match.Run();

            if(showTrace)
            {
                foreach (var trace in match.Traces)
                    Console.WriteLine(trace);
            }

            PrintResult(match);
        }

        private static void PrintResult(StringMatch match)
        {
            Console.WriteLine($"Result Index : {match.Result}");
        }

        private static void ReportComparing(
            int bruteForthIndex, TimeSpan bruteForthPerformance,
            int kmpIndex, TimeSpan kmpPerformance)
        {
            Console.WriteLine("---------------------------------");

            Console.WriteLine("* Bruth Forth");
            Console.WriteLine($"Index : {bruteForthIndex} / Performance : {bruteForthPerformance}");

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
