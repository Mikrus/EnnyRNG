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

        private List<PartFunction> fx;
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
            //Rendezés, diszjunktság vizsgálat majd megnézzük, hogy az átadott fv belefér-e az átadott [min;max[-ba
            intervals.Sort();
            for (int i = 0; i < intervals.Count - 1; i++)
            {
                if(intervals[i].U_bound > intervals[i + 1].L_bound)
                    throw new NotDisjointException("A megadott intervallumok osszeernek");
            }
            if(intervals[0].L_bound < min || intervals[intervals.Count-1].U_bound > min)
                throw new IndexOutOfRangeException("A megadott min-max szűkebb, mint az intervallumok lista össz intervallumai.");


            fx = new List<PartFunction>();
            //kiszámoljuk, hogy mennyi helyen nem definiált a fv és erre mekkora valószínűség maradt
            double remInterval = max - min;
            double remProp = 1;
            foreach (Interval i in intervals)
            {
                remInterval -= i.Length;
                remProp -= i.Prob;
            }
            //meghoatározzuk, hogy mekkora legyen a meredeksége a fv-nek a nem definiált helyeken
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
            //megkonstruáljuk az eloszlásfüggvényt
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
                    yUpper = yLower + deriv0 * (intervals[i].L_bound - X_prev);
                    deriv = 1 / deriv0;
                    b = min + X_prev - deriv * yLower;
                    fx.Add(new LinearFunc(yLower, yUpper, deriv, b));
                }

                yLower = yUpper;
                yUpper = yLower + intervals[i].Prob;
                deriv = intervals[i].Length / intervals[i].Prob;
                b = min + intervals[i].L_bound - deriv * yLower;
                fx.Add(new LinearFunc(yLower, yUpper, deriv, b));
                X_prev = intervals[i].U_bound;
            }

            if (yUpper != 1)
                fx.Add(new LinearFunc(yUpper, 1, 1 / deriv0, max - 1 / deriv0 * 1));
            rnd = new Random();
        }

        /// <summary>
        /// Generál egy véletlen számot
        /// </summary>
        /// <returns>A generált véletlen double-t.</returns>
        public double Generate()
        {
            double rn = rnd.NextDouble();

            foreach (PartFunction pf in fx)
            {
                if (rn >= pf.L_bound && rn < pf.U_bound)
                    return pf.ValueAt(rn);
            }

            return -1;
        }
    }
}