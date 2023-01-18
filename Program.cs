using System;

namespace Jaywapp.KMP
{
    class Program
    {
        static void Main(string[] args)
        {
            RunPrefixFunction();
            // RunStringMatch();
        }

        private static void RunPrefixFunction()
        {
            while (true)
            {
                Console.Write("pattern : ");
                var pattern = Console.ReadLine();
                if (pattern == "EOF")
                    break;

                var array = KnuthMorrisPratt.PrefixFunction(pattern);
                var chars = pattern.ToCharArray();

                Console.WriteLine(string.Join(" | ", chars));
                Console.WriteLine(string.Join(" | ", array));
            }

            Console.ReadLine();
        }


        private static void RunStringMatch()
        {
            while (true)
            {
                Console.Write("text : ");
                var text = Console.ReadLine();
                if (text == "EOF")
                    break;
                Console.Write("pattern : ");
                var pattern = Console.ReadLine();
                if (pattern == "EOF")
                    break;

                if (!TryRun(BruthForce.Match, text, pattern, out int bruthForthIndex, out TimeSpan bruthForthPerformance)
                    || !TryRun(KnuthMorrisPratt.Match, text, pattern, out int kmpIndex, out TimeSpan kmpPerformance))
                    break;

                Report(bruthForthIndex, bruthForthPerformance, kmpIndex, kmpPerformance);
            }

            Console.ReadLine();
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
