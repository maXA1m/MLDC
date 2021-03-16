using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using MiddleLevelDevCourse.Extensions;

namespace MiddleLevelDevCourse.UnionLesson
{
    public class UnionLesson : ILesson
    {
        public void Run()
        {
            var barcelona1 = ParseBarcelona(@"./ThirdLesson/Data/Barcelona1.csv", 20).ToList();
            var barcelona2 = ParseBarcelona(@"./ThirdLesson/Data/barcelona2.csv", 20).ToList();

            barcelona1.Print("Barcelona1");
            barcelona2.Print("Barcelona2");
            UnionAll(barcelona1, barcelona2).Print("UNION ALL");
            Union(barcelona1, barcelona2, new BarcelonaEqualityComparer()).Print("UNION");
        }

        private static IEnumerable<T> UnionAll<T>(IEnumerable<T> x, IEnumerable<T> y)
        {
            return x.Concat(y);
        }

        private static IEnumerable<T> Union<T>(IEnumerable<T> x, IEnumerable<T> y, IEqualityComparer<T> comparer)
        {
            var union = new HashSet<T>(x, comparer);
            foreach(var i in y)
            {
                union.Add(i);
            }

            //union.UnionWith(y);

            return union;
        }

        private static IEnumerable<Barcelona> ParseBarcelona(string path, int? take = null)
        {
            var csvRegex = new Regex("(?:^|,)(?=[^\"]|(\")?)\"?((?(1)[^\"]*|[^,\"]*))\"?(?=,|$)");
            using var reader = new StreamReader(path);

            for (int i = 0; !reader.EndOfStream && (!take.HasValue || i <= take); i++)
            {
                var values = csvRegex.Matches(reader.ReadLine());
                if(i == 0)
                {
                    continue;
                }

                if (!int.TryParse(GetValue(values, 0), out var id))
                {
                    var listingUrl = GetValue(values, 0).Split('/');
                    id = Convert.ToInt32(listingUrl[^1]);
                }

                yield return new Barcelona(id)
                {
                    Name = GetValue(values, 1),
                    ZipCode = Convert.ToInt32(GetValue(values, 2)),
                    Location = GetValue(values, 3),
                    Country = GetValue(values, 4),
                    Latitude = Convert.ToDecimal(GetValue(values, 5)),
                    Longitude = Convert.ToDecimal(GetValue(values, 6))
                };
            }

            static string GetValue(MatchCollection matches, int i)
            {
                return matches[i].Groups[^1].Value;
            }
        }
    }
}
