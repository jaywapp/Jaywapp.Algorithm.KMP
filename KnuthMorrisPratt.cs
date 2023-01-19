namespace Jaywapp.Algorithm.KMP
{
    public class KnuthMorrisPratt : StringMatch
    {
        #region Constructor
        public KnuthMorrisPratt(string text, string pattern) : base(text, pattern)
        {
        }
        #endregion

        #region Functions
        public override void Run()
        {
            int n = _text.Length;
            int m = _pattern.Length;

            var array = PrefixAnalysis.Analyze(_pattern);

            int i = 0; // Text내 인덱스
            int j = 0; // Pattern내 인덱스

            while (i < n)
            {
                AddTrace(i - j);

                // isMatched
                if (_text[i] == _pattern[j])
                {
                    // j가 pattern의 마지막 index일 때
                    // (pattern과 모두 일치하는 문자를 찾았을 때)
                    if (j == m - 1)
                    {
                        Result = i - j;

                        return;
                    }
                    else
                    {
                        // 다음 비교를 위한 인덱스 증가
                        i++;
                        j++;
                    }
                }
                else
                {
                    // pattern의 앞 문자들과 일치한 문자열일 존재했을 경우
                    if (j > 0)
                        j = array[j - 1]; // Prefix 정보 활용
                    else // 다음 단계 진행
                        i++;
                }
            }

            Result = -1;
            return;
        }

        public static int Match(string text, string pattern)
        {
            var kmp = new KnuthMorrisPratt(text, pattern);
            kmp.Run();

            return kmp.Result;
        }
        #endregion
    }
}
