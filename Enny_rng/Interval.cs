using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;
using Enny_rng.Exceptions;

namespace EnnyRNG
{
    /// <summary>
    /// Tárolja, hogy mettől, meddig, mekkora valószínűséggel sorsoljon véletlen számot.
    /// </summary>
    /// <seealso cref="Interval" />
    public class Interval : IComparable<Interval>
    {
        
        private double prob;

        public double L_bound { set; get; }
        public double U_bound { set; get; }

        public double Prob
        {
            set
            {
                if (value > 1)
                {
                    throw new InvalidPrababilityException("A megadott valoszinuseg nagyobb mint 1");
                }
                prob = value;
            }
            get { return prob; }
        }

        public double Length
        {
            get
            {
                return U_bound - L_bound;
            }
        }

        public Interval(double L_bound, double U_bound, double prob)
        {
            this.L_bound = L_bound;
            this.U_bound = U_bound;
            if (prob > 1)
            {
                throw new InvalidPrababilityException("A megadott valoszinuseg nagyobb mint 1");
            }
            else
            {
                this.prob = prob;
            }
        }

        public int CompareTo(Interval i)
        {
            return (int)Math.Sign(L_bound - i.L_bound);
        }
    }
}
