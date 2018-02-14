using System;
using System.Collections.Generic;
using EnnyRNG;
using EnnyRNG.Functions;
using Enny_rng.Exceptions;

/// <summary>
/// Random szám generátor Ennyhez.
/// </summary>
namespace EnnyRNG
{
    /// <summary>
    /// Eloszlás, amely a paraméterezése szerint generál random számokat.
    /// </summary>
    public class Distribution
    {

        private List<PartFunction> Fx;
        private double deriv0;
        private double min;
        private Random rnd;

        /// <summary>
        /// Létrehoz egy példányt a  <see cref="Distribution"/> osztályból.
        /// </summary>
        /// <param name="min">A minimum generálandó érték.</param>
        /// <param name="max">A maximum generálandó érték.</param>
        /// <param name="intervals">Az intervallumok, amik alapján generálni kell a random számokat.</param>
        /// <exception cref="Enny_rng.Exceptions.NotDisjointException">A megadott intervallumok osszeernek</exception>
        /// <exception cref="Enny_rng.Exceptions.InvalidPrababilityException">A megadott intervallumok kitoltik a rendelkezesre allo helyet, de az ossz valoszinuseguk nem 1</exception>
        public Distribution(double min, double max, List<Interval> intervals)
        {
            intervals.Sort();
            for (int i = 0; i < intervals.Count - 1; i++)
            {
                if(intervals[i].getU_bound() > intervals[i + 1].getL_bound())
                    throw new NotDisjointException("A megadott intervallumok osszeernek");
            }
            if(intervals[0].getL_bound() < min || intervals[intervals.Count-1].getU_bound() > min)
                throw new IndexOutOfRangeException("A megadott min-max szűkebb, mint az intervallumok lista össz intervallumai.");


            Fx = new List<PartFunction>();

            double remInterval = max - min;
            double remProp = 1;
            for (int i = 0; i < intervals.Count; i++)
            {
                remInterval -= intervals[i].length();
                remProp -= intervals[i].getProb();
            }

            double deriv0;
            if (remProp < 0.001)
            {
                deriv0 = 0;
            }
            else if (remInterval != 0)
            {
                deriv0 = remProp / remInterval;
            }
            else
            {
                if (remProp != 0)
                    throw new InvalidPrababilityException("A megadott intervallumok kitoltik a rendelkezesre allo helyet, de az ossz valoszinuseguk nem 1");
                deriv0 = 1;
            }

            double yLower = 0;
            double yUpper = 0;
            double b = 0;
            double deriv;
            double X_prev = min;
            for (int i = 0; i < intervals.Count; i++)
            {
                if (deriv0 != 0)
                {
                    yLower = yUpper;
                    yUpper = yLower + deriv0 * (intervals[i].getL_bound() - X_prev);
                    deriv = 1 / deriv0;
                    b = min + X_prev - deriv * yLower;
                    Fx.Add(new LinearFunc(yLower, yUpper, deriv, b));
                }

                yLower = yUpper;
                yUpper = yLower + intervals[i].getProb();
                deriv = intervals[i].length() / intervals[i].getProb();
                b = min + intervals[i].getL_bound() - deriv * yLower;
                Fx.Add(new LinearFunc(yLower, yUpper, deriv, b));
                X_prev = intervals[i].getU_bound();
            }

            if (yUpper != 1)
                Fx.Add(new LinearFunc(yUpper, 1, 1 / deriv0, max - 1 / deriv0 * 1));
            this.deriv0 = 1 / deriv0;
            this.min = min;
            rnd = new Random();
        }

        /// <summary>
        /// Generál egy véletlen számot
        /// </summary>
        /// <returns>A generált véletlen double-t.</returns>
        public double Generate()
        {
            double rn = rnd.NextDouble();

            for (int i = 0; i < Fx.Count; i++)
            {
                if (rn >= Fx[i].GetL_bound() && rn < Fx[i].GetU_bound())
                    return Fx[i].ValueAt(rn);
            }

            return -1;
        }
    }
}