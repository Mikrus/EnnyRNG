using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnnyRNG.Functions
    {
        /// <summary>
        /// Absztrakt osztály a részfüggvények tárolására.
        /// </summary>
        public abstract class PartFunction
        {
            private double L_bound;
            private double U_bound;

            public double GetL_bound()
            {
                return L_bound;
            }

            public void SetL_bound(double l_bound)
            {
                L_bound = l_bound;
            }

            public double GetU_bound()
            {
                return U_bound;
            }

            public void SetU_bound(double u_bound)
            {
                U_bound = u_bound;
            }

            /// <summary>
            /// Visszaadja az intervallum hosszát.
            /// </summary>
            /// <returns>Az intervallum hossza.</returns>
            public double Length()
            {
                return U_bound - L_bound;
            }

            public abstract double ValueAt(double x);
        }
    }