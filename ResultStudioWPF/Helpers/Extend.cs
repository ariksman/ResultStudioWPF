using System;
using System.Collections.Generic;
using System.Linq;

namespace ResultStudioWPF.Helpers
{
    public static class Extend
    {
        public static double StandardDeviation(this IEnumerable<double> values)
        {
          var localValues = values.ToList();
          double avg = localValues.Average();
            return Math.Sqrt(localValues.Average(v => Math.Pow(v - avg, 2)));
        }
    }
}