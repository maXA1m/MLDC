using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace MiddleLevelDevCourse.ThirdLesson
{
    public class BarcelonaEqualityComparer : EqualityComparer<Barcelona>
    {
        public override bool Equals(Barcelona x, Barcelona y)
        {
            return x.Id.Equals(y.Id);
        }

        public override int GetHashCode([DisallowNull] Barcelona obj)
        {
            return obj.Id.GetHashCode();
        }
    }
}
