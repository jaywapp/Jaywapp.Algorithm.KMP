using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jaywapp.Algorithm.KMP
{
    public static class DisplayHelper
    {
        #region Const Field
        private const string TAB = "\t";
        #endregion

        public static string DisplayIndex(int length) => string.Join(TAB, Enumerable.Range(0, length));

        public static string DisplayArray<T>(T[] array) => string.Join(TAB, array);

        public static string DisplayPivot(int length, int i, string iKeyword, int j, string jKeyword)
        {
            var array = new string[length];

            for (int k = 0; k < length; k++)
            {
                if (k == j)
                    array[k] += jKeyword;
                else if (k == i)
                    array[k] += iKeyword;
                else
                    array[k] = string.Empty;
            }

            return string.Join(TAB, array);
        }

        public static string DisplayArray<T>(int length, int index, T[] array)
        {
            var result = new string[length];

            for (int i = 0; i < array.Length; i++)
            {
                if (i + index >= length)
                    continue;

                result[i + index] = array[i].ToString();
            }

            return string.Join(TAB, result);
        }

    }
}
