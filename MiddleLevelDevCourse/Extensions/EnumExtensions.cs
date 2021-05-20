using System;
using System.Collections.Generic;

namespace MiddleLevelDevCourse.Extensions
{
    public static class EnumExtensions
    {
        public static IEnumerable<T> GetFlags<T>(this T input) where T : Enum
        {
            foreach (T value in Enum.GetValues(typeof(T)))
            {
                if (input.HasFlag(value))
                {
                    yield return value;
                }
            }
        }
    }
}
