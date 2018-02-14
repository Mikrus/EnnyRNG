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
            public double L_bound { get; set; }
            public double U_bound { get; set; }
            /// <summary>
            /// Visszaadja az intervallum hosszát.
            /// </summary>
            /// <returns>Az intervallum hossza.</returns>
            public double Length
            {
                get { return U_bound - L_bound; }
            }

            public abstract double ValueAt(double x);
        }
    }