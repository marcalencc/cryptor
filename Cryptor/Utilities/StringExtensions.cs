using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cryptor.Utilities
{
    public static class StringExtensions
    {
        public static bool Contains(this string source, string other, StringComparison comparisonType)
        {
            return source?.IndexOf(other, comparisonType) >= 0;
        }
    }
}
