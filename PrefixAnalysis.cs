using System.Collections.Generic;
using System.Text;

namespace Jaywapp.Algorithm.KMP
{
    public class PrefixAnalysis : ITracable
    {
        #region Const Field
        private const string TAB = "\t";
        #endregion

        #region Internal Field
        private readonly string _pattern;
        #endregion

        #region Properties
        public int[] Values { get; private set; }
        public List<string> Traces { get; private set; } = new List<string>();
        #endregion

        #region Constructor
        public PrefixAnalysis(string pattern)
        {
            _pattern = pattern;
            Values = new int[pattern.Length];
        }
        #endregion

        #region Functions
        public static int[] Analyze(string pattern)
        {
            var analysis = new PrefixAnalysis(pattern);
            analysis.Analyze();
            
            return analysis.Values;
        }

        public void Analyze()
        {
            var array = new int[_pattern.Length];

            array[0] = 0;
            int i = 1;
            int j = 0;

            while (i < _pattern.Length)
            {
                var isMatched = _pattern[i] == _pattern[j];

                AddTrace(array, j, i, isMatched ? $"{_pattern[i]}, {_pattern[j]} is same" : $"{_pattern[i]}, {_pattern[j]} is not same.");

                if (isMatched)
                {
                    AddTrace(array, j, i, $"array[{i}] = {j} + 1 and j, i increase.");
                    array[i] = j + 1;
                    i++;
                    j++;
                }
                else if (j > 0)
                {
                    var v = array[j - 1];

                    AddTrace(array, j, i, $"j = array[j - 1] ({v})");
                    j = v;
                }
                else
                {
                    AddTrace(array, j, i, $"array[{j}] = 0 and i increase.");
                    array[j] = 0;
                    i++;
                }
            }

            Values = array;
        }
        private void AddTrace(int[] array, int j, int i, string description)
        {
            Traces.Add(CreateTrace(_pattern, array, j, i, description));
        }

        public static string CreateTrace(string pattern, int[] values, int j, int i, string description)
        {
            var builder = new StringBuilder();

            builder.AppendLine($"Desc : {description}");

            builder.Append($"Index{TAB}|{TAB}");
            builder.AppendLine(DisplayHelper.DisplayIndex(pattern.Length));
            builder.Append($"Pivot{TAB}|{TAB}");
            builder.AppendLine(DisplayHelper.DisplayPivot(pattern.Length, j, "j", i, "i"));
            builder.Append($"Pattern{TAB}|{TAB}");
            builder.AppendLine(DisplayHelper.DisplayArray(pattern.ToCharArray()));
            builder.Append($"Value{TAB}|{TAB}");
            builder.AppendLine(DisplayHelper.DisplayArray(values));

            return builder.ToString();
        }
        #endregion
    }
}
