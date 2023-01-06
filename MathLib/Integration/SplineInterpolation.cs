using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathLib.Objects;
using MathLib.Аpproximation;
using static System.Math;

namespace MathLib.Integration
{
    public abstract class SplineInterpolation
    {
        public static double Approximate(double a, double b, Function f, double eps)
        {
            int n = 50;
            double h;
            Polynomial[] c;
            double[] x = new double[n + 1];
            double result = 0;
            double resultPrev = 0;
            double delta;

            do
            {
                resultPrev = result;
                result = 0;

                n *= 2;
                h = (b - a) / n;

                x = new double[n + 1];
                for (int i = 0; i <= n; i++)
                {
                    x[i] = a + h * i;
                }

                c = Аpproximation.SplineInterpolation.Type4.Approximate(x, f);
                for (int i = 0; i < n; i++)
                {
                    c[i] = c[i].Integrate();
                    result += c[i].Calculate(x[i + 1]);
                    result -= c[i].Calculate(x[i]);
                }

                delta = (resultPrev - result) / 7;
            }
            while (Abs(delta) >= eps);

            return resultPrev + delta;
        }
    }
}
