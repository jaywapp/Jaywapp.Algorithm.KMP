using System.Collections.Generic;
using System.Text;

namespace Jaywapp.Algorithm.KMP
{
    public abstract class StringMatch : ITracable
    {
        #region Internal Field
        protected readonly string _text;
        protected readonly string _pattern;
        #endregion

        #region Properties
        public List<string> Traces { get; } = new List<string>();
        public int Result { get; protected set; } = -1;
        #endregion

        #region Constructor
        public StringMatch(string text, string pattern)
        {
            _text = text;
            _pattern = pattern;
        }
        #endregion

        #region Functions
        public abstract void Run();

        protected void AddTrace(int index)
        {
            Traces.Add(CreateTrace(_text, _pattern, index));
        }

        protected static string CreateTrace(string text, string pattern, int i)
        {
            var builder = new StringBuilder();

            builder.AppendLine(DisplayHelper.DisplayArray(text.ToCharArray()));
            builder.AppendLine(DisplayHelper.DisplayArray(text.Length, i, pattern.ToCharArray()));

            return builder.ToString();
        }
        #endregion
    }
}
