namespace Jaywapp.Algorithm.KMP
{
    public static class KnuthMorrisPratt
    {
        public static int Match(string text, string pattern)
        {
            int n = text.Length;
            int m = pattern.Length;

            var array = PrefixAnalysis.Analyze(pattern);

            int i = 0;
            int j = 0;
            while (i < n)
            {
                if (text[i] == pattern[j])
                    if (j == m - 1)
                        return i - j;
                    else
                    {
                        i++;
                        j++;
                    }
                else
                {
                    if (j > 0)
                        j = array[j - 1];
                    else
                        i++;
                }
            }

            return -1;
        }
    }
}
