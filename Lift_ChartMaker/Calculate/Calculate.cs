using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lift_ChartMaker.Models;

namespace Lift_ChartMaker.Calculate
{
    public class Calculate
    {
        // wzór na 1RM McGlothina 
        public static double OneRepMax(List<Scores> Scores)
        {
            if (Scores.Count != 0)
            {
                List<double> l = new List<double>();
                double w = 0;
                foreach (var s in Scores)
                {
                    w = (100 * s.Weight) / (101.3 - 2.67123 * s.Reps);
                    l.Add(w);
                }
                return l.Max();
            }
            return 0;
        }

        public static List<double> CountVolume(List<Scores> Scores)
        {
            List<double> l = new List<double>();
            double sc;
            foreach (var s in Scores)
            {
                sc = s.Weight * s.Reps;
                l.Add(sc);
            }
            return l;
        }
    }
}
