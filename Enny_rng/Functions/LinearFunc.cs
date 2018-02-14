using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Enny_rng.Exceptions;


/// <summary>
/// A függvények, amiket az eloszlás kezelni tud.
/// </summary>
namespace EnnyRNG.Functions
{
        /// <summary>
        /// Lineáris függvényt reprezentál.
        /// </summary>
        /// <seealso cref="EnnyRNG.Functions.PartFunction" />
        public class LinearFunc : PartFunction
        {
            private double deriv;
            private double b;


            /// <summary>
            /// Létrehoz egy példányt a  <see cref="LinearFunc"/> osztályból.
            /// </summary>
            /// <param name="L_bound">Az alsó határ.</param>
            /// <param name="U_bound">A felső határ.</param>
            /// <param name="deriv">A meredekség.</param>
            /// <param name="b">Az eltolás.</param>
            /// <exception cref="LowerboundMoreThanUpperboundException">Az also hatar nagyobb a felsonel</exception>
            public LinearFunc(double L_bound, double U_bound, double deriv, double b)
            {
                if (U_bound < L_bound)
                    throw new LowerboundMoreThanUpperboundException("Az also hatar nagyobb a felsonel");
                SetL_bound(L_bound);
                SetU_bound(U_bound);
                this.deriv = deriv;
                this.b = b;
            }

            public double GetDeriv()
            {
                return deriv;
            }

            public double GetB()
            {
                return b;
            }

            /// <summary>
            /// Kiszámolja a függvény értékét az x helyen.
            /// </summary>
            /// <param name="x">A kiszámolandó hely.</param>
            /// <returns>A függvény értéke az x helyen.</returns>
            public override double ValueAt(double x)
            {
                return deriv * x + b;
            }
        }
    }