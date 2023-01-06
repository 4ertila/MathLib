using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathLib.Objects;

namespace MathLib.Integration
{
    public abstract class GaussQuadratureFormulas
    {
        public static double Approximate(double a, double b, double[] x, Function f, Function p, double eps)
        {
            int n = x.Length;
            double result = 0;
            double c;

            Polynomial tPolynomial;
            for (int i = 0; i < n; i++)
            {
                tPolynomial = new Polynomial(1d);
                for (int j = 0; j < n; j++)
                {
                    if (j != i)
                    {
                        tPolynomial *= new Polynomial(new double[] { -x[j], 1 }) / (x[i] - x[j]);
                    }
                }

                Function tF = p * tPolynomial.ToFunction();
                c = TrapeziumMethod.Approximate(a, b, tF, eps);

                f["x"] = x[i];
                result += c * f.Calculate();
            }

            return result;
        }
    }
}
