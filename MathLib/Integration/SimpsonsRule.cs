using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathLib.Objects;
using static System.Math;

namespace MathLib.Integration
{
    public abstract class SimpsonsRule
    {
        public static double Approximate(double a, double b, Function f, double eps)
        {
            f["x"] = 1;
            int n = 50;
            double h;
            double result = 0;
            double resultPrev = 0;
            double delta;
            Dictionary<string, double> x1 = new Dictionary<string, double>() { { "x", 0 } };
            Dictionary<string, double> x2 = new Dictionary<string, double>() { { "x", 0 } };
            Dictionary<string, double> x3 = new Dictionary<string, double>() { { "x", 0 } };

            do
            {
                n *= 2;
                h = (b - a) / n;

                resultPrev = result;
                result = 0;

                for (int i = 1; i <= n; i++)
                {
                    x1["x"] = a + h * i;
                    x2["x"] = a + h * i - h / 2;
                    x3["x"] = a + h * (i - 1);

                    result += (f.Calculate(x1) +
                               4 * f.Calculate(x2) +
                               f.Calculate(x3));
                }
                result *= h / 6;

                delta = (resultPrev - result) / 15;
            }
            while (Abs(delta) >= eps);

            return resultPrev + delta;
        }
    }
}
