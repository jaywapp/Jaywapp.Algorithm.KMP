namespace Jaywapp.KMP
{
    public static class KnuthMorrisPratt
    {
        public static int Match(string text, string pattern)
        {
			int n = text.Length;
			int m = pattern.Length;

			var array = PrefixFunction(pattern);

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

		public static int[] PrefixFunction(string pattern)
		{
            var array = new int[pattern.Length];

            array[0] = 0;
			int i = 1;
			int j = 0;

			while (i < pattern.Length)
			{
				if (pattern[i] == pattern[j])
				{
					array[i] = j + 1;
					i++;
					j++;
				}
				else if (j > 0)
				{
					j = array[j - 1];
				}
				else
				{
					array[j] = 0;
					i++;
				}
			}

			return array;
		}
	}
}
