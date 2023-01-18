namespace Jaywapp.KMP
{
    public static class BruthForce
    {
        public static int Match(string text, string pattern)
        {
            // text char 반복
            for (int i = 0; i< text.Length - pattern.Length; i++)
            {
                int j = 0;
                // pattern char를 반복하면서 해당 위치의 text char와 비교
                while (j < pattern.Length && pattern[j] == text[i + j])
                    j++;

                // pattern와 text의 부분 문자열이 같을 경우
                var isMatched = j == pattern.Length;
                if(isMatched)
                    return i;
            }

            return -1;
        }
    }
}
