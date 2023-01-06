using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathLib.Objects;
using static System.Math;
using MathLib.Аpproximation;

namespace MathLib.Integration
{
    public abstract class InterpolationMethod
    {
        public static double Approximate(double a, double b, double[] x, Function f)
        {
            Polynomial L;
            double result = 0;

            L = NewtonPolynomial.Approximate(x, f);
            L = L.Integrate();

            result += L.Calculate(b);
            result -= L.Calculate(a);

            return result;
        }

        public static double Approximate(double a, double b, Function f, int n)
        {
            double h;
            Polynomial L;
            double[] x;
            double result = 0;

            h = (b - a) / n;
            result = 0;
            x = new double[n + 1];
            for (int i = 0; i <= n; i++)
            {
                x[i] = a + h * i;
            }

            L = NewtonPolynomial.Approximate(x, f);
            L = L.Integrate();

            result += L.Calculate(b);
            result -= L.Calculate(a);

            return result;
        }
    }
}
