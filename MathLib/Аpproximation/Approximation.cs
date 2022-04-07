using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathLib.Аpproximation
{
    public abstract class Approximation
    {
        public delegate double Function(double x);

        internal static void Combinations(ref double a, int degree, double[] x, int startPos = 0, double tValue = 1)
        {
            if (degree - 1 > 0)
            {
                for (int i = startPos; i < x.Length - degree + 1; i++)
                {
                    if (x[i] != 0)
                    {
                        Combinations(ref a, degree - 1, x, i + 1, tValue * -x[i]);
                    }
                    else
                    {
                        continue;
                    }
                }
            }
            else
            {
                for (int i = startPos; i < x.Length; i++)
                {
                    a += tValue * (-x[i]);
                }
            }
        }
    }
}
