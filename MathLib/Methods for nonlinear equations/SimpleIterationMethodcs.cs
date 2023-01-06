using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathLib.Objects;
using static System.Math;

namespace MathLib.SolvingNonlinearEquations
{
    public abstract class SimpleIterationMethod
    {
        public static double Solve(double a, double b, double eps, Function phi)
        {
            double x = a;
            Dictionary<string, double> xPrev = new Dictionary<string, double>() { { "x", 0 } };

            do
            {
                xPrev["x"] = x;

                x = phi.Calculate(xPrev);
            }
            while (Abs(x - xPrev["x"]) > eps);

            return x;
        }
    }
}
