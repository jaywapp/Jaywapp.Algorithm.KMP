namespace Jaywapp.Algorithm.KMP
{
    public class BruteForce : StringMatch
    {
        #region Constructor
        public BruteForce(string text, string pattern) : base(text, pattern)
        {
        }
        #endregion

        #region Functions
        public override void Run()
        {
            var count = _text.Length - _pattern.Length;

            for(int i = 0; i<= count; i++)
            {
                int j;

                for(j = 0; j < _pattern.Length; j++)
                {
                    AddTrace(i);

                    var t = _text[i + j];
                    var p = _pattern[j];

                    if (t != p)
                        break;
                }

                if (j == _pattern.Length)
                {
                    Result = i;
                    return;
                }
            }

            return;
        }

        public static int Match(string text, string pattern)
        {
            var bruteForce = new BruteForce(text, pattern);
            bruteForce.Run();

            return bruteForce.Result;
        }
        #endregion
    }
}
