using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Enny_rng.Exceptions;

namespace EnnyRNG
{
    /// <summary>
    /// Tárolja, hogy mettől, meddig, mekkora valószínűséggel sorsoljon véletlen számot.
    /// </summary>
    /// <seealso cref="Interval" />
    public class Interval : IComparable<Interval>
    {
        private double L_bound;
        private double U_bound;
        private double prob;

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
    
        public void setL_bound(double l_bound)
        {
            L_bound = l_bound;
        }

        public void setU_bound(double u_bound)
        {
            U_bound = u_bound;
        }

        public void setProb(double prob)
        {
            if(prob > 1){
                throw new InvalidPrababilityException("A megadott valoszinuseg nagyobb mint 1");
            }
            this.prob = prob;
        }

        public double getU_bound()
        {
            return U_bound;
        }

        public double getProb()
        {
            return prob;
        }

        public double getL_bound()
        {
            return L_bound;
        }

        public double length()
        {
            return U_bound - L_bound;
        }

        public int CompareTo(Interval i)
        {
            return (int)Math.Sign(L_bound - i.getL_bound());
        }
    }
}
