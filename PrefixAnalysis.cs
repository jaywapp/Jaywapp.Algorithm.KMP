using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jaywapp.Algorithm.KMP
{
    public class PrefixAnalysis
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

            var states = new List<string>();

            void AddState(string desc)
            {
                states.Add(ToStateString(_pattern, array, j, i, desc));
            }

            while (i < _pattern.Length)
            {
                var isMatched = _pattern[i] == _pattern[j];

                AddState(isMatched ? $"{_pattern[i]}, {_pattern[j]} is same" : $"{_pattern[i]}, {_pattern[j]} is not same.");

                if (isMatched)
                {
                    AddState($"array[{i}] = {j} + 1 and j, i increase.");
                    array[i] = j + 1;
                    i++;
                    j++;
                }
                else if (j > 0)
                {
                    var v = array[j - 1];

                    AddState($"j = array[j - 1] ({v})");
                    j = v;
                }
                else
                {
                    AddState($"array[{j}] = 0 and i increase.");
                    array[j] = 0;
                    i++;
                }
            }

            Traces = states;
            Values = array;
        }

        public static string ToStateString(string pattern, int[] values, int j, int i, string description)
        {
            var builder = new StringBuilder();

            builder.AppendLine($"Desc : {description}");

            builder.Append($"Index{TAB}|{TAB}");
            builder.AppendLine(GetIndexArrayString(pattern.Length));
            builder.Append($"Pivot{TAB}|{TAB}");
            builder.AppendLine(GetPivotArrayString(pattern.Length, j, i));
            builder.Append($"Pattern{TAB}|{TAB}");
            builder.AppendLine(GetPatternArrayString(pattern));
            builder.Append($"Value{TAB}|{TAB}");
            builder.AppendLine(GetValueArrayString(values));

            return builder.ToString();
        }

        private static string GetIndexArrayString(int length)
        {
            return string.Join(TAB, Enumerable.Range(0, length));
        }

        private static string GetPivotArrayString(int length, int j, int i)
        {
            var array = new string[length];

            for (int k = 0; k < length; k++)
            {
                if (k == j)
                    array[k] = "j";
                else if (k == i)
                    array[k] = "i";
                else
                    array[k] = string.Empty;
            }

            return string.Join(TAB, array);
        }


        private static string GetPatternArrayString(string pattern) => string.Join(TAB, pattern.ToCharArray());

        private static string GetValueArrayString(int[] values) => string.Join(TAB, values);

        #endregion
    }
}
